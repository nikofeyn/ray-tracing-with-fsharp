#load "Vector.fs"
#load "PPM.fs"
#load "Ray.fs"
#load "Hitable.fs"
#load "Camera.fs"
#load "DiffuseMaterial.fs"

open System
open System.IO
open RayTracingWithFSharp.Vector
open RayTracingWithFSharp.PPM
open RayTracingWithFSharp.Ray
open RayTracingWithFSharp.Hitable
open RayTracingWithFSharp.Camera
open RayTracingWithFSharp.DiffuseMaterial

let baseDirectory = __SOURCE_DIRECTORY__
let imagesDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\images"))
Directory.CreateDirectory imagesDirectory |> ignore

let nx = 200
let ny = 100
let ns = 100

let rand = Random(101)

let ppm = PPM(ny, nx, Path.Combine(imagesDirectory, "main-chapter7.ppm"))

let rec color (ray : Ray) (world : Hitable) =
    match world.Hit(ray, 0.0, Double.MaxValue) with
    | Some record ->
        let target = record.P + record.Normal + randomInUnitSphere(rand)
        0.5 .* (color {A = record.P; B = target - record.P} world)
    | None ->
        let unitDirection = normalize ray.Direction
        let t = 0.5 * (unitDirection.Y + 1.0)
        ((1.0-t) .* vector(1.0, 1.0, 1.0)) + (t .* vector(0.5, 0.7, 1.0))

let world = HitableList [Sphere(vector(0.0,0.0,-1.0), 0.5);
                         Sphere(vector(0.0,-100.5,-1.0), 100.0)]

let camera = Camera()



for j = ny-1 downto 0 do
    for i = 0 to nx-1 do
        let mutable col = vector(0.0, 0.0, 0.0)
        for s = 0 to ns-1 do
            let u = (float i + rand.NextDouble()) / (float nx)
            let v = (float j + rand.NextDouble()) / (float ny)
            let r = camera.GetRay(u,v)
            let p = pointAtParameter r 2.0
            col <- col + (color r world)
        col <- col ./ (float ns)
        col <- roundVector (255.0 .* col)
        ppm.WriteVector col

ppm.Close()