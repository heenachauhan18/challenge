using AutoMapper;
using CosmosCRUD.DTOs;
using CosmosCRUD.Entities;
using CosmosCRUD.Queries;
using CosmosCRUD.Repositories;
using MediatR;

namespace CosmosCRUD.Handlers
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserResponseDTO>
    {
        private readonly UsersRepository UsersRepository;
        private readonly IMapper Mapper;

        public GetUserByEmailHandler(UsersRepository UsersRepository, IMapper Mapper)
        {
            this.UsersRepository = UsersRepository;
            this.Mapper = Mapper;
        }

        public async Task<UserResponseDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            UserEntity user = await UsersRepository.GetUserByEmailAddress(request.emailAddress);
            return this.Mapper.Map<UserResponseDTO>(user);
        }
    }
}
