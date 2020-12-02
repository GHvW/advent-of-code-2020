namespace FSharp.Lib

module Lib =


    let uncurry2 f (x, y) = f x y

            
    let optionPure x = Some x


    let (>=>) f g arg =
        f arg
        |> Option.bind g


    // ************************** Day 1 ************************************************
    let rec findSumNums (set : Set<int>) (sum : int) (ns : seq<int>) : Option<int * int> =
        if Seq.isEmpty ns then
            None
        else 
            let next = Seq.head ns
            let reqN = sum - next

            if Set.contains reqN set then
                Some(reqN, next)
            else
                findSumNums (Set.add next set) sum (Seq.tail ns)
            

    let find2020Product : seq<int> -> Option<int> = 
        (findSumNums Set.empty 2020) >=> ((uncurry2 (*)) >> optionPure)
        

    let rec findTriple (set : Set<int>) (sum : int) (ns : seq<int>) : Option<int * int * int> =
        if Seq.isEmpty ns then
            None
        else
            let next = Seq.head ns
            let reqN = sum - next

            match findSumNums set reqN set with
            | Some(x, y) -> Some(x, y, next)
            | None -> findTriple (Set.add next set) sum (Seq.tail ns)
            

    let find2020TripleProduct : seq<int> -> Option<int> =
        (findTriple Set.empty 2020) >=> ((fun (x, y, z) -> x * y * z) >> optionPure)

    // ********************** End Day 1 *************************************************


