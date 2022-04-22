namespace FSharpWorkshop.Implementation.Tests

open FSharpWorkshop.Implementation.Finances
open FSharpWorkshop.Implementation.Finances.Parsing
open System.Globalization
open Xunit

module TryParseMoneyTests =
    
    [<Fact>]
    let ``Not valid amount decimal - should return None`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let actual = "this is not a money string" |> tryParseMoney culture
        actual |> Assert.isNone
    
    [<Fact>]
    let ``Valid positive amount of known currency - should return Some with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = 123.456M; Currency = PLN }
        let actual = "123,456 PLN" |> tryParseMoney culture
        let someActual = Assert.isSome actual
        Assert.Equal(expected, someActual)
    
    [<Fact>]
    let ``Valid negative amount of known currency - should return Some with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = -123.456M; Currency = PLN }
        let actual = "-123,456 PLN" |> tryParseMoney culture
        let someActual = Assert.isSome actual
        Assert.Equal(expected, someActual)
    
    [<Fact>]
    let ``Valid positive amount of valid unknown currency - should return Some with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = 123.456M; Currency = OtherCurrency "CZK" }
        let actual = "123,456 CZK" |> tryParseMoney culture
        let someActual = Assert.isSome actual
        Assert.Equal(expected, someActual)
    
    [<Fact>]
    let ``Valid negative amount of valid unknown currency - should return Some with proper value`` () =
        let culture = CultureInfo.GetCultureInfo("pl-PL")
        let expected = { Amount = -123.456M; Currency = OtherCurrency "CZK" }
        let actual = "-123,456 CZK" |> tryParseMoney culture
        let someActual = Assert.isSome actual
        Assert.Equal(expected, someActual)