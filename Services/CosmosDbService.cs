using CosmosCRUD.Entities;
using CosmosCRUD.Exceptions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

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
            try
            {
                return await this._container.CreateItemAsync<UserEntity>
                (userEntity, new PartitionKey(userEntity.EmailAddress));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return null;
            }

        }

        public async Task<UserEntity> GetItemAsyncById(string id)
        {
            try
            {
                ItemResponse<UserEntity> response = await this._container
                    .ReadItemAsync<UserEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }
        
        public async Task UpdateItemAsync(string id, UserEntity item)
        {
            await this._container.UpsertItemAsync<UserEntity>(item, new PartitionKey(id));
        }
    }
}
