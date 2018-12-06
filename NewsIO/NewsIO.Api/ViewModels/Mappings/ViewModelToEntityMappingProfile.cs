using AutoMapper;
using NewsIO.Data.Models.User;

namespace NewsIO.Api.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, User>().ForMember(u => u.UserName, map => map.MapFrom(vm => vm.UserName));
        }
    }
}
