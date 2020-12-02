// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open FSharp.Lib.Lib


[<EntryPoint>]
let main argv =
    printfn "Hello Advent of Code 2020"

    let path = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\input.txt"

    // ******* Day 1 *********
    // File.ReadLines path
    // |> Seq.map Int32.Parse
    // |> find2020Product
    // |> Option.iter (printfn "%A")

    // ******* Day 2 *********
    File.ReadLines path
    |> Seq.map Int32.Parse
    |> find2020TripleProduct
    |> Option.iter (printfn "%A")

    0 // return an integer exit code