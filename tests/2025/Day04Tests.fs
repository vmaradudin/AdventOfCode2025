namespace ``2025``

open AdventOfCode2025.Day04
open Xunit

module ``Day 04``=
    let testInput1 = 
        [|
          "..@@.@@@@."
          "@@@.@.@.@@"
          "@@@@@.@.@@"
          "@.@@@@..@."
          "@@.@@@@.@@"
          ".@@@@@@@.@"
          ".@.@.@.@@@"
          "@.@@@.@@@@"
          ".@@@@@@@@."
          "@.@.@@@.@."
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(13, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(43, puzzle2(testInput1))