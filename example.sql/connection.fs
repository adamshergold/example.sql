namespace Example.Sql

type DbConnection( connector : DbConnector ) =
    
    static member Make( connector ) =
        new DbConnection( connector ) :> IDbConnection

    member this.Dispose () =
        connector.Dispose()
        
    member this.ConnectionString
        with get () = connector.ConnectionString

    member this.Open () =
        connector.Open()

    member this.Close () =
        connector.Close()
            
    member this.CreateCommand (text:string) (ps:seq<string*obj>): System.Data.Common.DbCommand =
        
        let cmd =
            connector.CreateComand()
            
        cmd.CommandText <- text
        
        ps |> Seq.iter ( fun (name,v) ->
            let p = cmd.CreateParameter()
            p.ParameterName <- name
            p.Value <- v
            cmd.Parameters.Add( p ) |> ignore )
        
        cmd

    interface System.IDisposable
        with
            member this.Dispose () =
                this.Dispose()
                
    interface IDbConnection
        with
            member this.ConnectionString =
                this.ConnectionString
                
            member this.Open () =
                this.Open()
                
            member this.CreateCommand text ps =
                this.CreateCommand text ps