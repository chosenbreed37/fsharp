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