namespace FSharpWorkshop.Implementation.Finances

// Discriminated union
type Currency =
    | PLN
    | USD
    | EUR
    | GBP
    | OtherCurrency of string