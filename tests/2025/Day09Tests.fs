namespace ``2025``

open AdventOfCode2025.Day09
open Xunit

module ``Day 09``=
    let testInput1 = 
        [|
        "7,1"
        "11,1"
        "11,7"
        "9,7"
        "9,5"
        "2,5"
        "2,3"
        "7,3"
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(50L, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(24L, puzzle2(testInput1))