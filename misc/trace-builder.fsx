type TraceBuilder() =
    member this.Bind(m, f) =
        match m with
        | None -> 
            printfn "Binding with None. Exiting."
            None
        | Some a -> 
            printfn "Binding with Some(%A). Continuing." a
            Option.bind f m

    member this.Return(x) =
        printfn "Returning a unwrapped %A as an option" x
        Some x

    member this.ReturnFrom(m) =
        printfn "Returning an option (%A) direclty" m
        m

let trace = new TraceBuilder()
trace {
        return 1 } |> printfn "Result1: %A"