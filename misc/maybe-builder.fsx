let divideBy bottom top =
    if bottom = 0 then None
    else Some(top/bottom)

// attempt division 3 times
let divideByWorkflow init x y z =
    let a = init |> divideBy x
    match a with
    |None -> None
    |Some a' ->
        let b = a' |> divideBy y
        match b with
        |None -> None
        |Some b' ->
            let c = b' |> divideBy z
            match c with
            |None -> None
            |Some c' -> Some c'

let good = divideByWorkflow 64 2 4 8
let bad = divideByWorkflow 64 2 0 8

type MaybeBuilder() =

    member this.Bind(x, f) =
        match x with
        |None -> None
        |Some a -> f a

    member this.Return(x) =
        Some x

let maybe = new MaybeBuilder()

// maybe has completely hidden the branching logic from above
let divideByWorkflow' init x y z =
    maybe {
        let! a = init |> divideBy x
        let! b = a |> divideBy y
        let! c = b |> divideBy z
        return c
    }   

let good' = divideByWorkflow' 64 2 4 8
let bad' = divideByWorkflow' 64 2 0 8     