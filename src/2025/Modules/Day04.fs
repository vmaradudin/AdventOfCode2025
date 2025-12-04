namespace AdventOfCode2025

open Common.Types

module Day04 =
    let step (input:string array) =
        input
        |> Array.mapi (fun x s -> 
            s|>String.mapi (fun y c -> 
                match c with
                | '@' -> 
                    ([|x-1..x+1|],[|y-1..y+1|])||> Array.allPairs 
                    |> Array.sumBy(function | (i,j) when i>=0 && j>=0 && i<input.Length && j<s.Length && ((i,j) <> (x,y)) && input[i][j]='@' -> 1 | _ -> 0)
                    |> function | count when count<4 -> 'x' | _ -> c
                | _ -> c))

    let rec work input =
        input |> step |> function
        | result when input = result -> input
        | result -> result |> work

    let countX (input:string array) =
        input |> Array.sumBy(Seq.sumBy(function |'x' -> 1 |_ -> 0))

    let puzzle1 (input:string array) = 
        input |> step |> countX

    let puzzle2 (input:string array) = 
        input |> work |> countX

    let Solution = (new Solution(4, puzzle1, puzzle2) :> ISolution).Execute