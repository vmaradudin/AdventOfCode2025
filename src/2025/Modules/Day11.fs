namespace AdventOfCode2025

open Common.Types

module Day11 =
    let parse (input:string array) =
        input
        |> Array.map (fun line -> line.Split([|':';' '|],System.StringSplitOptions.RemoveEmptyEntries)|> fun a -> (a|>Array.head, a|>Array.tail))
        |> Map.ofArray

    let getWaysNumber (start:string) (target:string) (mustVisit:string array) (map: Map<string, string array>)=
        let mutable paths = new System.Collections.Generic.Dictionary<string*int, int64>()
        let mustVisitMaskApply = 
            mustVisit 
            |> Array.fold (fun (newArr,pow) n -> newArr |> Array.append [|(n,(fun (flag:int) -> flag + pow))|], pow*10) ([||],1)
            |> fst |> Map.ofArray
            |> fun a -> fun (value:string,mask:int)-> match a.TryFind(value) with | Some(f) -> f(mask) | None -> mask  
            
        let rec findWays actual:(string*int*int64) array =
            if actual |> Array.isEmpty then
                actual
            else
            actual 
            |> Array.choose (fun (s,mask,pow) ->
                match map.TryFind(s) with 
                |Some(ns) -> ns|> Array.map (fun l -> l, mustVisitMaskApply(l,mask), pow) |> Some
                |_ -> None)
            |> Array.collect id
            |> Array.groupBy (fun (a,b,pow) -> (a,b))
            |> Array.map (fun ((a,b),v) -> (a,b, v |> Array.sumBy (fun (_,_,pow) -> pow)))
            |> fun a -> 
                a |> Array.iter(fun (a,b, c) -> match paths.ContainsKey(a,b) with | true -> paths.[(a,b)] <- (paths[(a,b)] + c) | false -> paths[(a,b)] <- c)
                findWays (a)
        findWays [|(start,0,1L)|]|> ignore
        paths[(target, mustVisit |> Array.fold(fun acc _ -> acc*10 + 1) 0)]
   
    let puzzle1 (input:string array) = 
        input|> parse |> getWaysNumber "you" "out" [||]

    let puzzle2 (input:string array) = 
        input|> parse |> getWaysNumber "svr" "out" [|"dac";"fft"|]

    let Solution = (new Solution(11, puzzle1, puzzle2) :> ISolution).Execute