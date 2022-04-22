namespace FSharpWorkshop.Implementation.Tests

open FSharpWorkshop.Implementation.Finances
open FSharpWorkshop.Implementation.Finances.Parsing
open Xunit

module ParseCurrencyTests =
    
    [<Fact>]
    let ``Not valid currency code - should return Error with proper message`` () =
        let str = "!@#$%^"
        let actual = parseCurrency str
        actual |> Assert.isError |> ignore
    
    [<Fact>]
    let ``Valid currency - should return Ok with proper value`` () =
        let str = "PLN"
        let expected = PLN
        let actual = parseCurrency str
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)
    
    [<Fact>]
    let ``Valid but unknown currency - should return Ok with proper value`` () =
        let str = "CZK"
        let expected = OtherCurrency "CZK"
        let actual = parseCurrency str
        let okActual = Assert.isOk actual
        Assert.Equal(expected, okActual)