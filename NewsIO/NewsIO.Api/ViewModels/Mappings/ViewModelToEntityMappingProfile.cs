using AutoMapper;
using NewsIO.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
