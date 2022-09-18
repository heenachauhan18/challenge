using CosmosCRUD.DTOs;
using MediatR;

namespace CosmosCRUD.Queries
{
    public record GetUserByEmailQuery(string emailAddress) : IRequest<UserResponseDTO>;
}
