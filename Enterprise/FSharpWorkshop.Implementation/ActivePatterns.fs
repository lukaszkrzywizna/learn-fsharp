module FSharpWorkshop.Implementation.ActivePatterns

open System.Text.RegularExpressions
open Microsoft.FSharp.Core

let (|TryRegexMatch|_|) (pattern: string) (input: string): Map<string, string> option =
    let m = Regex.Match(input, pattern)
    if not m.Success
    then None
    else
        let groups =
            m.Groups
            |> Seq.cast<Group>
        let map =
            groups
            |> Seq.filter (fun group -> group.Success)
            |> Seq.map (fun group -> (group.Name, group.Value))
            |> Map.ofSeq
        Some map