namespace FSharpWorkshop.Implementation.Tests

open FsUnit
open FSharpWorkshop.Implementation.Finances
open FSharpWorkshop.Implementation.Finances.Operators
open Xunit

module OperatorsTests =
    
    [<Fact>]
    let ``5 USD +! 2 PLN = fail`` () =
        let x = { Amount = 5M; Currency = USD }
        let y = { Amount = 2M; Currency = PLN }
        let action () = x +! y |> ignore
        action |> shouldFail
        
    [<Fact>]
    let ``5 USD +! 2 USD = 7 USD`` () =
        let expected = { Amount = 7M; Currency = USD }
        let x = { Amount = 5M; Currency = USD }
        let y = { Amount = 2M; Currency = USD }
        let actual = x +! y
        actual |> should equal expected
    
    [<Fact>]
    let ``5 USD +? 2 PLN = None`` () =
        let x = { Amount = 5M; Currency = USD }
        let y = { Amount = 2M; Currency = PLN }
        let actual = x +? y
        actual |> Assert.isNone 
        
    [<Fact>]
    let ``5 USD +? 2 USD = Some (7 USD)`` () =
        let expected = { Amount = 7M; Currency = USD }
        let x = { Amount = 5M; Currency = USD }
        let y = { Amount = 2M; Currency = USD }
        let actual = x +? y
        let someActual = Assert.isSome actual
        someActual |> should equal expected
    
    [<Fact>]
    let ``5 USD +?! 2 PLN = Error ('Different currencies')`` () =
        let x = { Amount = 5M; Currency = USD }
        let y = { Amount = 2M; Currency = PLN }
        let actual = x +?! y
        let errorActual = Assert.isError actual
        errorActual |> should equal "Different currencies"
        
    [<Fact>]
    let ``5 USD +?! 2 USD = Ok (7 USD)`` () =
        let expected = { Amount = 7M; Currency = USD }
        let x = { Amount = 5M; Currency = USD }
        let y = { Amount = 2M; Currency = USD }
        let actual = x +?! y
        let okActual = Assert.isOk actual
        okActual |> should equal expected
        
    [<Fact>]
    let ``Balance (5 USD) +++ 2 USD = Balance (7 USD)`` () =
        let expected = [{ Amount = 7M; Currency = USD }]
        let x = 
            [ USD, 5M ]
            |> Map.ofList
            |> Balance
        let y = { Amount = 2M; Currency = USD }
        let actual = x +++ y
        actual |> Balance.toList |> should matchList expected
    
    [<Fact>]
    let ``Balance (5 USD) +++ 2 PLN = Balance (5 USD, 2 PLN)`` () =
        let expected = 
            [ { Amount = 5M; Currency = USD }
              { Amount = 2M; Currency = PLN } ]
        let x = 
            [ USD, 5M ]
            |> Map.ofList
            |> Balance
        let y = { Amount = 2M; Currency = PLN }
        let actual = x +++ y
        actual |> Balance.toList |> should matchList expected