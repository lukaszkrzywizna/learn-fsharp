namespace FSharpWorkshop.Implementation.Finances

module Operators =

    let (+!) (x: Money) (y: Money): Money =
        failwith "not implemented"

    let (+?) (x: Money) (y: Money): Money option =
        failwith "not implemented"

    let (+?!) (x: Money) (y: Money): Result<Money, string> =
        failwith "not implemented"
    
    let (+++) (balance: Balance) (value: Money): Balance =
        failwith "not implemented"