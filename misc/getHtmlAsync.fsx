open System.IO
open System.Net

let getHtml (url:string) =
    async {
        try
            let req = WebRequest.Create(url)
            let! rsp = req.AsyncGetResponse()
            use stream = rsp.GetResponseStream()
            use reader = new StreamReader(stream)
            return reader.ReadToEndAsync().Result
        with
        | :? IOException as ioe ->
            return sprintf "IOException: %s" ioe.Message
    }

// execute the workflow synchronously (blocking)
let html = getHtml @"http://google.com" |> Async.RunSynchronously

// execute the workflow in parallel
let pages : string[] =
    ["http://google.com"; "http://bing.com"; "http://yahoo.com"]
    |> List.map getHtml
    |> Async.Parallel
    |> Async.RunSynchronously

// execute workflow, catching any unhandled exceptions
let htmlOrException =
    getHtml "http://google.com"
    |> Async.Catch
    |> Async.RunSynchronously
    |> function 
       |Choice1Of2 result -> printfn "success!"
       |Choice2Of2 (ex: exn) -> printfn "error: %s" ex.Message
