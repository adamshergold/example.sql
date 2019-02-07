namespace Example.Sql

open Microsoft.Data.Sqlite
open System.Data.SqlClient
open MySql.Data.MySqlClient

type DbConnector =
    | Sqlite of SqliteConnection
    | MySql of MySqlConnection
with
    static member Make( c: SqliteConnection ) =
        DbConnector.Sqlite( c )
            
    static member Make( c: MySqlConnection ) =
        DbConnector.MySql( c )

    member this.Dispose () =
        this.Close()
    
    member this.Open () =
        match this with
        | Sqlite(c) -> c.Open()
        | MySql(c) -> c.Open()

    member this.Close () =
        match this with
        | Sqlite(c) -> c.Close()
        | MySql(c) -> c.Close()
            
    member this.ConnectionString
        with get () =
            match this with
            | Sqlite(c) -> c.ConnectionString
            | MySql(c) -> c.ConnectionString
            
    member this.CreateComand () =
        match this with
        | Sqlite(c) -> c.CreateCommand() :> System.Data.Common.DbCommand
        | MySql(c) -> c.CreateCommand() :> System.Data.Common.DbCommand
            
    interface System.IDisposable
        with
            member this.Dispose () =
                this.Dispose()
                