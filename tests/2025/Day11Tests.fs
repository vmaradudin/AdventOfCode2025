namespace ``2025``

open AdventOfCode2025.Day11
open Xunit

module ``Day 11``=
    let testInput1 = 
        [|
        "aaa: you hhh"
        "you: bbb ccc"
        "bbb: ddd eee"
        "ccc: ddd eee fff"
        "ddd: ggg"
        "eee: out"
        "fff: out"
        "ggg: out"
        "hhh: ccc fff iii"
        "iii: out"
        |]
    
    let testInput2 = 
        [|
        "svr: aaa bbb"
        "aaa: fft"
        "fft: ccc"
        "bbb: tty"
        "tty: ccc"
        "ccc: ddd eee"
        "ddd: hub"
        "hub: fff"
        "eee: dac"
        "dac: fff"
        "fff: ggg hhh"
        "ggg: out"
        "hhh: out"
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(5L, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(2L, puzzle2(testInput2))