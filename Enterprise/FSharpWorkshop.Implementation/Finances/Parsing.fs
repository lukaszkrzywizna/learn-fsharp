module FSharpWorkshop.Implementation.Finances.Parsing

open System
open System.Globalization
open Microsoft.FSharp.Core
open FSharpWorkshop.Implementation.ActivePatterns

[<Literal>]
let CurrencyRegexPattern = "(?<currency>[a-zA-Z]{3,4})"

[<Literal>]
let MoneyRegexPattern = "(?<amount>-?\d+(,\d+)?)\s(?<currency>[a-zA-Z]{3,4})"

let tryParseDecimal (provider: IFormatProvider) (str: string): decimal option =
    failwith "not implemented"

let tryParseKnownCurrency (code: string): Currency option =
    failwith "not implemented"
    
let tryParseCurrency (str: string): Currency option =
    failwith "not implemented"
    
let tryParseMoney (provider: IFormatProvider) (str: string): Money option =
    failwith "not implemented"

let parseDecimal (provider: IFormatProvider) (str: string): Result<decimal, string> =
    failwith "not implemented"

let parseKnownCurrency (code: string): Result<Currency, string> =
    failwith "not implemented"

let parseCurrency (str: string): Result<Currency, string> =
    failwith "not implemented"
    
let parseMoney (provider: IFormatProvider) (str: string): Result<Money, string> =
    failwith "not implemented"