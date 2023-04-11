using AutoMapper;
using MyApp.Models;
using MyApp.WebMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyApp.WebMS
{
    public class MappingProfile:Profile 
    {
        protected override void Configure()
        {
            CreateMap<User, UserListItemViewModel>();
            CreateMap<UserListItemViewModel, User>();
            CreateMap<User, UserListItemViewModel>();
            CreateMap<IEnumerable<User>, UserListViewModel>()
       .ForMember(dest => dest.Items, opt => opt.MapFrom(src => Mapper.Map<IEnumerable<UserListItemViewModel>>(src)));

        }
    }
}