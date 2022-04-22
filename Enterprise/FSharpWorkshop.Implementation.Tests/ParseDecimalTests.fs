namespace FSharpWorkshop.Implementation.Tests

open FSharpWorkshop.Implementation.Finances.Parsing
open System.Globalization
open Xunit

module ParseDecimalTests =
    
    [<Fact>]
    let ``Not valid Slavic decimal - should return Error with proper message`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = 123.456M
        let actual = "1,234.567" |> parseDecimal culture
        actual |> Assert.isError |> ignore
    
    [<Fact>]
    let ``Valid positive Slavic decimal - should return Ok with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = 123.456M
        let actual = "123,456" |> parseDecimal culture
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)
    
    [<Fact>]
    let ``Valid negative Slavic decimal - should return Ok with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = -123.456M
        let actual = "-123,456" |> parseDecimal culture
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)