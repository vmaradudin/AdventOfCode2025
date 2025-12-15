namespace AdventOfCode2025

open Common.Types
open System.Text.RegularExpressions
open Microsoft.Z3

module Day10 =
    let private parseLine (line: string) =
        let indicatorPattern = @"\[[.#]+\]"
        let buttonPattern = @"\(([\d,]+)\)"
        let joltagePattern = @"\{([\d,]+)\}"
        let indicatorMatch = Regex.Match(line, indicatorPattern)
        let indicators = indicatorMatch.Value.ToCharArray() |> Array.tail |> Array.take (indicatorMatch.Value.Length - 2) |> Array.map (function |'#'->1 |_->0)
        let buttonMatches = Regex.Matches(line, buttonPattern)
        let buttons = [| for m in buttonMatches -> m.Groups.[1].Value.Split(',') |> Array.map int |]
        let joltageMatch = Regex.Match(line, joltagePattern)
        let joltage = joltageMatch.Groups.[1].Value.Split(',') |> Array.map int
        indicators, buttons, joltage
    
    let toggle(button:int array) (state:int array) =
        Array.map2 (fun b s -> if b=s then 0 else 1) button state
    
    let rec toggleUntil (states:((int array)*int) array) (target:int array) (buttons:int array array) =
        match states|> Array.tryFind (fun (s,_) -> s = target) with
        |Some (_, presses) -> presses
        |_ ->
            let newStates =
                states
                |> Array.collect (fun (s, presses) -> buttons |> Array.map (fun b -> (toggle b s, presses + 1)))
                |> Array.groupBy fst
                |> Array.map (fun (s, group) -> (group |> Array.minBy snd))
            toggleUntil newStates target buttons
        
    let minButtonPresses (buttons: int[] array) (joltage: int[]) =
        use context = new Context()
        let indexedButtons = buttons |> Array.indexed
        let variables = indexedButtons|> Array.unzip |> fst|> Array.map (fun i -> context.MkIntConst("buttonClicks_" + string(i)))
        let constraints =
            [|
            variables|> Array.map (fun v -> context.MkGe(v, context.MkInt(0)))
            joltage |> Array.mapi (fun i jValue -> 
                indexedButtons
                |> Array.filter(fun (_, bList) -> Array.contains i bList)
                |> Array.map (fun (idx, _) -> variables[idx]:> ArithExpr)
                |> context.MkAdd |> fun s -> context.MkEq(s, context.MkInt(jValue)))
            |] |> Array.collect id

        let opt = context.MkOptimize()
        opt.Add(constraints) |> ignore
        variables |> Array.map (fun v -> v :> ArithExpr) |> context.MkAdd |> opt.MkMinimize|> ignore

        match opt.Check() with
        | Status.SATISFIABLE ->
            variables
            |> Array.sumBy (fun v ->
                match opt.Model.Evaluate(v, true) with
                | :? IntNum as intNum -> intNum.Int
                | _ -> failwith "Unexpected variable value type.")
        | _ -> failwith "No solution found."

    let puzzle1 (input: string array) =
        let solutions = input |> Array.map (parseLine) |> Array.map (fun (i,b,_) -> 
            let buttonsAsMask = b|> Array.map (fun btn -> [|for idx in 0..(i.Length-1) -> if (btn |> Array.contains idx) then 1 else 0|])
            toggleUntil [|(Array.create i.Length 0),0|] i buttonsAsMask)
        solutions |> Array.sum

    let puzzle2 (input: string array) =
        input |> Array.map(fun line -> line |> parseLine |> fun (_,b,j) -> minButtonPresses b j) |> Array.sum

    let Solution = (new Solution(10, puzzle1, puzzle2) :> ISolution).Execute