module LearnFsharp.Tasks.Pipeline

(*

    We want to compose pipeline which for every provided operation and it's argument decorate it with:
    a) logger - which log every input and output
    
    b) simple metrics - it will measure execution time and log it
       hint: use System.Diagnostics.Stopwatch
    
    c) retrying if fail: when provided operation throws error, it will retry it up to 5 times. 
                         if still fails, log and re-throw exception
       hint - it should be done with recursion
       
    For example: operation is (fun x -> x * x) and arg is 5
 *)

// a) logger 
let logger operation arg =
    printfn $"arg: {arg}"
    let result = operation arg
    printfn $"result: {result}"
    result

// b) metrics
let metrics operation arg =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let result = operation arg
    printfn $"execution time: {sw.ElapsedTicks}"
    result
    
// c) retrying if fail 
let retryWhenFail operation arg =
    let rec retry counter =
        match counter with
        | 5 -> operation arg
        | _ ->
            try
                operation arg
            with
            | ex ->
                printfn $"failed retry number: {counter}"
                retry (counter + 1)
    retry 0

// now, create a whole pipeline
let decorated operation arg =
    let decorators = [retryWhenFail; logger; metrics] |> List.reduce (>>)
    let handle = operation |> decorators
    arg |> handle

// * extra - extend retryWithFail to accept counter value as a argument 
// ** extra - remove dependency for printfn and make it parametrized

// code new variants below:

// a) logger 
let extraLogger log operation arg =
    log $"arg: {arg}"
    let result = operation arg
    log $"result: {result}"
    result

// b) metrics
let extraMetrics log operation arg =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let result = operation arg
    log $"execution time: {sw.ElapsedTicks}"
    result
    
// c) retrying if fail 
let extraRetryWhenFail log counter operation arg =
    let rec retry rest =
        match rest with
        | _ when rest = counter -> operation arg
        | _ ->
            try
                operation arg
            with
            | ex ->
                log $"failed retry number: {rest}"
                retry (rest + 1)
    retry 0

let extraDecorated log operation arg =
    let decorators = (extraRetryWhenFail log 10) >> (extraLogger log) >> (extraMetrics log)
    let handle = operation |> decorators
    arg |> handle