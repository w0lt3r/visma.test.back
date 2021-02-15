using FluentValidation.Results;
using Orchestration.Common;
using Orchestration.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orchestration.Interface
{
    public interface IUserOrchestrator
    {
        Task<Pagination<UserDto>> List(Paginator<UserFilterDto> paginator);
        Task<LoginResponseDto> Access(string userName, string password);
        Task Create(UserInsertDto userInsert);
        Task Update(UserUpdateDto userUpdate);
        Task Delete(List<int> users);
    }
}
