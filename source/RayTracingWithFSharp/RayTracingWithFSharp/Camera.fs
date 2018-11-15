module RayTracingWithFSharp.Camera

open Vector
open Ray

type Camera () =
    let lowerLeftCorner = vector(-2.0, -1.0, -1.0)
    let horizontal = vector(4.0, 0.0, 0.0)
    let vertical = vector(0.0, 2.0, 0.0)
    let origin = vector(0.0, 0.0, 0.0)

    member this.GetRay (u : float, v : float) =
        {A = origin
         B = lowerLeftCorner + (u .* horizontal) + (v .* vertical) - origin}