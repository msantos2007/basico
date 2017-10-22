using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using Basico.Entities;
using Basico.Web.Models;

namespace Basico.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure() //Versao 3.3.1
        {

            Mapper.CreateMap<identityUser, UserEditViewModel>()
                .ForMember(vm => vm.ID, map => map.MapFrom(m => m.ID))
                .ForMember(vm => vm.Username, map => map.MapFrom(m => m.Username))
                .ForMember(vm => vm.Firstname, map => map.MapFrom(m => m.Firstname))
                .ForMember(vm => vm.Email, map => map.MapFrom(m => m.Email))
                .ForMember(vm => vm.DateCreated, map => map.MapFrom(m => m.DateCreated))
                .ForMember(vm => vm.HashedPassword, map => map.MapFrom(m => m.HashedPassword))
                .ForMember(vm => vm.IsLocked, map => map.MapFrom(m => m.IsLocked))
                .ForMember(vm => vm.Salt, map => map.MapFrom(m => m.Salt));

            Mapper.CreateMap<identityRole, UserRoleViewModel>();

            Mapper.CreateMap<identityRole, RoleViewModel>();

        }
    }
}