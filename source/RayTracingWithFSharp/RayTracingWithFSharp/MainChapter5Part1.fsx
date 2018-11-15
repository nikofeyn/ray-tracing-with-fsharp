#load "Vector.fs"
#load "PPM.fs"
#load "Ray.fs"

open System.IO
open RayTracingWithFSharp.Vector
open RayTracingWithFSharp.PPM
open RayTracingWithFSharp.Ray

let baseDirectory = __SOURCE_DIRECTORY__
let imagesDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\images"))
Directory.CreateDirectory imagesDirectory |> ignore

let nx = 200
let ny = 100

let ppm = PPM(ny, nx, Path.Combine(imagesDirectory, "main-chapter5-part1.ppm"))

let lowerLeftCorner = vector(-2.0, -1.0, -1.0)
let horizontal = vector(4.0, 0.0, 0.0)
let vertical = vector(0.0, 2.0, 0.0)
let origin = vector(0.0, 0.0, 0.0)

let hitSphere center radius (ray : Ray) =
    let AC = ray.Origin - center
    let a = dotProduct ray.Direction ray.Direction
    let b = 2.0 * (dotProduct ray.Direction AC)
    let c = (dotProduct AC AC) - (radius*radius)
    let discriminant = b*b - 4.0*a*c
    if discriminant < 0.0
    then -1.0
    else (-b - (sqrt discriminant))/(2.0*a)

let color (ray : Ray) =
    let t1 = hitSphere (vector(0.0, 0.0, -1.0)) 0.5 ray
    if t1 > 0.0
    then 0.5 .* (normalize ((pointAtParameter ray t1) - vector(0.0, 0.0, -1.0)) + vector(1.0, 1.0, 1.0))
    else
        let unitDirection = normalize ray.Direction
        let t = 0.5 * (unitDirection.Y + 1.0)
        ((1.0-t) .* vector(1.0, 1.0, 1.0)) + (t .* vector(0.5, 0.7, 1.0))

for j = ny-1 downto 0 do
    for i in 0 .. nx-1 do
        let u = (float i) / (float nx)
        let v = (float j) / (float ny)
        let r = {A = origin
                 B = lowerLeftCorner + (u .* horizontal) + (v .* vertical)}
        let col = roundVector (255.0 .* (color r))
        ppm.WriteVector col

ppm.Close()