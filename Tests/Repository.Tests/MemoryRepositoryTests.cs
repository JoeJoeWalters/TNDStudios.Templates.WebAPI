using Repository.Common;
using Xunit;

namespace Repository.Tests
{
    public class MemoryRepositoryTests : RepositoryTestsBase
    {
        public MemoryRepositoryTests() : base()
        {
            _repository = new MemoryRepository<TestDomainObject, TestDocumentObject>(ToDocumentObject, ToDomainObject);
        }

        [Fact, Trait("InMemory", "yes")]
        public override void Add() => base.Add();

        [Fact, Trait("InMemory", "yes")]
        public override void Delete() => base.Delete();
        
        [Fact, Trait("InMemory", "yes")]
        public override void Get() => base.Get();
        
        [Fact, Trait("InMemory", "yes")]
        public override void Query() => base.Query();

        [Fact, Trait("InMemory", "yes")]
        public override void DataLoad() => base.DataLoad();
    }
}
