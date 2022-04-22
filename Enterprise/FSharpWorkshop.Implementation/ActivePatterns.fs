module FSharpWorkshop.Implementation.ActivePatterns

open Microsoft.FSharp.Core

let (|TryRegexMatch|_|) (pattern: string) (input: string): Map<string, string> option =
    failwith "not implemented"