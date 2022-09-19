using CosmosCRUD.Entities;

namespace CosmosCRUD.Repositories
{
    public interface IUserRepository
    {
        public Task<UserEntity> CreateUser(UserEntity entity);

        public Task<UserEntity?> GetUserByEmailAddress(string emailAddress);
    }
}
