module LearnFsharp.ChapterOne.Lesson
open System
open FsToolkit.ErrorHandling.OptionCE
open System
open Microsoft.FSharp.Collections

(* SIMPLE TYPES *)

let num = 5                                 // int
let ``this is string`` = "my txt"           // `` `` allows for names with whitespaces
let boolean = true
let list = [1;2;3]
let array = [|1;2;3|]
let sequence = seq {1;2;3}                  // sequence aka IEnumerable
let tuple = (2, "txt")                      // parentheses and comma are important!
let unit = ()                               // special 'unit' type which defines 'nothing' like a 'void'

type MyEnum =                               // simple enum
    | One = 1
    | Two = 2
    | Three = 3
    
type NumOrTxtUnion =                        // discriminated (tagged) union
    | Integer of int
    | Text of string
    
type SimpleRecord = { Age: int; Name: string }

type SimpleClass(age: int, name: string) =  // class with constructor
    member this.Age = age                   // property
    member this.Name = name

let boolResult = num = 3                    // '=' is an EQUAL operator!
                                            // use '<-' for mutation 

// string <- "another text"                 // immutable by default so this won't work
let mutable mutableString = "txt"           // but if we define explicitly as mutable...
mutableString <- "my other txt"             // we can change value
// mutableString <- 5                       // but variables are statically typed

(* FLOW CONTROL *)

// IF/ELSE

let greaterOrNot x y =                       
    if x > y then "yes" else "no"           // simple if-else
                                            // but remember that does not work in imperative way
                                            // more like conditional operator from C# `?:`
                                            // it always have to return a value
                                            
let printIfEven x =
    if x % 2 = 0 then printf "it's even"    // this still return a value
    
    let result =                            // more explicit way
        if x % 2 = 0
        then printf "it's event"
        else ()
    result

let manyIfElse x =                          // of course, it supports if - else if - else
    if x % 2 = 0 then "2"
    elif x % 3 = 0 then "3"
    else "not 2 and 3"

// IF/ELSE on steroids = PATTERN MATCHING

let greaterThanOne x =                      // simple true or false case
    match x > 1 with
    | true -> "yes"
    | false -> "no"
    
let compareNums x y =                       //  case when we have more possibilities
    match (x, y) with
    | 1, 2 | 2, 1 -> "one and two"          // we can choose from many cases with `|`
    | x, y when x > y -> "x greater than y"
    | x, y when x >= 100 && y >= 100
         -> "both at least 100" 
    | x, _ when x >= 50                     // '_' discard symbol
         -> "x at least 50"
    | (x, y) & (5, _) -> "first is 5"       // we can combine many cases with '&'
    | _ -> "not matched"                    // note that without default case code won't compile
    
let analyzeString txt =                     // works for many types
    match txt with
    | "one" | "two" | "three" -> 1
    | "a" | "b" | "c" | "d" -> 2
    | "" -> 3
    | _ -> 0
    
let shortenVersion =                        // shorten version - valid only for one parameter function
    function
    | "one" | "two" | "three" -> 1
    | "a" | "b" | "c" | "d" -> 2
    | "" -> 3
    | _ -> 0 

// Exceptions

let divide x y =
    try
        // exception with custom msg
        if x < 0 then failwith "we don't accept negative values" 
        
        let result = x / y
        result
    with
        | :? DivideByZeroException as ex -> // ':?' match subtype operator
            printfn $"Catch of {ex.Message}"
            0
        | ex when ex.Message = "we don't accept negative values" -> // catch base exception with condition
            raise (InvalidOperationException(ex.Message))           // raise throws any exception
            0

(* LOOPS *)

for i=0 to 3 do                             // normal for
    printfn $"{i}"
    
for i=3 downto 0 do                         // reversed
    printfn $"reversed {i}"                 
    
for item in [1;2;3] do                      // for-in
    printfn $"for-each / for-in: {item}"    
    
let mutable i = 50
while i > 25 do                             // while loop
    printfn $"{i}"
    i <- i - 1
    

(* FUNCTIONS *)

let add a b = a + b                         // function is defined in the same way like values
                                            // no need for a type annotation - type inference
                                            // no need for `return`. last statement is returned by default
let explicitAdd (a: int) (b: int) : int =   // all types can be declared explicitly
    a + b                                   // indentation is important!
let addResult = add 5 4                     // simple executing without parenthesis (just use whitespace)

