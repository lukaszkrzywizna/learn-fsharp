module LearnFsharp.Tasks.Scrabble

open System
open Microsoft.FSharp.Collections

(*
    Task: single-letter scrabble score counter:
    https://exercism.org/tracks/fsharp/exercises/scrabble-score
 *)

let scrabbleLetterScore str =
    failwith "not implemented"

let scrabbleWholeWord (str: string) =
    failwith "not implemented"

(* 
    EXTRA TASK: Extend scoring function to accept custom scoring
    HINT: Use union to group letters to different score level
    Question: How unrecognized value should be handled?
 *)
 
    
let scrabbleLetterScoreCustom str =
    failwith "not implemented"

let myScore level =
    failwith "not implemented"

let scrabbleWholeWordCustom customScore (str: string) =
    failwith "not implemented"

open Xunit
open FsUnit.Xunit
[<Theory>]
[<InlineData("cabbage", 14)>]
[<InlineData("fsharpIsAwesome", 28)>]
[<InlineData("anotherWord", 18)>]
[<InlineData("___  ___", 0)>]
let ``My word is worth exactly like scrabble would score``(word, score) =
    scrabbleWholeWord word |> should equal score
    