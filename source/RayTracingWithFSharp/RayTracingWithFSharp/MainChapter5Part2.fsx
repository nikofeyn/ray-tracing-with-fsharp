#load "Vector.fs"
#load "PPM.fs"
#load "Ray.fs"
#load "Hitable.fs"

open System
open System.IO
open RayTracingWithFSharp.Vector
open RayTracingWithFSharp.PPM
open RayTracingWithFSharp.Ray
open RayTracingWithFSharp.Hitable

let baseDirectory = __SOURCE_DIRECTORY__
let imagesDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\images"))
Directory.CreateDirectory imagesDirectory |> ignore

let nx = 200
let ny = 100

let ppm = PPM(ny, nx, Path.Combine(imagesDirectory, "main-chapter5-part2.ppm"))

let lowerLeftCorner = vector(-2.0, -1.0, -1.0)
let horizontal = vector(4.0, 0.0, 0.0)
let vertical = vector(0.0, 2.0, 0.0)
let origin = vector(0.0, 0.0, 0.0)

let color (ray : Ray) (world : Hitable) =
    match world.Hit(ray, 0.0, Double.MaxValue) with
    | Some record -> 0.5 .* (1.0 .+ record.Normal)
    | None ->
        let unitDirection = normalize ray.Direction
        let t = 0.5 * (unitDirection.Y + 1.0)
        ((1.0-t) .* vector(1.0, 1.0, 1.0)) + (t .* vector(0.5, 0.7, 1.0))

let world = HitableList [Sphere(vector(0.0,0.0,-1.0), 0.5);
                         Sphere(vector(0.0,-100.5,-1.0), 100.0)]

for j = ny-1 downto 0 do
    for i in 0 .. nx-1 do
        let u = (float i) / (float nx)
        let v = (float j) / (float ny)
        let r = {A = origin
                 B = lowerLeftCorner + (u .* horizontal) + (v .* vertical)}
        let p = pointAtParameter r 2.0
        let col = roundVector (255.0 .* (color r world))
        ppm.WriteVector col

ppm.Close()