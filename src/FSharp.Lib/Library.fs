﻿namespace FSharp.Lib

open System


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


    // *********************** Day 2 *************************************
    type PasswordData =
        { First: int
          Second: int 
          Character: char
          Password: string }

    let parseLine (line : string) : Option<PasswordData> =
        try
            let result = line.Split(" ") 
            let bounds = result.[0].Split("-")
            let character = result.[1].ToCharArray()
            let password = result.[2]

            Some({ First = Int32.Parse(bounds.[0]);
                   Second = Int32.Parse(bounds.[1]);
                   Character = character.[0];
                   Password = password })
        with
        | _ -> None


    let isWrongValidPassword (item : PasswordData) : bool =
        let count = 
            item.Password
            |> Seq.filter (fun character -> character = item.Character)
            |> Seq.length

        count >= item.First && count <= item.Second
            

    let wrongValidPasswordCount lines =
        lines
        |> Seq.choose parseLine
        |> Seq.filter isWrongValidPassword
        |> Seq.length


    let isValidPassword (data : PasswordData) : bool =
        try
            let f = data.First - 1
            let s = data.Second - 1
            let firstEq = data.Password.[f] = data.Character
            let secondEq = data.Password.[s] = data.Character
            (firstEq || secondEq) && (not (firstEq && secondEq))
        with
        | _ -> false


    let validPasswordCount : seq<string> -> int =
        Seq.choose parseLine
        >> Seq.filter isValidPassword
        >> Seq.length


