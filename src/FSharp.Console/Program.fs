// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open FSharp.Lib.Lib


[<EntryPoint>]
let main argv =
    printfn "Hello Advent of Code 2020"

    let path1 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt"
    let path2 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-2-input.txt"

    // ******* Day 1 *********
    // File.ReadLines path1
    // |> Seq.map Int32.Parse
    // |> find2020Product
    // |> Option.iter (printfn "%A")

    File.ReadLines path1
    |> Seq.map Int32.Parse
    |> find2020TripleProduct
    |> Option.iter (printfn "Day 1.2: %A")


    // ******* Day 2 *********
    //File.ReadLines path2
    //|> wrongValidPasswordCount
    //|> printfn "Day 2.1: %A"

    File.ReadLines path2
    |> validPasswordCount
    |> printfn "Day 2.2: %A"

    0 // return an integer exit code