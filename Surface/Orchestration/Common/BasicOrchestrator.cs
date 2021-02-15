using AutoMapper;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Auth;

namespace Orchestration.Common
{
    public class BasicOrchestrator
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUser CurrentUser;

        public BasicOrchestrator(IServiceProvider serviceProvider)
        {
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _mapper = serviceProvider.GetService<IMapper>();
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();


            if (_httpContextAccessor.HttpContext.User.Claims.Count() > 4
                )
            {

                CurrentUser = new AppUser(
                    int.Parse(_httpContextAccessor.HttpContext.User.Claims.ElementAt(0).Value.ToString()),
                    new AppUserRole(int.Parse(_httpContextAccessor.HttpContext.User.Claims.ElementAt(2).Value.ToString()),
                    _httpContextAccessor.HttpContext.User.Claims.ElementAt(3).Value.ToString()),
                    _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            }
        }
    }
}
