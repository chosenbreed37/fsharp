// memoization
let f x =
    System.Threading.Thread.Sleep(x * 1000)
    x
 
let memoise (f: 'a -> 'b) =
    let store = new System.Collections.Generic.Dictionary<'a, 'b>()
    
    let memoizedF (x: 'a) =
        match store.TryGetValue(x) with
        |true, y -> y
        |false, _ ->
            let y = f x
            store.Add(x, y)
            y
    memoizedF
