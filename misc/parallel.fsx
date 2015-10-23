open System
open System.Threading.Tasks

let data = [|1..10|]
let data' = 
    data
    |> Array.Parallel.map (fun x -> x * x)

