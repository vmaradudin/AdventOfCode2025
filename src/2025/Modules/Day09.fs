namespace AdventOfCode2025

open Common.Types

module Day09 =
    let getRectangles (input:string array) =
        input |> Array.map(fun l -> l.Split(',') |> Array.map int |> fun a -> (a.[0],a[1]))
        |> fun a -> 
            a |> Array.groupBy snd |> fun a -> a |> Array.map(fun (x,ys) -> (x, ys |> Array.map fst |> Array.sort |> fun e -> (e[0],e[1]))) |> Array.sortBy fst
        |> fun a -> a

    let getLines ((currS,currE):(int*int),(acc:(int*(int*int)) array)) (x:int,(y1:int,y2:int)) =
        if acc.Length=0 then
            ((y1,y2),[|(x,(y1,y2))|])
        else
            (((if (currS = y1) then y2 else (min currS y1)), (if (currE = y2) then y1 else (max currE y2))), [|(x,((min currS y1),(max currE y2)))|])
        |> fun (newCurr, newLine) -> (newCurr, Array.append acc newLine)
    
    let isValid (lines:((int*(int*int)) array)) (x1,y1) (x2,y2)=
        lines |> Array.filter (fun (x,(ys,ye)) -> x >= (min x1 x2) && x <= (max x1 x2) && (y1<ys || y2<ys || y1>ye || y2>ye))
        |> Array.isEmpty
        
    let square ((x1,y1):(int*int)) ((x2,y2):(int*int)) lines =
        if isValid lines (x1,y1) (x2,y2) then
            int64(abs(x2 - x1) + 1) * int64(abs(y2 - y1) + 1)
        else
            0L
    
    let maxSquare (x1,(y11,y12)) (x2,(y21,y22)) lines =
        max (square (x1,y11) (x2,y22) lines) (square (x1,y12) (x2,y21) lines)
    
    let getMaxRectangle applyRules input =
        let rectangles= getRectangles input
        let lines = if applyRules then rectangles |> Array.fold getLines ((0,0),[||])|> snd else [||]
        [0..rectangles.Length - 2] |> List.map (fun i -> [i+1..rectangles.Length - 1] |> List.map (fun j -> maxSquare rectangles[i] rectangles[j] lines) |> List.max)
        |> List.max

    let puzzle1 (input:string array) = 
        input |> getMaxRectangle false

    let puzzle2 (input:string array) = 
        input |> getMaxRectangle true

    let Solution = (new Solution(9, puzzle1, puzzle2) :> ISolution).Execute