namespace Example.Sql.Tests

open Microsoft.Extensions.Logging

open Xunit
open Xunit.Abstractions

open Example.Sql
open Microsoft.Data.Sqlite

type ConnectorShould( oh: ITestOutputHelper ) =
    
    let logger =
        Logging.CreateLogger oh
        
    [<Fact>]
    member this.``BeCreateable`` () =
        
        use sut =
            DbConnector.Make( new SqliteConnection( SqliteSpecification.Default.ConnectionString ) )
            
        logger.LogInformation( "{ConnectionString}", sut.ConnectionString )
        
        Assert.True( sut.ConnectionString.Length > 0 )

        sut.Open()
        
        let cmd =
            sut.CreateComand() 
            
        sut.Close()
        
        Assert.True( true )
