namespace ``2025``

open AdventOfCode2025.Day05
open Xunit

module ``Day 05``=
    let testInput1 = 
        [|
            "3-5"
            "10-14"
            "16-20"
            "12-18"
            ""
            "1"
            "5"
            "8"
            "11"
            "17"
            "32"
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(3, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(14L, puzzle2(testInput1))