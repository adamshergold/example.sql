namespace Example.Sql.Tests

open Xunit
open Xunit.Abstractions

open Example.Sql


type FactoryShould( oh: ITestOutputHelper ) =
    
    let logger =
        Logging.CreateLogger oh
        
    [<Fact>]
    member this.``AllowConnectionToBeEstablished`` () =
        
        use sut =
            DbConnectionFactory.Make( logger )
            
        let spec =
            DbConnectionSpecification.Sqlite( SqliteSpecification.Default )
            
        sut.Create "test" spec |> ignore 

        let connection =
            sut.Lookup "test"

        connection.Check()
        
        let cmd =
            connection.CreateCommand "SELECT date('now')" Seq.empty
            
        let reader = cmd.ExecuteReader()
        
        Assert.True( reader.HasRows )
        
        reader.Close()
        
        
