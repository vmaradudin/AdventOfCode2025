namespace ``2025``

open AdventOfCode2025.Day02
open Xunit

module ``Day 02``=
    let testInput1 = 
        [|
          "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
        |]
    
    [<Fact>]
    let ``Puzzle 1`` () =
        Assert.Equal(1227775554L, puzzle1(testInput1))
    
    [<Fact>]
    let ``Puzzle 2`` () =
        Assert.Equal(4174379265L, puzzle2(testInput1))