using CosmosCRUD.Commands;
using CosmosCRUD.DTOs;
using CosmosCRUD.Queries;
using MediatR;

namespace CosmosCRUD.Services
{
    public class UsersServiceImpl : IUsersService
    {
        private readonly IMediator mediator;

        public UsersServiceImpl(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<UserResponseDTO> CreateUser(UserRequestDTO usersDTO)
        {
            return mediator.Send(new CreateUserCommand(usersDTO));
        }

        public Task<UserResponseDTO> GetUser(string emailAddress)
        {
            return mediator.Send(new GetUserByEmailQuery(emailAddress));
        }
    }
}