//let previous x = x + next x               // order DOES MATTER 
//let next a = a * 2

let returnsNothing a =
    printf $"{a}"
    ()                                      // returns nothing (unit)

let produce5 () = 5                         // accepts nothing

let generic a b =                           // generalized by default
    a.ToString() + b.ToString()
let stringResult = generic "txt" "other txt"
let intResult = generic 5 4

let mapAndAdd1 mapToInt (x: string) =       // function can be easily pass as an argument
    let mapped = mapToInt(x)                // abstraction for free: we want (string -> int) 
    add mapped 1                            // we don't care how this will be implemented

let lenghtPlusOne =
    mapAndAdd1 (fun x -> x.Length) "txt"    // (fun) is a anonymous func a.k.a "lambda"
let parsePlusOne =
    mapAndAdd1 (fun x -> Int32.Parse x) "5"
    
let rec factorial x =                       // we can use recursion with `rec` keyword
    match x with
    | 0 | 1 -> 1
    | x -> x * factorial(x - 1)
    
let operatorIsAFunc a b = (+) a b           // even operator is a function

let (<=>) a (min, max) =                    // we can define own operator!
    a >= min && a <= max
let isInRange = 2 <=> (1, 5)

(* COMPOSITION *)
    
let add1 x = x + 1                          // we have two functions which we would like to use together
let multiplyBy5 x = x * 5

let add1AndMultiplyBy5 x =                  
    let added = add1(x)
    let multiplied = multiplyBy5 added      // 1. standard way
    
    let added = x |> add1                   // '|>' it's a pipeline operator which just do: let (|>) x f = f(x)
    let added = add1 <| x                   // '<|' there is a reversed pipeline version!
    let multiplied = added |> multiplyBy5   // 2. pipelined way
    
    let multiplied =
        x |> add1 |> multiplyBy5            // we can define chain of functions aka 'pipeline'
    
    let composed = add1 >> multiplyBy5      // '>>' it's a composition pipeline let (>>) f g x = g(f(x))
    let multiplied = x |> composed
    let multiplied =
        x |> (add1 >> multiplyBy5)          // 3. with composition        
    
    multiplied                              // by the way: we're creating the same value multiple times: shadowing
                                            // it is not MUTATION

// OK, it works with one argument functions. What about >1 ones?
// It turns out that all functions have always one argument!

let subtract x y = y - x
let subtract1 = subtract 1                  // How this is possible? What about snd argument?
let subtractLambda =                        // Two argument function means:
    fun x ->
        fun y -> y - x                      // Give an single argument and I'll give a function for a second
                                            // It's called CURRYING
                                            // That's why we have "arrowed" signature
                                            // int -> int -> int
    
let threeArgFunc =                          // it works with infinite number of arguments
    fun x ->
        fun y ->
            fun z -> z - y * x
            
let compose4curried =
    fun op1 ->                              // it's what '>>' operator do under the hood
        fun op2 ->
            fun n -> op1 (op2 n)

let explicitParams x y = subtract x y       // so this function
let implicitParams = subtract               // and this do the same thing
    
let giveMeSumOfTuple (x, y) =               // this accepts TUPLE which is an one argument
    x + y

// giveMeSumOfTuple 5                       // this won't work

// But why I can pass only one argument? What about the rest?
// Applying only some of arguments it's called Partial Application
let take3args x y z =
    x + y + z
    
let takenX = take3args 5
let takenXAndY = takenX 4
let takenAll = takenXAndY 6

// it's often used for "functional dependency injection"

type GenerateCode = string -> string        // this is a type alias. helps make function definitions more readable

let createReservation                       // ORDER MATTERS - final input must be last
    (codeGen: GenerateCode)                 // [dependency] this is code generation function
    resExists                               // [dependency] check in db if reservation with same id exists
    id                                      // [input]      final id parameter taken from an user
    =
    if resExists(id) then "not created"
    else codeGen(id)
    
// ------------- during startup resolve dependencies -----------------------
let codeGen (id: string) =
    $"SPECIAL_CODE:{id.Length}"             // this is string interpolation
    
let reservationExists (id: string) =
    id.Length > 10
    
let createReservationFinal =                // we bake-in dependencies
    createReservation codeGen reservationExists
// -------------------------------------------------------------------------

