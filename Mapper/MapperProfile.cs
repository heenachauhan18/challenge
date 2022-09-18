using AutoMapper;
using CosmosCRUD.DTOs;
using CosmosCRUD.Entities;

namespace CosmosCRUD.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRequestDTO, UserEntity>();
            CreateMap<UserEntity, UserResponseDTO>()
                .ForMember(user => user.userId, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(user => user.name, opt => opt.MapFrom(entity => string.Format("{0} {1} {2}",
                entity.FirstName, entity.MiddleName, entity.LastName)));
        }
    }
}
