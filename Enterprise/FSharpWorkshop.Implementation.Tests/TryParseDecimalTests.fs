namespace FSharpWorkshop.Implementation.Tests

open FSharpWorkshop.Implementation.Finances.Parsing
open System.Globalization
open Xunit

module TryParseDecimalTests =
    
    [<Fact>]
    let ``Not valid Slavic decimal - should return None`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let actual = "1,234.567" |> tryParseDecimal culture
        actual |> Assert.isNone
    
    [<Fact>]
    let ``Valid positive Slavic decimal - should return Some with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = 123.456M
        let actual = "123,456" |> tryParseDecimal culture
        let okActual = Assert.isSome actual
        Assert.Equal(expected, okActual)
    
    [<Fact>]
    let ``Valid negative Slavic decimal - should return Some with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = -123.456M
        let actual = "-123,456" |> tryParseDecimal culture
        let okActual = Assert.isSome actual
        Assert.Equal(expected, okActual)