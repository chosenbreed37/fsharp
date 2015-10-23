open System

let strToInt str =
    match Int32.TryParse(str) with
    |false,_ -> None
    |true, x -> Some x

type YourWorkflowBuilder() =
    
    member __.Return (x) = Some x

    member __.Bind (x, f) =
        match x with
        |None -> None
        |Some x -> f x

let yourWorkflow = new YourWorkflowBuilder()

let stringAddWorkflow x y z =
    yourWorkflow {
        let! a = strToInt x
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
    }

let strAdd str i =
    match strToInt str with
    |None -> None
    |Some x -> Some (x + i)

let (>>=) m f =
    match m with
    |None -> None
    |Some x -> f x

let good = strToInt "1" >>= strAdd "2" >>= strAdd "3"
let bad = strToInt "1" >>= strAdd "xyz" >>= strAdd "3"