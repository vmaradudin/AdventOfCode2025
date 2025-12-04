namespace AdventOfCode2025

open Common.Types

module Day02 =
    let split (s1:string) (s2:string) =
        if s1.Length = s2.Length then
            [|s1, s2|]
        else
            Array.init (s2.Length - s1.Length) (fun i -> [|"".PadLeft(s1.Length + i, '9'); "1".PadRight(s1.Length + i + 1, '0')|])
            |> Array.collect id
            |> Array.append [|s1|]
            |> fun arr -> Array.append arr [|s2|]
            |> Array.chunkBySize 2
            |> Array.map (fun pair -> pair[0], pair[1])

    let rec pref prefixLength (acc:string[]) (s:string, e:string) =
        if prefixLength = 0 || s.Length % prefixLength <> 0 then [||]
        else
        if acc.Length = 0 || acc[0].Length = s.Length then acc|> Array.map int64 |> Array.sort
        else
        match acc[0].Length with
        | currentLength when currentLength < prefixLength -> 
            acc |> Array.collect (fun a -> [|0L..9L|]|> Array.map (fun n -> (if a="" then 0L else int64(a))*10L + n)) 
            |> Array.filter (fun v -> v >= int64(s.Substring(0, (currentLength + 1))) && v <= int64(e.Substring(0, (currentLength + 1))))
            |> Array.map string
        | currentLength ->
            acc |> Array.map (fun a -> a + a.Substring(0,prefixLength))
            |> Array.filter (fun v -> int64(v) >= int64(s.Substring(0, (currentLength + prefixLength))) && int64(v) <= int64(e.Substring(0, (currentLength + prefixLength))))
        |> fun newAcc -> pref prefixLength newAcc (s,e)

    let puzzle1 (input:string array) = 
        input 
        |> Array.head |> fun s -> s.Split ',' |> Array.collect (fun x -> x.Split '-' |> fun a -> split a[0] a[1])
        |> Array.collect (fun a -> if (a|>fst).Length % 2 = 0 then pref ((a|>fst).Length/2) [|""|] a else [||])
        |> Array.distinct |> Array.sum

    let puzzle2 (input:string array) = 
        input 
        |> Array.head |> fun s -> s.Split ',' |> Array.collect (fun x -> x.Split '-' |> fun a -> split a[0] a[1])
        |> Array.collect (fun a -> [|1 .. fst(a).Length/2 |] |> Array.collect(fun b -> pref b [|""|] a))
        |> Array.distinct |> Array.sum

    let Solution = (new Solution(2, puzzle1, puzzle2) :> ISolution).Execute