using CosmosCRUD.Entities;
using CosmosCRUD.Exceptions;
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

        public async Task<UserEntity?> AddItemAsync(UserEntity userEntity)
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
            catch (Exception)
            {
                throw new InterenalServerException("Something went wrong");
            }

        }

        public async Task<UserEntity?> GetItemAsyncById(string emailId)
        {
            try
            {
                ItemResponse<UserEntity> response = await this._container
                    .ReadItemAsync<UserEntity>(emailId, new PartitionKey(emailId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception)
            {
                throw new InterenalServerException("Something went wrong");
            }

        }

        public async Task<UserEntity> UpdateItemAsync(string emailId, UserEntity item)
        {
            try
            {
                return await this._container.UpsertItemAsync<UserEntity>(item, new PartitionKey(emailId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception)
            {
                throw new InterenalServerException("Something went wrong");
            }
        }

        public async Task<bool> IsHealthy()
        {
            try
            {
                await _container.ReadContainerAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
