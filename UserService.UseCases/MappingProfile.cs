using AutoMapper;
using MSACommerce.UserService.Core.Entites;
using UserService.UseCases.Commands;
using UserService.UseCases.Queries;

namespace UserService.UseCases
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, TbUser>();

            CreateMap<TbUser, UserDto>();
        }
    }

}
