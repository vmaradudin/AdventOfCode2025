namespace AdventOfCode2025

open Common.Types

module Day12 =
    let folder (acc:string array array) (input:string) =
        match input with
        | "" -> Array.append acc [|[||]|]
        | l when l.EndsWith(":") -> acc
        | l -> Array.append acc[0..acc.Length-2] [|Array.append acc[acc.Length-1] [|l|]|]
        
    let parse (acc: string array array)=   
        acc
        |> fun a -> a[0..a.Length-2], a[a.Length-1]
        |> fun (presents, area) ->
            presents |> Array.map (fun present -> present|> Array.map (fun line -> line.ToCharArray()|>Array.map (function |'#' -> true | _ -> false))),
            area |> Array.map (fun a -> a.Split([|' ';'x';':'|],System.StringSplitOptions.RemoveEmptyEntries) |> fun arr -> ((int(arr[0]), int(arr[1]))), arr[2..]|> Array.map(int)) 
    
    /// Checks if the presents can fit in the area at all (ignores shape and arrangement)
    let fit (presents: bool array array array) ((areaSize: int*int), (presentsCount: int array)) =
        let squares =
            presents |> Array.map (fun p -> p|> Array.collect id |> Array.sumBy (function | true -> 1 | false -> 0))
        if (areaSize|> fun (i,j) -> i*j) < ((Array.map2 (fun a b -> a*b) presentsCount squares)|> Array.sum) then
            false
        else
            true

    let puzzle1 (input:string array) = 
        let (presents, areas) = input|> Array.fold folder [|[||]|] |> parse
        areas |> Array.filter (fit presents) |> Array.length

    let puzzle2 (input:string array) = 
        "*"

    let Solution = (new Solution(12, puzzle1, puzzle2) :> ISolution).Execute