// Learn more about F# at http://fsharp.org

open System

let rec luc x =
    match x with
    | y when y <= 0 -> failwith "argument value to luc must be positive."
    | 1 -> 1
    | 2 -> 3
    | x -> luc (x-1) + luc (x-2)

let rec concatStringList = function head :: tail -> head + concatStringList tail | [] -> ""

[<EntryPoint>]
let main argv =
    printfn "luc 2 = %i" (luc 2)
    printfn "luc 3 = %i" (luc 3)
    printfn "luc 4 = %i" (luc 4)
    printfn "luc 5 = %i" (luc 5)
    printfn "%s" (concatStringList ["hey "; "hi "; "ho"])
    let prints = printfn "%s"
    concatStringList ["hey "; "hi "; "ho"] |> prints
    0 // return an integer exit code

