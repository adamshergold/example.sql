namespace Example.Sql

open Microsoft.Data.Sqlite
open System.Data.SqlClient
open MySql.Data.MySqlClient

type DbConnector =
    | Sqlite of SqliteConnection
    | MySql of MySqlConnection
    | SqlServer of SqlConnection
with
    static member Make( c: SqliteConnection ) =
        DbConnector.Sqlite( c )
            
    static member Make( c: MySqlConnection ) =
        DbConnector.MySql( c )

    static member Make( c: SqlConnection ) =
        DbConnector.SqlServer( c )
    
    member this.ConnectorType
        with get () =
            match this with
            | Sqlite(_) -> "sqlite"
            | MySql(_) -> "mysql"
            | SqlServer(_) -> "sqlserver"
            
    member this.Dispose () =
        this.Close()
    
    member this.Open () =
        match this with
        | Sqlite(c) -> c.Open()
        | MySql(c) -> c.Open()
        | SqlServer(c) -> c.Open()

    member this.Check () =
        if this.State() <> System.Data.ConnectionState.Open then
            this.Open()
    
    member this.State () =
        match this with
        | Sqlite(c) -> c.State
        | MySql(c) -> c.State
        | SqlServer(c) -> c.State
    
    member this.Close () =
        match this with
        | Sqlite(c) -> c.Close()
        | MySql(c) -> c.Close()
        | SqlServer(c) -> c.Close()
            
    member this.ConnectionString
        with get () =
            match this with
            | Sqlite(c) -> c.ConnectionString
            | MySql(c) -> c.ConnectionString
            | SqlServer(c) -> c.ConnectionString
            
    member this.CreateComand () =
        match this with
        | Sqlite(c) -> c.CreateCommand() :> System.Data.Common.DbCommand
        | MySql(c) -> c.CreateCommand() :> System.Data.Common.DbCommand
        | SqlServer(c) -> c.CreateCommand() :> System.Data.Common.DbCommand
            
    interface System.IDisposable
        with
            member this.Dispose () =
                this.Dispose()
