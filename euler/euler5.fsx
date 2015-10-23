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

let euler5Async n =
   let del = new System.Func<int64, int64>(euler5)
   Async.FromBeginEnd(n, del.BeginInvoke, del.EndInvoke)

let execute n =
    Async.StartWithContinuations
        (euler5Async n, 
        (fun result -> printfn "euler5 [%d] completed with result: %d" n result),
        (fun exn -> printfn "exception: %s" exn.Message),
        (fun oce -> printfn "cancelled: %s" oce.Message)
    )
