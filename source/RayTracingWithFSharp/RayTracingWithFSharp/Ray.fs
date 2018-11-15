module RayTracingWithFSharp.Ray

open Vector

type Ray = {A : Vector; B : Vector} with
    member this.Origin = this.A
    member this.Direction = this.B

let pointAtParameter (ray : Ray) time =
    ray.Origin + (time .* ray.Direction)