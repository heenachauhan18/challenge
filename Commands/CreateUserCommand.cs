using CosmosCRUD.DTOs;
using MediatR;

namespace CosmosCRUD.Commands
{
    public record CreateUserCommand(UserRequestDTO UserRequestDTO) : IRequest<UserResponseDTO>;
}