// ------------- during handling user request ------------------------------
let idFromApi = "my-id"
let response = createReservationFinal idFromApi
// -------------------------------------------------------------------------

let multiply x y = x * y

let subtract1AndMultiply5 =                 // we can easily prepare chain of operations 
    subtract 1 >> multiply 2

let subtractXAndMultiplyY x y =             // it's better to keep it parametrized
    subtract x >> multiply y
    
let fancyVersion x y = (-) x >> (*) y

let subtract2AndMultiply3 =
    subtractXAndMultiplyY 2 3
let shouldBeSix =
    subtract2AndMultiply3 4

(* COLLECTIONS *)

// LIST

let rangeList = [1..10]                     // from 1 to 10
let fromFor =
    [for item in rangeList do item * item] // for as a generator
    
let listInit = List.init 10 (fun x -> x * 2)

//listInit[0] <- 1                          // it's immutable

let processedList =                         // collections can be easily processed by build-in functions
    listInit
    |> List.filter (fun x -> x > 10)
    |> List.map (fun x -> x.ToString())
    |> List.averageBy (fun x -> x.Length |> float)

let fstItem = listInit.Head                 // list is a linked list: (item, ref to next) -> (item, ref to next) ...
let rest = listInit.Tail                    // so we can always split list to fst and the rest

let rec functionalIter list =
    match list with
    | [] -> ()
    | head::rest ->
        printfn $"current head: {head}"
        functionalIter rest                 // tail-recursion - it's safe, no stack overflow exception
        //()                                // this would cause stack overflow cause there is extra work after
                                            // execution of next iteration

// ARRAYS

// Array is an Array from C#

let myArray = Array.init 5 (fun x -> x * x)
    
let iterArray array =                       // it can't be iterate like list
    for item in array do
        printfn $"current item: {item}"
    
myArray[0] |> ignore                        // but allows for faster item reading by using index
myArray[0] <- 1                             // and for updating

myArray                                     // has pretty much the same methods like Seq and List
|> Array.map (fun x -> x /2)
|> Array.reduce(fun current next -> current + next)
|> ignore
        
// SEQUENCES

let squares =                               // generating with loop
    seq {
        for i in 1..3 -> i * i
    }

let cubes =
    seq {
        for i in 1..3 -> i * i * i
    }
    
let weekdays includeWeekend =               // we can use "yield" keyword to return item
    seq {
        yield "Monday"
        yield "Tuesday"
        yield "Wednesday"
        yield "Thursday"
        yield "Friday"
        if includeWeekend then
            yield "Saturday"
            yield "Sunday"
    }    
    
let weekdaysShort includeWeekend =          // but it works without as well
    seq {
        "Monday"
        "Tuesday"
        "Wednesday"
        "Thursday"
        "Friday"
        if includeWeekend then
            "Saturday"
            "Sunday"
    }

let squaresAndCubes =                       // yield! returns a list instead of a single item
    seq {                                   // it's like selectMany
        yield! squares
        yield! cubes
    }

// MAPS

// Basically pretty much like C#'s Dictionary


let myMap =
    Map.empty
    |> Map.add "Bartosz" 5

//myMap["Bartosz"] <- 3                     // but immutable

let removed = Map.remove "Bartosz" myMap    // add|remove creates a new map with updated set

(* MORE ABOUT TYPES - RECORDS, ENUMS, UNION, CLASS *)

// RECORD

type MyRecord = { X: int; Y: string }       // this is simple record

let myRecord = { X = 5; Y = "txt" }         // that's how we create. compiler search for record with those fields

type MyRecord2 =
    { X: int; Y: string; Z: int64 }
    
let myRecord2 =
    { X = 15; Y = "other txt"; Z = 5L }     // compiler takes the last matching record by default

let myRecordOne : MyRecord =                // but we can override this by type annotation
    { X = 3; Y = "abc" }
    
let equal =                                 // records have build-in equality comparer
    {X = 3; Y = "xyz"; Z = 10L } = {X = 3; Y = "xyz"; Z = 10L }

let recordWithListEqual =                   // even with list; // those are anonymous records
    {| Name = "Name"; Collection = [1;2] |} =
        {| Name = "Name"; Collection = [1;2] |}

let updated = { myRecord with X = 3 }       // simple immutable-update

let matched r =
    match r with
    | { X = 1; Y = ""; Z = 8L } -> "matched 18L"
    | { X = _; Y = "txt"; Z = _  } -> "matched txt"
    | _ -> String.Empty

