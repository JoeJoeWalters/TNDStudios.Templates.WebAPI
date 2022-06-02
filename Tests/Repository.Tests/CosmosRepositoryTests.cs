using Xunit;

namespace Repository.Tests
{
    public class CosmosRepositoryTests : RepositoryTestsBase
    {
        private const string _connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string _databaseName = "testDatabase";
        private const string _collectionName = "testCollection";
        private const string _partitionKey = "/partitionKey";

        public CosmosRepositoryTests() : base()
        {
            _repository = new CosmosRepository<TestDomainObject, TestDocumentObject>
                (ToDocumentObject, ToDomainObject, 
                _connectionString, _databaseName, 
                _collectionName, _partitionKey);
        }

        [Fact, Trait("InMemory", "no")]
        public override void Add() => base.Add();

        [Fact, Trait("InMemory", "no")]
        public override void Delete() => base.Delete();

        [Fact, Trait("InMemory", "no")]
        public override void Get() => base.Get();

        [Fact, Trait("InMemory", "no")]
        public override void Query() => base.Query();

        [Fact, Trait("InMemory", "no")]
        public override void DataLoad() => base.DataLoad();
    }
}
