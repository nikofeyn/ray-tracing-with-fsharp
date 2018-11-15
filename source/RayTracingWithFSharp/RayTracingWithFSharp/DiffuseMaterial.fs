module RayTracingWithFSharp.DiffuseMaterial

open Vector
open System

(*
let random () =
    let r = Random()
    r.NextDouble()

let randomVector () = vector(random(), random(), random())
*)

let next (r : Random) = r.NextDouble()

let randomInUnitSphere (r : Random) =
    let mutable p = 2.0 .* vector(next(r),next(r),next(r)) - vector(1.0,1.0,1.0)
    while normSquared p >= 1.0 do
        p <- 2.0 .* vector(next(r),next(r),next(r)) - vector(1.0,1.0,1.0)
    p

let randomInUnitSphere2 () =
    let r1 = Random()
    let r2 = Random()
    let r3 = Random()
    let getNext (random : Random) = 2.0 * random.NextDouble() - 1.0
    let one = vector(0.95,0.95,0.95)
    let rec helper (test : Vector) =
        if normSquared test >= 1.0
        then helper (vector(getNext r1, getNext r2, getNext r3))
        else test
    helper (vector(getNext r1, getNext r2, getNext r3))