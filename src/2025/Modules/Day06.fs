namespace AdventOfCode2025

open Common.Types

module Day06 =
    let rec parse acc startPos direction (input:string array)=
        if startPos >=input[0].Length then acc
        else
        let next = 
            match input[input.Length-1].IndexOfAny([|'+';'*'|],startPos+1) with
            |x when x>=0 -> x
            |_ -> input[0].Length
        
        let operation = input[input.Length-1][startPos]
        let numbers = 
         match direction with
         |"H" -> input |> Array.take (input.Length-1) |> Array.map (fun l-> l.Substring(startPos, next - startPos).Trim())
         |"V" -> [|startPos..next-1|] |> Array.map(fun i -> 
                 input|>Array.take (input.Length-1)
                 |> Array.map(fun l -> l[i] |> string)|>String.concat ""
                 |> fun a -> a.Trim())
         |_ -> failwith "Invalid direction"
         |> Array.filter (fun a -> a<>"")|> Array.map int64
        
        parse (acc|>Array.append [|(operation,numbers)|]) next direction input
    
    let calc (operation, numbers) =
        match operation with
        |'+' -> (+), 0L
        |'*' -> (*), 1L
        |_ -> failwith "unexpected operation"
        |> fun (op,acc) -> numbers |> Array.fold op acc 
    
    let puzzle1 (input:string array) = 
        input |> parse [||] 0 "H" |> Array.sumBy calc


    let puzzle2 (input:string array) = 
        input |> parse [||] 0 "V" |> Array.sumBy calc

    let Solution = (new Solution(6, puzzle1, puzzle2) :> ISolution).Execute