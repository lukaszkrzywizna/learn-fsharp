namespace FSharpWorkshop.Implementation.Tests

open Xunit

module Assert =

    let equalMaps (expected: Map<'key, 'value>) (actual: Map<'key, 'value>): unit =
        let expectedEn = System.Linq.Enumerable.AsEnumerable(expected)
        let actualEn = System.Linq.Enumerable.AsEnumerable(actual)
        Assert.Equal<System.Collections.Generic.KeyValuePair<'key, 'value>>(expectedEn, actualEn) |> ignore
        ()
        
    let isSome (actual: 'a option): 'a =
        Assert.True(actual.IsSome, "Given value was None")
        actual.Value
        
    let isNone (actual: 'a option): unit =
        match actual with
        | Some value -> Assert.True(false, $"Expected None but value was Some(%s{value.ToString()})")
        | None -> ()

    let isOk (actual: Result<'ok, 'error>): 'ok =
        match actual with
        | Ok value -> value
        | Error error ->
            Assert.True(false, $"Given value was Error(%s{error.ToString()})")
            failwith "not expected point of code"
            
    let isError (actual: Result<'ok, 'error>): 'error =
        match actual with
        | Ok value ->
            Assert.True(false, $"Given value was Ok(%s{value.ToString()})")
            failwith "not expected point of code"
        | Error error -> error