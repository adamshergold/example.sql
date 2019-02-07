namespace Example.Sql

module Utilities =
    
    let TryEnvironmentVariable (defaultTo:string) (name:string) =
        
        let ev =
            System.Environment.GetEnvironmentVariable(name)
            
        if System.String.IsNullOrWhiteSpace(name) then defaultTo else ev            
