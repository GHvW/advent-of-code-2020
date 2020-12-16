// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open FSharp.Lib.Lib
open System.Collections.Immutable


[<EntryPoint>]
let main argv =
    printfn "Hello Advent of Code 2020"

    let path1 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-1-input.txt"
    let path2 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-2-input.txt"
    let path3 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-3-input.txt"
    let path4 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-4-input.txt"
    let path5 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-5-input.txt"
    let path6 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-6-input.txt"
    let path7 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-7-input.txt"
    let path8 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-8-input.txt"
    let path9 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-9-input.txt"
    let path10 = @"C:\Users\ghvw\projects\dotnet\advent-of-code-2020\day-10-input.txt"

    // ******* Day 1 *********
    File.ReadLines path1
    |> Seq.map Int32.Parse
    |> find2020Product
    |> Option.iter (printfn "%A")

    File.ReadLines path1
    |> Seq.map Int32.Parse
    |> find2020TripleProduct
    |> Option.iter (printfn "Day 1.2: %A")

    // ******* Day 2 *********
    File.ReadLines path2
    |> wrongValidPasswordCount
    |> printfn "Day 2.1: %A"

    File.ReadLines path2
    |> validPasswordCount
    |> printfn "Day 2.2: %A"

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
    File.ReadLines path6
    |> Seq.fold (fun result next ->
        if next = "" then
            []::result
        else
            match result with
            | x::xs -> (next::x)::xs) [[]]
    |> Seq.map uniqueAnswers
    |> Seq.sumBy (Set.count)
    |> printfn "Day 6.1: %A"

    File.ReadLines path6
    |> Seq.fold (fun result next ->
        if next = "" then
            []::result
        else
            match result with
            | x::xs -> (next::x)::xs) [[]]
    |> Seq.map matchingAnswers
    |> Seq.sumBy (Set.count)
    |> printfn "Day 6.2: %A"


    // ************* Day 7 ******************
    File.ReadLines path7
    |> Seq.map parseBagNode
    |> Map.ofSeq
    |> connectedToGold
    |> printfn "Day 7.1 %A"

    File.ReadLines path7
    |> Seq.map parseBagNode
    |> Map.ofSeq
    |> bagsInShinyGold
    |> printfn "Day 7.2 %A"

    // ********* Day 8 ************
    File.ReadLines path8
    |> Seq.map parseInstruction
    |> Seq.toArray
    |> runProgram
    |> printfn "Day 8.1 %A"

    File.ReadLines path8
    |> Seq.map parseInstruction
    |> Seq.toArray
    |> runProgramHealer
    |> printfn "Day 8.2 %A"

    // ************ Day 9 ***********
    File.ReadLines path9
    |> Seq.map (UInt64.Parse)
    |> findNoAdd 25 ImmutableQueue.Empty
    |> Option.iter (printfn "Day 9.1 %A")


    let seq =
        File.ReadLines path9
        |> Seq.map (UInt64.Parse) 

    findNoAdd 25 ImmutableQueue.Empty seq
    |> Option.bind (findWeakness seq)
    |> printfn "Day 9.2 %A"

    // ************** Day 10 ***************
    File.ReadLines path10
    |> Seq.map (Int32.Parse)
    |> Set.ofSeq
    |> parseJoltages
    |> Seq.groupBy (fun (vertex, _) -> vertex)
    |> Map.ofSeq
    |> traverseAdapters 0
    |> Seq.fold (fun (ones, threes) next ->
        match next with
        | (_, 1) -> (ones + 1, threes)
        | (_, 3) -> (ones, threes + 1)) (0, 1) // start off with one three since our rating is always three higher than our highest joltage
    |> (uncurry2 (*))
    |> printfn "Day 10.1 %A"


    //File.ReadLines path10
    //|> Seq.map (Int32.Parse)
    //[28
    // 33
    // 18
    // 42
    // 31
    // 14
    // 46
    // 20
    // 48
    // 47
    // 24
    // 23
    // 49
    // 45
    // 19
    // 38
    // 39
    // 11
    // 1
    // 32
    // 25
    // 35
    // 8
    // 17
    // 7
    // 9
    // 4
    // 2
    // 34
    // 10
    // 3]
    [16
     10
     15
     5
     1
     11
     7
     19
     6
     12
     4]
    |> Set.ofSeq
    |> parseJoltages
    |> Seq.groupBy (fun (vertex, _) -> vertex)
    |> Map.ofSeq
    |> findPermutationCount [0] 0
    |> printfn "Day 10.2 %A" 

    0 // return an integer exit code