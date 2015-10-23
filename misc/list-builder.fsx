type ListBuilder() =
    member __.Bind(m, f) =
        m |> List.collect f
    member __.Zero() =
        []
    member __.Yield(x) =
        [x]
    member __.YieldFrom(m) =
        m
    member __.For(m, f) =
        __.Bind(m, f)
    member __.Combine(a, b) =
        List.concat [a;b]
    member __.Delay(f) =
        f()             

let lister = new ListBuilder()

lister {
    yield 1
    yield 2}

lister {
    yield 1
    yield! [2;3]}

lister {
    for i in ["red";"blue"] do
        yield i
        for j in ["hat";"tie"] do
            yield! [i + " " + j ; "-"]}