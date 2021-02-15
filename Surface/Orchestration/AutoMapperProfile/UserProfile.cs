using AutoMapper;
using Core.Entity;
using Orchestration.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.RoleId, m => m.MapFrom(d => d.RoleId))
                .ForMember(x => x.RoleName, m => m.MapFrom(d => d.Role.Name));
            CreateMap<UserInsertDto, User>()
               .ForMember(x => x.UserName, m => m.MapFrom(d => d.Email))
               .ForMember(x => x.Active, m => m.MapFrom(d => true));
        }
    }
}
