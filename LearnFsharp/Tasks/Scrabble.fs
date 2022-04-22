module LearnFsharp.Tasks.Scrabble

open System
open Microsoft.FSharp.Collections

(*
    Task: single-letter scrabble score counter:
    https://exercism.org/tracks/fsharp/exercises/scrabble-score
 *)

let scrabbleLetterScore str =
    match str with
    |"A"|"E"|"I"|"O"|"U"|"L"|"N"|"R"|"S"|"T" -> 1
    |"D"|"G" -> 2
    |"B"|"C"|"M"|"P" -> 3
    |"F"|"H"|"V"|"W"|"Y" -> 4
    |"K" -> 5
    |"J"|"X" -> 8
    |"Q"|"Z" -> 10
    | _ -> 0

let scrabbleWholeWord (str: string) =
    Seq.init str.Length (fun x -> str[x].ToString().ToUpper())
    |> Seq.map scrabbleLetterScore
    |> Seq.sum

(* 
    EXTRA TASK: Extend scoring function to accept custom scoring
    HINT: Use union to group letters to different score level
    Question: How unrecognized value should be handled?
 *)
 
type ScoreLevel =
    | Lowest
    | Lower
    | Low
    | Medium
    | High
    | Higher
    | Highest
    
let scrabbleLetterScoreCustom str =
    match str with
    |"A"|"E"|"I"|"O"|"U"|"L"|"N"|"R"|"S"|"T" -> Some Lowest
    |"D"|"G" -> Some Lower
    |"B"|"C"|"M"|"P" -> Some Low
    |"F"|"H"|"V"|"W"|"Y" -> Some Medium
    |"K" -> Some High
    |"J"|"X" -> Some Higher
    |"Q"|"Z" -> Some Highest
    | _ -> None

let myScore (level: ScoreLevel) =
    match level with
    | Lowest -> 10
    | Lower -> 100
    | Low -> 1000
    | Medium -> 1500
    | High -> 2500
    | Higher -> 5000
    | Highest -> 100000

let scrabbleWholeWordCustom customScore (str: string) =
    Seq.init str.Length (fun x -> str[x].ToString().ToUpper())
    |> Seq.map scrabbleLetterScoreCustom
    |> Seq.map (Option.map customScore)
    |> Seq.map (Option.defaultValue 0)
    |> Seq.sum

open Xunit
open FsUnit.Xunit
[<Theory>]
[<InlineData("cabbage", 14)>]
[<InlineData("fsharpIsAwesome", 28)>]
[<InlineData("anotherWord", 18)>]
[<InlineData("___  ___", 0)>]
let ``My word is worth exactly like scrabble would score``(word, score) =
    scrabbleWholeWord word |> should equal score
    