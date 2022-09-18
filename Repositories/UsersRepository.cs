using CosmosCRUD.Data;
using CosmosCRUD.Entities;
using Microsoft.Azure.Cosmos;

namespace CosmosCRUD.Repositories
{
    public class UsersRepository
    {
        private readonly CosmosDbService cosmosDbService;

        public UsersRepository(CosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        public async Task<UserEntity> CreateUser(UserEntity entity)
        {
            return await cosmosDbService.AddItemAsync(entity);
        }

        public async Task<UserEntity> GetUserByEmailAddress(string emailAddress)
        {
            string queryString = string.Format("Select * from c WHERE c.EmailAddress= \"{0}\"", emailAddress);
            return await cosmosDbService.GetItemsAsync(queryString);
            
        }
    }
}
