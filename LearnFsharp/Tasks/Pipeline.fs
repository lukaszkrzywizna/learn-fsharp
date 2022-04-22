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
    failwith "not implemented"

// b) metrics
let metrics operation arg =
    failwith "not implemented"
    
// c) retrying if fail
let retryWhenFail operation arg =
    failwith "not implemented"

// now, create a whole pipeline
let decorated operation arg =
    failwith "not implemented"

// * extra - extend retryWithFail to accept counter value as a argument 
// ** extra - remove dependency for printfn and make it parametrized

// code new variants below: