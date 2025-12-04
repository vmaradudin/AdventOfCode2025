namespace ``2025``

open AdventOfCode2025.Day01
open Xunit

module ``Day 01``=
    let testInput1 = 
        [|
          "L68"
          "L30"
          "R48"
          "L5"
          "R60"
          "L55"
          "L1"
          "L99"
          "R14"
          "L82"
        |]  
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(3, puzzle1 testInput1)
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(6, puzzle2 testInput1)