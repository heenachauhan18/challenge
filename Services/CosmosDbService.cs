using CosmosCRUD.Entities;
using Microsoft.Azure.Cosmos;

namespace CosmosCRUD.Data
{
    public class CosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<UserEntity> AddItemAsync(UserEntity userEntity)
        {
            return await this._container.CreateItemAsync<UserEntity>(userEntity, new PartitionKey(userEntity.Id));
        }

        public async Task<UserEntity> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<UserEntity> response = await this._container.ReadItemAsync<UserEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<UserEntity> GetItemsAsync(string queryString)
        {

            var query = this._container.GetItemQueryIterator<UserEntity>(new QueryDefinition(queryString));
            List<UserEntity> results = new List<UserEntity>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results.First<UserEntity>();
        }

        public async Task UpdateItemAsync(string id, UserEntity item)
        {
            await this._container.UpsertItemAsync<UserEntity>(item, new PartitionKey(id));
        }
    }
}
