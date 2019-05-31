module RayTracingWithFSharp.DiffuseMaterial

open System
open Vector

// This implements a do-while loop as in the book.
let randomInUnitSphere (random : Random) =
    let newRandomVector () = 2.0 .* vector(random.NextDouble(),random.NextDouble(),random.NextDouble()) - vector(1.0,1.0,1.0)
    let mutable p = newRandomVector()
    while normSquared p >= 1.0 do
        p <- newRandomVector()
    p

// This is a more functional approach using tail recursion.
let randomInUnitSphere2 (random : Random) =
    let newRandomVector () = 2.0 .* vector(random.NextDouble(),random.NextDouble(),random.NextDouble()) - vector(1.0,1.0,1.0)
    let rec helper (p : Vector) =
        if normSquared p >= 1.0
        then helper (newRandomVector())
        else p
    helper (newRandomVector())