namespace FSharpWorkshop.Implementation.Tests

open FSharpWorkshop.Implementation.ActivePatterns
open Xunit

module TryRegexMatchTests =
    
    [<Fact>]
    let ``Input does not match the regex pattern - should return None`` () =
        let pattern = "[a-z]"
        let input = "123"
        let expected = None
        let actual =
            match input with
            | TryRegexMatch pattern regexResult -> Some regexResult
            | _ -> None
        Assert.Equal(expected, actual)
        
    [<Fact>]
    let ``Input matches the regex pattern - should return one indexed group`` () =
        let pattern = "[0-9]"
        let input = "a2b"
        let expected =
            [ "0", "2" ]
            |> Map.ofList
        let actual =
            match input with
            | TryRegexMatch pattern regexResult ->  Some regexResult
            | _ -> None
        let someActual = Assert.isSome actual
        Assert.equalMaps expected someActual
        
    [<Fact>]
    let ``Input matches the regex pattern - should return one named group`` () =
        let pattern = "(?<num>[0-9])"
        let input = "a2b"
        let expected =
            [ "0", "2"
              "num", "2" ]
            |> Map.ofList
        let actual =
            match input with
            | TryRegexMatch pattern regexResult ->  Some regexResult
            | _ -> None
        let someActual = Assert.isSome actual
        Assert.equalMaps expected someActual
        
    [<Fact>]
    let ``Input matches the regex pattern with one of two groups - should return only first group`` () =
        let pattern = "(?<matched>[0-4])|(?<not_matched>[5-9])"
        let input = "1b"
        let expected =
            [ "0", "1"
              "matched", "1" ]
            |> Map.ofList
        let actual =
            match input with
            | TryRegexMatch pattern regexResult ->  Some regexResult
            | _ -> None
        let someActual = Assert.isSome actual
        Assert.equalMaps expected someActual