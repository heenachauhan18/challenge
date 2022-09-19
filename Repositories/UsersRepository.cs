using CosmosCRUD.Data;
using CosmosCRUD.Entities;
using CosmosCRUD.Exceptions;

namespace CosmosCRUD.Repositories
{
    public class UsersRepository : IUserRepository
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
            UserEntity user = await cosmosDbService.GetItemAsyncById(emailAddress);

            if(user == null)
            {
                throw new UserNotFoundException(string.Format("User not found with {0} email address", emailAddress));
            }

            return user;
        }
    }
}
