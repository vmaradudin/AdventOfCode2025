namespace ``2025``

open AdventOfCode2025.Day06
open Xunit

module ``Day 06``=
    let testInput1 = 
        [|
            "123 328  51 64 "
            " 45 64  387 23 "
            "  6 98  215 314"
            "*   +   *   +  "
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(4277556L, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(3263827L, puzzle2(testInput1))