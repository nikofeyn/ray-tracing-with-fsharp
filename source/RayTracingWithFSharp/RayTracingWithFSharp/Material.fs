module RayTracingWithFSharp.Material

open Vector
open Ray
open Hitable

[<AbstractClass>]
type Material =
    abstract member Scatter: ray : Ray * attenuation : Vector * scattered : Ray -> HitRecord option