module RayTracingWithFSharp.PPM

open System.IO
open Vector

type PPM(numberOfRows : int, numberOfColumns : int, filePath : string) =
    let ny = numberOfRows
    let nx = numberOfColumns
    let maxColor = 255
    let directory = Path.GetDirectoryName(filePath)
    do Directory.CreateDirectory(directory) |> ignore
    let file = File.CreateText(filePath)
    do
        file.WriteLine("P3")
        file.WriteLine(sprintf "%d %d" nx ny)
        file.WriteLine(sprintf "%d" maxColor)
    
    member this.WriteVector v =
        let rounded = roundVector v
        file.WriteLine(sprintf "%d %d %d" (int rounded.X) (int rounded.Y) (int rounded.Z))
    
    member this.Close () = file.Close()