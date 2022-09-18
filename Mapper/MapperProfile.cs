using AutoMapper;
using CosmosCRUD.DTOs;
using CosmosCRUD.Entities;

namespace CosmosCRUD.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRequestDTO, UserEntity>().ForMember(user => user.EmailAddressId,
                opt => opt.MapFrom(user => user.EmailAddress));

            CreateMap<UserEntity, UserResponseDTO>()
                .ForMember(user => user.userId, opt => opt.MapFrom(entity => entity.Id))
                .ForMember(user => user.name, opt => opt.MapFrom(entity => string.Format("{0} {1} {2}",
                entity.FirstName, entity.MiddleName, entity.LastName)));
        }
    }
}
