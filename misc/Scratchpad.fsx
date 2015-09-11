open System.IO

let rec allFilesUnder basePath =
    seq {
            yield! Directory.GetFiles(basePath)
            for subdir in Directory.GetDirectories(basePath) do
                yield! allFilesUnder subdir
        }

let nextFibUnder100 (a, b) = 
    if (a + b) > 100 then
        None
    else
        let next = a + b
        Some(next, (next, a))

let euler5Bf =
    let mutable i = 20
    while i %  2 <> 0 || i %  3 <> 0 || i %  4 <> 0 || i %  5 <> 0 ||
         i %  6 <> 0 || i %  7 <> 0 || i %  8 <> 0 || i %  9 <> 0 ||
         i % 10 <> 0 || i % 11 <> 0 || i % 12 <> 0 || i % 13 <> 0 ||
         i % 14 <> 0 || i % 15 <> 0 || i % 16 <> 0 || i % 17 <> 0 ||
         i % 18 <> 0 || i % 19 <> 0 || i % 20 <> 0 do
        i <- i + 20
    i
let preds n = [1..n] |> (fun x y -> y % n <> 0)

// Single case active pattern
open System.IO

let (|FileExtension|) filePath = Path.GetExtension(filePath)

let checkFileType (filePath: string) = 
    match filePath with
    |filePath when Path.GetExtension(filePath) = ".txt" 
        -> printfn "It's a text file."
    |FileExtension ".jpg"
    |FileExtension ".png"
    |FileExtension ".gif"
        -> printfn "It's an image file."
    |FileExtension ext
        -> printfn "Unknown file extension [%s]" ext

// Partial active pattern (aka optional single case active pattern)

let (|ToBool|_|) input =
    let success, result = System.Boolean.TryParse(input)
    if success then Some(result)
    else None

let (|ToInt|_|) input =
    let success, result = System.Int32.TryParse(input)
    if success then Some(result)
    else None

let (|ToFloat|_|) input =
    let success, result = System.Double.TryParse(input)
    if success then Some(result)
    else None

let describe input =
    match input with
    |ToBool b -> printfn "%s is a bool with value %b" input b
    |ToInt i -> printfn "%s is an int with value %d" input i
    |ToFloat f -> printfn "%s is a float with value %f" input f
    |_ -> printfn  "%s is a neither a bool, an int or a float" input

let isPrime n =
    match n with
    | 0 | 1 -> false
    | 2 -> true
    | n when n % 2 = 0 -> false
