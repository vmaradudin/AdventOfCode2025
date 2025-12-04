namespace ``2025``

open AdventOfCode2025.Day03
open Xunit

module ``Day 03``=
    let testInput1 = 
        [|
          "987654321111111"
          "811111111111119"
          "234234234234278"
          "818181911112111"
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(357L, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(3121910778619L, puzzle2(testInput1))