﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Common
{
    [JsonObject]
    public class RepositoryDocument
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("partitionKey")]
        public String PartitionKey { get; set; }
    }

    public class RepositoryDomainObject
    {
        public String Id { get; set; }
    }

    public interface IRepository<TDomain, TDocument> 
        where TDocument: RepositoryDocument 
        where TDomain: RepositoryDomainObject
    {
        TDomain ToDomain(TDocument document);
        TDocument ToDocument(TDomain domain);

        Task<TDomain> Get(String id, String partitionKey);
        Task<IEnumerable<TDomain>> Query(Expression<Func<TDocument, Boolean>> query, string partitionKey);
        Task<Boolean> Delete(String id, String partitionKey);
        Task<Boolean> Upsert(TDomain item);

        Task<Boolean> WithData(List<TDomain> data);
    }
}
