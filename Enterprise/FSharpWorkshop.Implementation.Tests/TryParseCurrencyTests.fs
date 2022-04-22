namespace FSharpWorkshop.Implementation.Tests

open FSharpWorkshop.Implementation.Finances
open FSharpWorkshop.Implementation.Finances.Parsing
open Xunit

module TryParseCurrencyTests =
    
    [<Fact>]
    let ``Not valid currency code - should return None`` () =
        let str = "!@#$%^"
        let actual = tryParseCurrency str
        actual |> Assert.isNone
    
    [<Fact>]
    let ``Valid currency - should return Some with proper value`` () =
        let str = "PLN"
        let expected = PLN
        let actual = tryParseCurrency str
        let someActual = Assert.isSome actual
        Assert.Equal(expected, someActual)
    
    [<Fact>]
    let ``Valid but unknown currency - should return Some with proper value`` () =
        let str = "CZK"
        let expected = OtherCurrency "CZK"
        let actual = tryParseCurrency str
        let someActual = Assert.isSome actual
        Assert.Equal(expected, someActual)