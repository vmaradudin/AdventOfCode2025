namespace AdventOfCode2025

open Common.Types

module Day01 =
    let rotate (onlyStops, position, zeroes) (value:string) =
        match value[0], (value[1..] |> int) with
        | 'L', v -> (position - v) |> fun p -> (((p % 100) + 100) % 100, zeroes + (if p>0 then 0 else (abs(p - 100)/100)) - (if position=0 then 1 else 0))
        | 'R', v -> (position + v) |> fun p -> (p % 100, zeroes + p/100)
        | _ -> raise (System.ArgumentException("Invalid move"))
        |> fun (newPos, newZeroes) ->
            match onlyStops, newPos with
            | true, 0 -> (newPos, zeroes + 1)
            | true, _ -> (newPos, zeroes)
            | false, _ -> (newPos, newZeroes)
            |> fun (p, z) -> (onlyStops, p, z)

    let puzzle1 (input:string array) = 
        input 
        |> Array.fold rotate (true, 50, 0)
        |> fun (_,_,z) -> z

    let puzzle2 (input:string array) = 
        input
        |> Array.fold rotate (false, 50, 0)
        |> fun (_,_,z) -> z

    let Solution = (new Solution(1, puzzle1, puzzle2) :> ISolution).Execute