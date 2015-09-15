let map1 = [("1", "One"); ("2", "Two");] |> Map.ofList
let map2 = [("3", "Three"); ("4", "Four");] |> Map.ofList
let map3 = [("5", "Five"); ("6", "Six");] |> Map.ofList

let multiLookup key =
    match map1.TryFind key with
    |Some result1 -> Some result1
    |None ->
        match map2.TryFind key with
        |Some result2 -> Some result2
        |None ->
            match map3.TryFind key with
            |Some result3 -> Some result3
            |None -> None
multiLookup "3" |> printfn "Result for 3 is %A"
multiLookup "X" |> printfn "Result for X is %A"

type OrElseBuilder() =
    
    member __.ReturnFrom (x) = x

    member __.Combine (a, b) =
        match a with
        |Some _ -> a // a succeeds -- use it
        |None -> b // a fails -- use b instead
    
    member __.Delay (f) = f()

let orElse = new OrElseBuilder()

let multiLookup' key =
    orElse {
        return! map1.TryFind key
        return! map2.TryFind key
        return! map3.TryFind key
    }

multiLookup' "3" |> printfn "Result for 3 is %A"
multiLookup' "X" |> printfn "Result for X is %A"