namespace FSharp.Lib

open System


module Lib =


    let uncurry2 f (x, y) = f x y

            
    let optionPure x = Some x // Option.op_Implicit instead?


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


    // ******************** Day 3 **********************
    let parseMap (lines : seq<string>) : char[][] =
        lines
        |> Seq.map (fun line -> line.ToCharArray())
        |> Seq.toArray


    let nextLocation (width : int) (x, y) (currentX, currentY) : int * int =
        let wrap = (currentX + x) - width

        let newXPosition = 
            if wrap < 0 then
                currentX + x
            else
                wrap

        (newXPosition, currentY + y)
        

    let encounteredTreesCount ((x, y) : int * int) (map : char[][]) : int =
        let findNextLocation = nextLocation (map.[0].Length) (x, y)
        let rec loop count (currentX, currentY) =
            if currentY > map.Length - 1 then
                count
            else
                let newCount = 
                    if map.[currentY].[currentX] = '#' then 
                        count + 1 
                    else 
                        count
                loop newCount (findNextLocation (currentX, currentY))
                
        loop 0 (0, 0)


    let treeCountsProduct (slopes : List<int * int>) (map : char[][]) : UInt64 =
        slopes
        |> Seq.map (fun it -> encounteredTreesCount it map)
        |> Seq.fold (fun total next -> total * (uint64 next)) 1UL
        

    // ***************** Day 4 ***********************************
    let parseData (lines : seq<string>) : List<List<string[]>> =
        lines
        |> Seq.fold (fun result line -> 
            match line with
            | "" -> []::result
            | _ ->
                let newHead = line::(List.head result)
                newHead::(List.tail result)) [[]]
        |> List.map (fun list ->
            list
            |> List.collect (fun line -> 
                line.Trim().Split(" ")
                |> Array.map (fun item -> item.Split(":"))
                |> Array.toList))


    let validatePassports (validator : List<string[]> -> Option<List<string[]>>) (passportInfos : List<List<string[]>>) =
        passportInfos
        |> Seq.choose (fun data ->
            if data.Length = 8 then
                validator data
            else if data.Length = 7 then
                let hasCID = // this could be better
                    data
                    |> Seq.exists (fun [|field; _|] -> field = "cid")

                if hasCID then 
                    None 
                else 
                    validator data
            else
                None)


    let validItem (min, max) num = 
        num >= min && num <= max


    let numeric = validItem ('0', '9')


    let validGroup group item = 
        Set.contains item group
        

    let validEyeColor = validGroup (Set.ofList ["amb"; "blu"; "brn"; "gry"; "grn"; "hzl"; "oth";])


    let validBirthYear = validItem (1920, 2002)


    let validIssueYear = validItem (2010, 2020)


    let validExpirationYear = validItem (2020, 2030)


    let validHairColor hairColor =
        match Seq.head hairColor with
        | '#' ->
            Seq.tail hairColor
            |> Seq.forall (fun c -> (numeric c) || (validItem ('a', 'f') c))
        | _ -> false


    let validPID (pid : string) : bool =
        if pid.Length <> 9 then
            false
        else
            pid 
            |> Seq.forall numeric


    let splitHeight (height : string) =
        try
            let i = 
                height 
                |> Seq.findIndex (fun c -> not (numeric c))

            (height.[..i - 1], height.[i..])
        with
        | _ -> ("", "")


    let validHeight measure =
        match measure with
        | (height, "cm") -> 
            validItem (150, 193) (Int32.Parse(height))
        | (height, "in") ->
            validItem (59, 76) (Int32.Parse(height))
        | _ -> false


    let passportValidator (data : List<string[]>) =
        let isValid =
            data
            |> Seq.forall (fun field ->
                match field with
                | [|"pid"; pid|] -> validPID pid
                | [|"byr"; birthYear |] -> validBirthYear (Int32.Parse(birthYear))
                | [|"iyr"; issueYear |] -> validIssueYear (Int32.Parse(issueYear))
                | [|"eyr"; expirationYear |] -> validExpirationYear (Int32.Parse(expirationYear))
                | [|"hgt"; height |] -> (splitHeight >> validHeight) height
                | [|"hcl"; hairColor |] -> validHairColor hairColor
                | [|"ecl"; eyeColor |] -> validEyeColor eyeColor
                | [|"cid"; _ |] -> true
                | _ -> false)

        if isValid then Some(data) else None


    // ***************** Day 5 ****************************
    let nextPosition upperChar id ((min, max) : double * double) : double * double =
        let difference = max - min
        if id = upperChar then
            if difference = 1.0 then
                (max, max)
            else
                (min + (Math.Round (difference / 2.0)), max)
        else
            (min, min + Math.Truncate(difference / 2.0))


    let row rowPartition =
        rowPartition
        |> Seq.fold (fun result c -> nextPosition 'B' c result) (0.0, 127.0)
        |> fst


    let column columnPartition =
        columnPartition
        |> Seq.fold (fun result c -> nextPosition 'R' c result) (0.0, 7.0)
        |> fst


    let calcSeatId (spacePartition : string) : double =
        let row' = row spacePartition.[..7 - 1]
        let column' = column spacePartition.[7..]
        row' * 8.0 + column'


    // ***************** Day 6 ***************************
    let uniqueAnswers group =
        group
        |> Seq.fold (fun result answer ->
            answer
            |> Seq.fold (fun result' c -> Set.add c result') result) Set.empty


    let matchingAnswers group =
        group
        |> Seq.map (fun answer ->
            answer
            |> Seq.fold (fun result c -> Set.add c result) Set.empty)
        |> Seq.reduce (fun result set -> Set.intersect set result)

    // *************** Day 7 *************************
    let parseBagNode (line : string) =
        let kvs = line.Split(" contain ")
        //|> Seq.zip (seq { 0 .. line.Length })
        let keyArr = kvs.[0].Split(" ");
        let valsArr = kvs.[1].Split(", ")
        let key = (keyArr.[0], keyArr.[1])
        let vs =
            valsArr
            |> Seq.choose (fun line -> 
                let child = 
                    line.Split(" ")
                    |> Seq.filter (fun word -> word <> "bags" && word <> "bag" && word <> "bag." && word <> "bags.") 
                    |> Seq.toArray

                if child.[0] = "no" then
                    None
                else
                    Some((child.[1], child.[2]), Int32.Parse(child.[0])))
            |> Map.ofSeq

        (key, vs)


    let rec dfs visited stack adjacency : seq<string * string> = seq {
        match stack with
        | [] -> ()
        | x::xs ->
            yield x

            let (visited', stack') =
                Map.find x adjacency
                |> Map.fold (fun (v, s) k _ -> 
                    if Set.contains k visited then
                        (v, s)
                    else
                        let v' = Set.add k v
                        let s' = k::s
                        (v', s')) (visited, xs)

            yield! dfs visited' stack' adjacency }

        
    // make this better
    let connectedToGold adjacency =
        adjacency
        |> Map.toSeq
        |> Seq.filter (fun (k, _) -> k <> ("shiny", "gold"))
        |> Seq.map (fun (k, _) ->
            dfs (Set.ofList [k]) [k] adjacency
            |> Seq.contains ("shiny", "gold")) 
        |> Seq.fold (fun sum b -> if b = true then sum + 1 else sum) 0



    let rec totalBags (multiplier : int) (bag : string * string) (adjacency : Map<string * string, Map<string * string, int>>) : int =
        Map.find bag adjacency
        |> Map.toSeq
        |> Seq.fold (fun total (k, v) ->
            total + totalBags (multiplier * v) k adjacency) multiplier


    let bagsInShinyGold adjacency =
        Map.find ("shiny", "gold") adjacency
        |> Map.toSeq
        |> Seq.map (fun (k, v) -> totalBags v k adjacency)
        |> Seq.sum


    // ************** Day 8 **********************
    let parseInstruction (instruction : string) =
        let words = instruction.Split(" ")
        let positiveNegative = words.[1].[0]
        let n = words.[1].[1..]
        (words.[0], (positiveNegative, Int32.Parse(n)))

    // part 1
    let rec runProgramWithTrace instructionsRun pc (index : int) (program : (string * (char * int))[]) : int =
        if Set.contains index instructionsRun then
            pc
        else
            let instructionsRun' = Set.add index instructionsRun
            match program.[index] with
            | ("acc", x) -> 
                match x with
                | ('+', n) -> runProgramWithTrace instructionsRun' (pc + n) (index + 1) program
                | (_, n) -> runProgramWithTrace instructionsRun' (pc - n) (index + 1) program
            | ("jmp", x) ->
                match x with
                | ('+', n) -> runProgramWithTrace instructionsRun' pc (index + n) program
                | (_, n) ->  runProgramWithTrace instructionsRun' pc (index - n) program // ('-', n)
            | _ -> runProgramWithTrace instructionsRun' pc (index + 1) program // (nop, _) TODO make a DU if time

            
    let runProgram = runProgramWithTrace Set.empty 0 0
        

    // part 2
    let rec runProgramWithTrace2 instructionsRun pc (index : int) (program : (string * (char * int))[]) : Option<int> =
        if Set.contains index instructionsRun then
            None
        else if index > Array.length program then // + 1 - 1 = 0
            None
        else if index = Array.length program then
            Some(pc)
        else
            let instructionsRun' = Set.add index instructionsRun
            match program.[index] with
            | ("acc", x) -> 
                match x with
                | ('+', n) -> runProgramWithTrace2 instructionsRun' (pc + n) (index + 1) program
                | (_, n) -> runProgramWithTrace2 instructionsRun' (pc - n) (index + 1) program
            | ("jmp", x) ->
                match x with
                | ('+', n) -> runProgramWithTrace2 instructionsRun' pc (index + n) program
                | (_, n) ->  runProgramWithTrace2 instructionsRun' pc (index - n) program // ('-', n)
            | _ -> runProgramWithTrace2 instructionsRun' pc (index + 1) program // (nop, _) TODO make a DU if time


    let rec runProgramWithHeals instructionsRun pc (index : int) (program : (string * (char * int))[]) : int =
        let set = Set.add index instructionsRun
        match program.[index] with
        | ("acc", x) -> 
            match x with
            | ('+', n) -> runProgramWithHeals set (pc + n) (index + 1) program
            | (_, n) -> runProgramWithHeals set (pc - n) (index + 1) program
        | ("jmp", x) ->
            match runProgramWithTrace2 set pc (index + 1) program with
            | None -> 
                match x with
                | ('+', n) -> runProgramWithHeals set pc (index + n) program
                | (_, n) -> runProgramWithHeals set pc (index - n) program
            | Some result -> result
        | (_, x) ->
            match x with
            | ('+', n) ->
                match runProgramWithTrace2 set pc (index + n) program with
                | None -> runProgramWithHeals set pc (index + 1) program
                | Some result -> result
            | (_, n) -> 
                match runProgramWithTrace2 set pc (index - n) program with
                | None -> runProgramWithHeals set pc (index + 1) program
                | Some result -> result
        

    let runProgramHealer = runProgramWithHeals Set.empty 0 0
