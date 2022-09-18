using CosmosCRUD.DTOs;

namespace CosmosCRUD.Services
{
    public interface IUsersService
    {
        Task<UserResponseDTO> CreateUser(UserRequestDTO usersDTO);
        Task<UserResponseDTO> GetUser(string emailAddress);
    }
}
