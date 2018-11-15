#load "Vector.fs"
#load "PPM.fs"

open System.IO
open RayTracingWithFSharp.Vector
open RayTracingWithFSharp.PPM

let baseDirectory = __SOURCE_DIRECTORY__
let imagesDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\images"))
Directory.CreateDirectory imagesDirectory |> ignore

let calculate x y = (float x)/(float y) * 255.

let nx = 200
let ny = 100

let ppm = PPM(ny, nx, Path.Combine(imagesDirectory, "main-chapter2.ppm"))

for j = ny-1 downto 0 do
    for i in 0 .. nx-1 do
        ppm.WriteVector {X = calculate i nx
                         Y = calculate j ny
                         Z = round (255. * 0.2)}

ppm.Close()