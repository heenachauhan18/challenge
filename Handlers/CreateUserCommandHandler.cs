using AutoMapper;
using CosmosCRUD.Commands;
using CosmosCRUD.DTOs;
using CosmosCRUD.Entities;
using CosmosCRUD.Repositories;
using MediatR;

namespace CosmosCRUD.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDTO>
    {
        private readonly UsersRepository usersRepository;
        private readonly IMapper Mapper;

        public CreateUserCommandHandler(UsersRepository usersRepository, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            Mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity entity = Mapper.Map<UserEntity>(request.UserRequestDTO);
            UserEntity entityResponse = await this.usersRepository.CreateUser(entity);
            return Mapper.Map<UserResponseDTO>(entityResponse);
        }
    }
}
