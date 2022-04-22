module LearnFsharp.Tasks.Serialization

open Newtonsoft.Json
open Xunit

(*
    Task: Please prepare
    - Point type which will
        -- hold two ints : x and y
        -- will have `add` function/operator which add two point by summing their fields (x + x; y + y) 
    - custom operator ?+? which will 
        1. accept two serialized points, 
        2. deserialized them, 
        3. add them, 
        4. serialize whole point
    HINT: for serialization/deserialization use JsonConvert.SerializeObject and JsonConvert.DeserializeObject<'t>
 *)
 
let (?+?) a b =
    failwith "not implemented"
    
open FsUnit.Xunit
[<Fact>]
let ``I add two serialized points and I get serialized sum of their fields``() =
    """ {"X":3,"Y":5} """ ?+? """ {"X":4,"Y":6} """ |> should equal """{"X":7,"Y":11}"""