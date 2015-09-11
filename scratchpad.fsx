open System

type Validator(rule: string -> bool) =
    member this.Execute(input) =
        rule input

    interface IDisposable with
        member this.Dispose() = printfn "Disposing..."


let printListRev list =
    let rec printListRevTR list cont =
        match list with
        | [] -> cont()
        | hd :: tl ->
            printListRevTR tl (fun () -> printf "%d " hd
                                         cont())
    printListRevTR list (fun () -> printfn "Done!")

                