namespace FSharpWorkshop.Implementation.Finances

module Operators =

    let (+!) (x: Money) (y: Money): Money =
        if x.Currency = y.Currency
        then
            { Amount = x.Amount + y.Amount
              Currency = x.Currency }
        else failwith "Different currencies"

    let (+?) (x: Money) (y: Money): Money option =
        if x.Currency = y.Currency
        then
            { Amount = x.Amount + y.Amount
              Currency = x.Currency }
            |> Some
        else None

    let (+?!) (x: Money) (y: Money): Result<Money, string> =
        if x.Currency = y.Currency
        then
            { Amount = x.Amount + y.Amount
              Currency = x.Currency }
            |> Ok
        else Error "Different currencies"
    
    let (+++) (balance: Balance) (value: Money): Balance =
        balance.add value