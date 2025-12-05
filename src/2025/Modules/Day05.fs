namespace AdventOfCode2025

open Common.Types

module Day05 =
    let getRanges (acc:(int64*int64) array) (range:string) =
        let (s,e) = range.Split('-') |> Array.map int64 |> fun a -> (a[0], a[1])
        let overlappingRanges = acc |> Array.filter(fun (s1,e1) -> (s>=s1 && s<=e1) || (e>=s1 && e<=e1) || (e1>=s && e1<=e) || (s1>=s && s1<=e))
        match overlappingRanges with
        | [||] -> [|(s,e)|]
        | toMerge -> toMerge |> Array.append [|(s,e)|] |> Array.unzip ||> fun s e -> [|(s|>Array.min, e|>Array.max)|]
        |> Array.append (acc |> Array.except overlappingRanges)

    let parse (input:string array) =
        let ranges = input |> Array.takeWhile(fun s -> s <> "") |> Array.fold getRanges [||]
        let numbers = input |> Array.skipWhile(fun s -> s <> "") |> Array.tail |> Array.map int64
        (ranges, numbers)

    let puzzle1 (input:string array) = 
        input |> parse ||> fun ranges numbers ->
        numbers |> Array.filter(fun n -> ranges |> Array.exists(fun (s,e) -> n>=s && n<=e)) |> Array.length

    let puzzle2 (input:string array) = 
        input |> parse |> fst |> Array.sumBy(fun (s,e) -> e - s + 1L)

    let Solution = (new Solution(5, puzzle1, puzzle2) :> ISolution).Execute