namespace Example.Sql.Tests

open Xunit
open Xunit.Abstractions

open Example.Sql
open Microsoft.Data.Sqlite

type ConnectionShould( oh: ITestOutputHelper ) =
    
    let logger =
        Logging.CreateLogger oh
        
    [<Fact>]
    member this.``BeCreateableAndExecuteSimpleQuery-Sqlite`` () =
        
        use sut =
            
            let connector =
                DbConnector.Make( new SqliteConnection( SqliteSpecification.Default.ConnectionString ) )
                
            DbConnection.Make( connector )
            
        Assert.True( sut.ConnectionString.Length > 0 )

        sut.Open()
        
        let cmd =
            sut.CreateCommand "SELECT date('now')" Seq.empty
            
        let reader = cmd.ExecuteReader()
        
        Assert.True( reader.HasRows )
        
        reader.Close()
        
        
        
     
        