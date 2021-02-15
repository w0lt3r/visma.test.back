using Core.Entity;
using Core.Interface;
using FluentValidation;
using LinqKit;
using SharedKernel.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IRepository<User, int> _userRepository;
        public UserValidator(IRepository<User, int> userRepository, int action)
        {
            _userRepository = userRepository;
            RuleFor(x => x).MustAsync((user, cancel) => { return EmailMustBeUnique(user, action); })
                    .WithMessage("El email ya está siendo usado.");
        }
        private async Task<bool> EmailMustBeUnique(User user, int action)
        {
            var predicate = PredicateBuilder.New<User>();
            predicate = predicate.And(x => x.Email == user.Email);
            if (action == (int)CRUDActionEnum.Update) predicate = predicate.And(x => x.Id != user.Id);
            return await _userRepository.CountAsync(predicate) == 0;
        }
    }
}
