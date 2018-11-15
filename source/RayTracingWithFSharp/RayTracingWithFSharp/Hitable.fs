module RayTracingWithFSharp.Hitable

open Vector
open Ray

type HitRecord = {Time : float; P : Vector; Normal : Vector}

[<AbstractClass>]
type Hitable () =
    abstract member Hit: Ray * float * float -> HitRecord option
    //abstract member HitImperative: Ray * float * float * HitRecord ref -> bool

type Sphere (center : Vector, radius : float) =
    inherit Hitable()
    new() = Sphere(vector(0.0, 0.0, 0.0), 0.0)

    override this.Hit(ray, tMin, tMax) =
        let oc = ray.Origin - center
        let a = dotProduct ray.Direction ray.Direction
        let b = dotProduct oc ray.Direction
        let c = (dotProduct oc oc) - radius*radius
        let discriminant = b*b - a*c
        if discriminant > 0.0
        then
            let temp1 = (-b - sqrt(b*b-a*c))/a
            if temp1 < tMax && temp1 > tMin
            then
                let time = temp1
                let p = pointAtParameter ray time
                let n = (p - center)./radius
                Some {Time = time; P = p; Normal = n}
            else
                let temp2 = (-b + sqrt(b*b-a*c))/a
                if temp2 < tMax && temp2 > tMin
                then
                    let time = temp2
                    let p = pointAtParameter ray time
                    let n = (p - center)./radius
                    Some {Time = time; P = p; Normal = n}
                else None
        else None

type HitableList (hitableList : Hitable list) =
    inherit Hitable()

    override this.Hit(ray, tMin, tMax) =
        let mutable tempRecord : HitRecord option = None
        let mutable closestSoFar = tMax
        for i in hitableList do
            match i.Hit(ray,tMin,closestSoFar) with
            | Some record as r-> closestSoFar <- record.Time
                                 tempRecord <- r
            | None -> ()
        tempRecord