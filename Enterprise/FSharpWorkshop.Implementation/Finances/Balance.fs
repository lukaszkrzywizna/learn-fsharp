namespace FSharpWorkshop.Implementation.Finances

type Balance (values: Map<Currency, decimal>) =
    member private _._values =
        values
        |> Map.map (fun currency amount -> { Amount = amount; Currency = currency })
    member private this._update (value: Money) fChange =
        this._values
        |> Map.change value.Currency fChange
        |> Map.map (fun _ money -> money.Amount)
        |> Balance
    member this.add (value: Money) =
        this._update value (fun current ->
            current
            |> Option.map (fun currentValue -> { value with Amount = currentValue.Amount + value.Amount })
            |> Option.orElseWith (fun () -> Some value))
    static member empty =
        []
        |> Map.ofList
        |> Balance
    static member toList (balance: Balance): Money list =
        balance._values
        |> Map.toList
        |> List.map snd