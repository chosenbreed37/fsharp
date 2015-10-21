#r "System.DirectoryServices"
#r "System.DirectoryServices.AccountManagement.dll"

open System.DirectoryServices
open System.DirectoryServices.AccountManagement

let ad = new PrincipalContext(ContextType.Domain, "edftrading.com")
let createSearcher o = new PrincipalSearcher(o)

let asGroup id =            
    let g = new GroupPrincipal(ad)
    g.SamAccountName <- id
    g

let asUser id =            
    let u = new UserPrincipal(ad)
    u.SamAccountName <- id
    u

let queryAd withPrincipal id =
    use searcher = id |> withPrincipal |> createSearcher
    let result = searcher.FindOne()
    if result <> null then
        Some result
    else 
        None

type OrElseBuilder() =
    member __.ReturnFrom(x) = x
    
    member __.Combine(a, b) =
        match a with
        | Some _ -> a
        | None -> b
    
    member __.Delay(f) = f()

let orElse = new OrElseBuilder()

let adQuery id = orElse {
    return! queryAd asGroup id
    return! queryAd asUser id
}