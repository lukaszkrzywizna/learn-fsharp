module FSharpWorkshop.Console
// For more information see https://aka.ms/fsharp-console-apps

open FSharpWorkshop.Implementation.Finances
open FSharpWorkshop.Implementation.Finances.Parsing
open FSharpWorkshop.Implementation.Finances.Operators
open System.Globalization

let sprintCurrency (currency: Currency): string = 
    match currency with
    | PLN -> "PLN"
    | EUR -> "EUR"
    | USD -> "USD"
    | GBP -> "GBP"
    | OtherCurrency code -> code

let sprintMoney (money: Money): string =
    sprintf "%f %s" money.Amount (sprintCurrency money.Currency)

[<EntryPoint>]
let main argv =
    let culture = CultureInfo.GetCultureInfo("pl-PL")

    let input =
        [| "100 PLN"
           "10 USD"
           "-9,99 PLN"
           "-4,99 PLN"
           "-1,49 USD" |]

    let values =
        input
        |> Array.map (tryParseMoney culture)
        |> Array.choose id

    let totalBalance =
        values
        |> Array.fold (+++) Balance.empty
        |> Balance.toList
        |> List.map sprintMoney
        |> List.reduce (sprintf "%s\r\n%s")

    printfn "Your total balance is:\r\n%s" totalBalance
    
    0