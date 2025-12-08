namespace AdventOfCode2025

open Common.Types

module Day08 =
    let getPoint (p:string) = 
        p.Split(',') |> Array.map float|> fun arr -> (arr[0],arr[1],arr[2])

    let getDistance ((x1,y1,z1),(x2,y2,z2))=
        ((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2) + (z1-z2)*(z1-z2))|> float |> sqrt
    
    let getDistances (input:(int*(float*float*float)) array) =
        input |> Array.collect (fun (i1,p1) -> input |> Array.skip (i1+1) |> Array.map (fun (i2,p2) -> ((i1,i2), getDistance(p1,p2))))
        |> Array.sortBy snd
        |> Array.map fst

    let getCircuits (circuits:int Set array) (p1,p2) =
        circuits |> Array.filter (fun s -> s.Contains(p1) || s.Contains(p2))
        |> function
        | [||] -> circuits |> Array.append [| Set.ofList [p1;p2] |]
        | sets -> (circuits|> Array.except sets) |> Array.append [|(sets |> Set.unionMany |> Set.add p1 |> Set.add p2)|]
    
    let solvePuzzle1 num (input:string array) =
        input|> Array.mapi (fun i v ->(i, getPoint v))
        |> getDistances
        |> Array.take num
        |> Array.fold getCircuits [||]
        |> Array.sortByDescending (fun s -> s.Count)
        |> Array.take 3
        |> Array.fold (fun acc s -> acc * s.Count) 1

    let rec getLastConnection num (circuits:int Set array) index (input:(int*int)array)  =
        let nc = getCircuits circuits (input[index])
        if (nc.Length = 1) && nc[0].Count = num then
            input[index]
        else
            getLastConnection num nc (index+1) input

    let puzzle1 (input:string array) = 
        input |> solvePuzzle1 1000

    let puzzle2 (input:string array) = 
        input |> Array.mapi(fun i v -> (i,getPoint v)) |> getDistances
        |> getLastConnection input.Length Array.empty 0
        |> fun (i1,i2) -> [|input[i1]; input[i2]|] |> Array.fold (fun acc v -> (v|> getPoint |> fun (x,_,_)-> x |> int64) * acc) 1L

    let Solution = (new Solution(8, puzzle1, puzzle2) :> ISolution).Execute