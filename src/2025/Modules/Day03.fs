namespace AdventOfCode2025

open Common.Types

module Day03 =
    let rec findMax length result row=
        if length = 0 then result
        else
        row |> Array.take (row.Length - length + 1) |> Array.maxBy snd
        |> fun (i,v) -> findMax (length - 1) (result*10L + (v |> int64)) (row |> Array.skipWhile (fun (ii,_) -> ii <= i))
    
    let findJoltage length row =
        row |> Array.mapi (fun i v -> (i,v)) |> (findMax length 0L)

    let puzzle1 (input:string array) = 
        input |> Array.map (fun s -> s.ToCharArray() |> Array.map (fun a -> a|> string |> int))
        |> Array.sumBy (findJoltage 2)

    let puzzle2 (input:string array) = 
        input |> Array.map (fun s -> s.ToCharArray() |> Array.map (fun a -> a|> string |> int))
        |> Array.sumBy (findJoltage 12)

    let Solution = (new Solution(3, puzzle1, puzzle2) :> ISolution).Execute