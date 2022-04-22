namespace FSharpWorkshop.Implementation.Tests

open FSharpWorkshop.Implementation.Finances
open FSharpWorkshop.Implementation.Finances.Parsing
open System.Globalization
open Xunit

module ParseMoneyTests =
    
    [<Fact>]
    let ``Not valid amount decimal - should return Error`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let actual = "abcd USD" |> parseMoney culture
        actual |> Assert.isError |> ignore
    
    [<Fact>]
    let ``Valid positive amount of known currency - should return Ok with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = 123.456M; Currency = PLN }
        let actual = "123,456 PLN" |> parseMoney culture
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)
    
    [<Fact>]
    let ``Valid negative amount of known currency - should return Ok with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = -123.456M; Currency = PLN }
        let actual = "-123,456 PLN" |> parseMoney culture
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)
    
    [<Fact>]
    let ``Valid positive amount of valid unknown currency - should return Ok with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = 123.456M; Currency = OtherCurrency "CZK" }
        let actual = "123,456 CZK" |> parseMoney culture
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)
    
    [<Fact>]
    let ``Valid negative amount of valid unknown currency - should return Ok with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = -123.456M; Currency = OtherCurrency "CZK" }
        let actual = "-123,456 CZK" |> parseMoney culture
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)