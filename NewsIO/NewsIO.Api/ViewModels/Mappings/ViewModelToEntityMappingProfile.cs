using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewsIO.Data.Models.Account;

namespace NewsIO.Api.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, User>()
                .ForMember(u => u.UserName, map => map.MapFrom(vm => vm.UserName));

            CreateMap<User, AppUserViewModel>()
                .ReverseMap();

            CreateMap<User, AppUserViewModel>()
                .ReverseMap();

            CreateMap<IdentityRole, AppUserViewModel>()
                .ForMember(u => u.RoleId, map => map.MapFrom(vm => vm.Id))
                .ForMember(u => u.RoleName, map => map.MapFrom(vm => vm.Name))
                .ReverseMap();

            CreateMap<RoleViewModel, IdentityRole>()
                .ReverseMap();
        }
    }
}
