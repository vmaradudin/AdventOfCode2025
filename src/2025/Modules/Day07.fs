namespace AdventOfCode2025

open Common.Types

module Day07 =
    let rec calculate ((count: int),(beams:(int*int64) array)) (row:string) =
        if beams.Length = 0 then (0,[|row.IndexOf('S'),1L|])
        else
        ((count + (beams |> Array.sumBy(fun (pos,pow) -> match row[pos] with |'^' -> 1 |_ -> 0))),
        (beams |> Array.collect (fun (pos,pow) -> match row[pos] with |'^' -> [|(pos-1,pow);(pos+1,pow)|] |_ -> [|(pos,pow)|])
        |> Array.groupBy fst |> Array.map (fun (pos,arr) -> pos, arr |>Array.sumBy snd)))

    let puzzle1 (input:string array) = 
        input |> Array.fold calculate (0,[||])|> fst

    let puzzle2 (input:string array) = 
        input |> Array.fold calculate (0,[||]) |> snd |> Array.sumBy snd

    let Solution = (new Solution(7, puzzle1, puzzle2) :> ISolution).Execute