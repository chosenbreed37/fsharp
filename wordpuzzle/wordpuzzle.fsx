open System.IO

let loadPuzzle file =
    let arr' = File.ReadAllLines file
    let arr = Array2D.create arr'.Length arr'.Length ' '
    for l = 0 to arr'.Length - 1 do
        let line = arr'.[l].ToCharArray()
        for m = 0 to line.Length - 1 do
           arr.[l,m] <- line.[m]
    arr

let findw (w: string) a =
    let findw' (w: char []) (a: char[,]) =
        let ubound = (Array2D.length1 a) - 1

        let rec right r i =
            match r with
            |[||] -> Some i
            |_ -> 
                match i with
                |(x, y) when y = ubound -> None
                |(x, y) when a.[x,y+1] = r.[0] -> right r.[1..] (x,y+1)
                |_ -> None

        let rec down r i =
            match r with
            |[||] -> Some i
            |_ -> 
                match i with
                |(x, y) when x = ubound -> None
                |(x, y) when a.[x+1,y] = r.[0] -> down r.[1..] (x+1,y)
                |_ -> None

        let rec start l i =
            match i with
            |(x, y) when a.[x,y] = l -> 
                match right w.[1..] i with
                |Some j -> Some (i, j)
                |_ -> 
                    match down w.[1..] i with
                    |Some j -> Some (i, j)
                    |_ -> 
                        match x,y with
                        |x, y when y = ubound && x = ubound -> None
                        |x, y when y = ubound -> start l (x+1, 0)
                        |_ -> start l (x,y+1)
            |(x, y) when x = ubound && y = ubound -> None
            |(x, y) when y = ubound -> start l (x+1, 0)
            |(x, y) -> start l (x, y+1)
            
        start w.[0] (0, 0)
    findw' (w.ToCharArray()) a
