let rec trialDiv n d =
    match n with
    | n when (d * d) > n -> true
    | n when n % d = 0 -> false
    | n -> trialDiv n (d + 2)
    | _ -> true

let isPrime n =
    match n with
    | 0 | 1 -> false
    | 2 -> true
    | n when n % 2 = 0 -> false
    | n -> trialDiv n 3

let primes = 
    seq { for n in 1..System.Int32.MaxValue -> n }
    |> Seq.filter isPrime

let euler7 n = primes |> Seq.skip(n - 1) |> Seq.take 1   