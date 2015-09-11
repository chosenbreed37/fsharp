open System.Windows.Forms

let form = new Form(Text="Relative Clicking", TopMost=true)

form.MouseClick.AddHandler(
    new MouseEventHandler(
        fun sender clickArgs -> printfn "MouseClickEvent @ [%d, %d]" clickArgs.X clickArgs.Y))

let centeredClickEvent =
    form.MouseClick
    |> Observable.map (fun clickArgs -> clickArgs.X - (form.Width / 2), clickArgs.Y - (form.Height / 2))

centeredClickEvent
|> Observable.add (fun (x, y) -> printfn "CenteredClickEvent @ [%d, %d]" x y)