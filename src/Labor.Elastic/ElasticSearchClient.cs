using Elasticsearch.Net;
using log4net;
using Nest;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Labor.Elastic
{
    public class ElasticSearchClient<TEntity>
        where TEntity : class
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public readonly string IndexName;

        protected readonly IElasticClient Client;

        public ElasticSearchClient(IElasticClient client, string indexName)
        {
            IndexName = indexName;
            Client = client;
        }

        public ElasticSearchClient(ConnectionString connectionString, string indexName)
            : this(CreateClient(connectionString, indexName), indexName)
        {
        }

        public ICreateIndexResponse CreateIndex()
        {
            var response = Client.IndexExists(IndexName);

            if (response.Exists)
            {
                return null;
            }

            return Client.CreateIndex(IndexName, index =>
                index.Mappings(ms =>
                    ms.Map<TEntity>(x => x.AutoMap())));
        }

        public IBulkResponse BulkInsert(IEnumerable<TEntity> entities)
        {
            var request = new BulkDescriptor();

            foreach (var entity in entities)
            {
                request
                    .Index<TEntity>(op => op
                        .Id(Guid.NewGuid().ToString())
                        .Index(IndexName)
                        .Document(entity));
            }

            return Client.Bulk(request);
        }

        public IEnumerable<TEntity> Search(SearchRequest<TEntity> searchRequest)
        {
            var response = Client.Search<TEntity>(searchRequest);

            return response.Documents;
        }

        public IEnumerable<TEntity> Search(Func<SearchDescriptor<TEntity>, ISearchRequest> selector = null)
        {
            var response = Client.Search<TEntity>(selector);

            return response.Documents;
        }

        private static IElasticClient CreateClient(ConnectionString connectionString, string defaultIndex)
        {
            var node = new UriBuilder(connectionString.Scheme, connectionString.Host, connectionString.Port);
            var connectionPool = new SingleNodeConnectionPool(node.Uri);
            var connectionSettings = new ConnectionSettings(connectionPool).DefaultIndex(defaultIndex);

            return new ElasticClient(connectionSettings);
        }
    }
}