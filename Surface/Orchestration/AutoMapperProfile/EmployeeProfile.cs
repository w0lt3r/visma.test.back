using AutoMapper;
using Core.Entity;
using Orchestration.Dto.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orchestration.AutoMapperProfile
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeInsertDto, Employee>()
                .ForMember(x => x.Active, m => m.MapFrom(d => true));
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
