module RayTracingWithFSharp.Vector

type Vector = {X : float; Y : float; Z : float} with
    member this.R = this.X
    member this.G = this.Y
    member this.B = this.Z

    static member (.+) (a:float, v:Vector) = {X=a+v.X; Y=a+v.Y; Z=a+v.Z}
    static member (.*) (a:float, v:Vector) = {X=a*v.X; Y=a*v.Y; Z=a*v.Z}
    static member (./) (v:Vector, a:float) = {X=v.X/a; Y=v.Y/a; Z=v.Z/a}

    static member (+) (u:Vector, v:Vector) = {X=u.X+v.X; Y=u.Y+v.Y; Z=u.Z+v.Z}
    static member (-) (u:Vector, v:Vector) = {X=u.X-v.X; Y=u.Y-v.Y; Z=u.Z-v.Z}
    static member (*) (u:Vector, v:Vector) = {X=u.X*v.X; Y=u.Y*v.Y; Z=u.Z*v.Z}
    static member (/) (u:Vector, v:Vector) = {X=u.X/v.X; Y=u.Y/v.Y; Z=u.Z/v.Z}

let vector (x,y,z) = {X=x; Y=y; Z=z}

let square x = x*x

let mapVector f v = {X = f v.X; Y = f v.Y; Z = f v.Z}

let sumVector v = v.X + v.Y + v.Z

let dotProduct u v = sumVector (u*v)

let crossProduct u v =
    {X = u.Y*v.Z - v.Y*u.Z
     Y = v.X*u.Z - u.X*v.Z
     Z = u.X*v.Y - v.X*u.Y}

let norm v = sqrt(dotProduct v v)

let normSquared v = dotProduct v v

let normalize v = v ./ (norm v)

let roundVector v = mapVector round v