// Union && Option|Result type

type Boss = { Name: string }
type Worker =
    | Boss of Boss                          // can bind any type like a record
    | Accountant of string * int            // tuple
    | HR                                    // or event nothing

let matchedWorker worker =                  // pattern matching supports union
    match worker with
    | Boss b -> b.Name
    | Accountant(name, id) -> name + id.ToString()
    | HR -> "hr"                            // union has big advantage over enum and class inheritance
                                            // compiler knows all cases so it'll put a warning if some case is missed
                                            // try to comment some case
// OPTION

//type Option<'a> =                         // most used union - Option
//    | Some of 'a
//    | None
    
let tryDivide x y =                         // much more safe + gives documentation for free
    match y with
    | 0 -> None
    | _ -> Some (x / y)

let multiplyIfEven x y =
    match y % 2 with
    | 0 -> Some <| x * y
    | _ -> None

tryDivide 5 3                               // we can easily build flow of optional calculations
|> Option.map (fun result -> result * 5)
|> Option.bind (fun result -> result |> multiplyIfEven 5)
|> Option.filter (fun x -> x > 30)
|> ignore

option {                                    // (OUT OF SCOPE) more sexy version - read about computation expressions
    let! result = tryDivide 5 3
    let mapped = result * 5
    return! multiplyIfEven 5 mapped
} |> ignore

// RESULT

//type Result<'success, 'error> =           // another common union type. gives more info than option
//    | Ok of 'success
//    | Error of 'error
    
let tryDivideResult x y =                   // we can get some extra exception msg
    match y with
    | 0 -> Error "divided by zero"
    | _ -> Ok (x / y)

type TryDivideError =                       // but we can be more explicit and define potential errors with another union!
    | DivideByZero

let evenBetterTryDivide x y =
    match y with
    | 0 -> Error DivideByZero
    | _ -> Ok <| x / y

// CLASS

type MyComplexClass(x: int) =               // class with primary constructor
    member this.X = x                       // property
    member val Y = "" with get, set         // property with get and set
    member this.MyMth() =                   // method
        this.Y <- "executed"
    new () = new MyComplexClass(0)          // secondary constructor
    interface IDisposable with              // implemented interface
        member this.Dispose() = printfn "disposing"
    
let notEqual =                              // it DOES NOT support equality comparing
    new MyComplexClass(5) = new MyComplexClass(5)
let classObj = new MyComplexClass()
(classObj :> IDisposable).Dispose()         // to call interface mth, we need use upcast operator

(* ACTIVE PATTERNS *)

let (|Free|Cheap|Expensive|) input =        // we can create custom pattern matching rules
    match input with                        // it makes code more clear
    | x when x <= 0 -> Free
    | x when x <= 10 -> Cheap
    | _ -> Expensive
    
let isExpensiveThing thing =
    match thing with
    | Free -> "no"
    | Cheap -> "no"
    | Expensive -> "yes"

let (|FLOOR|) (col : double) =              // single case
     floor col |> int
   
let printFloored number =
   match number with
   | FLOOR f -> printfn $"Floored %i{f}"
       
let (|FIRST10CHARS|_|) (str: string) =      // partial active pattern
    if str.Length >= 10
    then Some <| str.Substring(0, 10)
    else None
    
let (|NUMBER|_|) (str: string) =           
    match Int32.TryParse str with
    | true, i -> Some i
    | _ -> None
    
let printChars txt =
    match txt with
    | FIRST10CHARS ch ->
        match ch with
        | NUMBER n -> printfn $"i am at least 10 chars number!: {n}"
        | _ -> printfn $"i'm just a string!"
    | _ -> printfn "no 10 chars"
    
(* TYPE PROVIDERS *)

open FSharp.Data

// type provides allows to design types during code development

// building type using some seed
type Conducted = JsonProvider<""" { "guestName": "Bartosz", "debt": 55 } """>
let parsed = Conducted.Parse(""" { "guestName":"Tomas", "debt":4 } """)

parsed.Debt |> ignore                       // we know that there is Debt field!

type FsharpInfo = HtmlProvider<"https://en.wikipedia.org/wiki/F_Sharp_(programming_language)">
FsharpInfo()
    .Tables.VersionsEdit.Rows |> ignore     // we can access F# versions table from wiki