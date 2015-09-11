// euler # 5
// 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
// What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?

let rec divisibleBy (n: int64) acc (x:int64) =
    match n, x with
    |(1L, _) -> acc
    |(n, x) -> divisibleBy (n - 1L) (acc && (x % n = 0L)) x

let euler5 n =
    seq { n .. n .. System.Int64.MaxValue }
    |> Seq.filter (divisibleBy n true)
    |> Seq.head