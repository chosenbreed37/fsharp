open System.Diagnostics

let windowedProcesses =
    query {
        for activeProcess in Process.GetProcesses() do
        where (activeProcess.MainWindowHandle <> nativeint 0)
        select activeProcess
    }