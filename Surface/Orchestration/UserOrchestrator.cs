using Auth;
using AutoMapper.QueryableExtensions;
using Core.Entity;
using Core.Interface;
using Core.Validator;
using FluentValidation.Results;
using LinqKit;
using Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Orchestration.Common;
using Orchestration.Dto;
using Orchestration.Dto.User;
using Orchestration.Interface;
using SharedKernel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Orchestration
{
    public class UserOrchestrator : BasicOrchestrator, IUserOrchestrator
    {
        private readonly IRepository<User, int> _userRepository;
        private readonly IConfiguration _configuration;
        public UserOrchestrator(IServiceProvider serviceProvider,
            IConfiguration configuration,
            IRepository<User, int> userRepository) : base(serviceProvider)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<Pagination<UserDto>> List(Paginator<UserFilterDto> paginator)
        {
            var paginationResult = new Pagination<UserDto>();
            var predicate = PredicateBuilder.New<User>();
            predicate.DefaultExpression = t => true;
            if (paginator.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(paginator.Filter.Name)) predicate = predicate.And(x => x.Name.StartsWith(paginator.Filter.Name));
                if (!string.IsNullOrWhiteSpace(paginator.Filter.LastName)) predicate = predicate.And(x => x.LastName.StartsWith(paginator.Filter.LastName));
                if (!string.IsNullOrWhiteSpace(paginator.Filter.Email)) predicate = predicate.And(x => x.Email.StartsWith(paginator.Filter.Email));
                if (!string.IsNullOrWhiteSpace(paginator.Filter.Phone)) predicate = predicate.And(x => x.Phone.StartsWith(paginator.Filter.Phone));
                if (paginator.Filter.Role != null) predicate = predicate.And(x => x.RoleId == paginator.Filter.Role);
                if (paginator.Filter.Active != null) predicate = predicate.And(x => x.Active == paginator.Filter.Active);
            }

            var users = await _userRepository.Find(predicate).Include(x => x.Role).ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
            paginationResult.TotalCount = users.Count;
            paginationResult.Elements = users.Skip(paginator.Page * paginator.Count)
                .Take(paginator.Count).ToList();
            return paginationResult;
        }
        public async Task Create(UserInsertDto userInsert)
        {
            var entity = _mapper.Map<User>(userInsert);
            var validationResult = await _userRepository.AddAsync(entity, new UserValidator(_userRepository, (int)CRUDActionEnum.Create));
            if (validationResult.IsValid) await _unitOfWork.SaveChangesAsync(CurrentUser.Id);
            else throw new ControlledException(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
        }
        public async Task Update(UserUpdateDto userUpdate)
        {
            var user = await _userRepository.Find(x => x.Id == userUpdate.Id).SingleOrDefaultAsync();
            user.Name = userUpdate.Name;
            user.LastName = userUpdate.LastName;
            user.Email = userUpdate.Email;
            user.UserName = userUpdate.Email;
            user.Phone = userUpdate.Phone;
            user.RoleId = userUpdate.RoleId;
            user.Active = userUpdate.Active;
            if (userUpdate.Password != null) user.Password = userUpdate.Password;
            var validationResult = await _userRepository.UpdateAsync(user, new UserValidator(_userRepository, (int)CRUDActionEnum.Update));
            if (validationResult.IsValid) await _unitOfWork.SaveChangesAsync(CurrentUser.Id);
            else throw new ControlledException(validationResult.Errors.Select(x => x.ErrorMessage).ToList(), HttpStatusCode.BadRequest);
        }
        public async Task<LoginResponseDto> Access(string userName, string password)
        {
            //var user = await _userRepository.Find(x => x.UserName == userName && x.Password == password && x.Active)
            //    .Include(x => x.Role).SingleOrDefaultAsync();
            var user = await _userRepository.Find(x => x.UserName == userName && x.Password == password && x.Active)
                .Include(x => x.Role).SingleOrDefaultAsync();
            if (userName == "admin@gmail.com" && password == "123456")
            {
                var jwtFactory = new JwtFactory(_configuration);
                return new LoginResponseDto { Token = await jwtFactory.GenerateJwt(user.Id, user.UserName, user.Role.Id, user.Role.Name) };
            }
            else throw new ControlledException(new List<string> { "The email and password combination does not match any account." }, HttpStatusCode.BadRequest);
        }
        public async Task Delete(List<int> users)
        {
            var foundUsers = await _userRepository.Find(x => users.Contains(x.Id)).ToListAsync();
            await _userRepository.BulkDeleteAsync(foundUsers);
        }
    }
}
