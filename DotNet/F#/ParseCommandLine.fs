module ParseCommandLine
    
    open System

    type Parameter =
        | SwitchMissing
        | SwitchExistsNoValue
        | SwitchExistsValuePresentFailsValidation
        | SwitchExistsValuePresentPassesValidation of string

    let parseParameter (commandName: string) (validator: string -> bool) (commands: string[]) : Parameter =
        match Array.tryFindIndex (fun arg -> arg = commandName) commands with
            | Some idx ->
                try
                    let value = commands.[idx + 1]
                    if value.StartsWith "-" then
                        SwitchExistsNoValue
                    else                
                        match validator value with
                            | true -> SwitchExistsValuePresentPassesValidation(value)
                            | false -> SwitchExistsValuePresentFailsValidation
                with
                    | :? IndexOutOfRangeException -> SwitchExistsNoValue
            | None -> SwitchMissing

    type Toggle =
        | SwitchMissing
        | SwitchExists

    let parseToggle (commandName: string) (commands: string[]) : Toggle =
        match Array.tryFind (fun arg -> arg = commandName) commands with
            | Some _ -> SwitchExists
            | None -> SwitchMissing