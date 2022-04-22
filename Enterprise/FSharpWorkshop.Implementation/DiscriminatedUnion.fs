module FSharpWorkshop.Implementation.DiscriminatedUnion

open Microsoft.FSharp.Reflection

let toString (x: 'a): string = 
    match FSharpValue.GetUnionFields(x, typeof<'a>) with
    | case, _ -> case.Name

let fromString<'a> (fNone: string -> 'a option) (s: string) =
    match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
    | [|case|] -> Some(FSharpValue.MakeUnion(case, [||]) :?> 'a)
    | _ -> fNone s