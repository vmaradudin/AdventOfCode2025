namespace ``2025``

open AdventOfCode2025.Day12
open Xunit

module ``Day 12``=
    let testInput1 = 
        [|
        "0:"
        "###"
        "##."
        "##."
        ""
        "1:"
        "###"
        "##."
        ".##"
        ""
        "2:"
        ".##"
        "###"
        "##."
        ""
        "3:"
        "##."
        "###"
        "##."
        ""
        "4:"
        "###"
        "#.."
        "###"
        ""
        "5:"
        "###"
        ".#."
        "###"
        ""
        "4x4: 0 0 0 0 2 0"
        "12x5: 1 0 1 0 2 2"
        "12x5: 1 0 1 0 3 2"
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(3, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal("*", puzzle2(testInput1))