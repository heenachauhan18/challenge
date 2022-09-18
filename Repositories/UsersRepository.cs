using CosmosCRUD.Data;
using CosmosCRUD.Entities;
using CosmosCRUD.Exceptions;
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
            UserEntity createdUser = await cosmosDbService.AddItemAsync(entity);

            if (createdUser == null)
            {
                throw new UserAlreadyExistsException("User already exists");
            }

            return createdUser;
        }

        public async Task<UserEntity> GetUserByEmailAddress(string emailAddress)
        {
            return await cosmosDbService.GetItemAsyncById(emailAddress);
            
        }
    }
}
