// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open FSharp.Lib.Lib


[<EntryPoint>]
let main argv =
    printfn "Hello Advent of Code 2020"

    let path1 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt"
    let path2 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-2-input.txt"
    let path3 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-3-input.txt"
    let path4 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-4-input.txt"
    let path5 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-5-input.txt"

    // ******* Day 1 *********
    // File.ReadLines path1
    // |> Seq.map Int32.Parse
    // |> find2020Product
    // |> Option.iter (printfn "%A")

    //File.ReadLines path1
    //|> Seq.map Int32.Parse
    //|> find2020TripleProduct
    //|> Option.iter (printfn "Day 1.2: %A")


    // ******* Day 2 *********
    //File.ReadLines path2
    //|> wrongValidPasswordCount
    //|> printfn "Day 2.1: %A"

    //File.ReadLines path2
    //|> validPasswordCount
    //|> printfn "Day 2.2: %A"

    // ********* Day 3 ***********
    File.ReadLines path3
    |> parseMap
    |> encounteredTreesCount (3, 1)
    |> printfn "Day 3.1: %A"

    File.ReadLines path3
    |> parseMap
    |> treeCountsProduct [(1, 1); (3, 1); (5, 1); (7, 1); (1, 2)]
    |> printfn "Day 3.2: %A"

    // *************** Day 4 *****************
    File.ReadLines path4
    |> parseData
    |> validatePassports (Option.op_Implicit)
    |> Seq.length
    |> printfn "Day 4.1: %A"

    File.ReadLines path4
    |> parseData
    |> validatePassports passportValidator
    |> Seq.length
    |> printfn "Day 4.2: %A"

    // ****************** Day 5 ********************
    File.ReadLines path5
    |> Seq.map calcSeatId
    |> Seq.max
    |> printfn "Day 5.1: %A"

    // ****************** Day 6 **********************

    0 // return an integer exit code