namespace ``2025``

open AdventOfCode2025.Day10
open Xunit

module ``Day 10``=
    let testInput1 = 
        [|
        "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}"
        "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}"
        "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(7, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(33, puzzle2(testInput1))