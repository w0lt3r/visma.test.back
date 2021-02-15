using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchestration.Common;
using Orchestration.Dto.User;
using Orchestration.Interface;
using Surface.Model;

namespace Surface.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserOrchestrator _userOrchestrator;
        public UserController(IUserOrchestrator userOrchestrator)
        {
            _userOrchestrator = userOrchestrator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] Paginator<UserFilterDto> paginator)
        {
            var result = await _userOrchestrator.List(paginator);
            return new OkObjectResult(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] List<int> users)
        {
            await _userOrchestrator.Delete(users);
            return new OkObjectResult(new Wrapper<object>("Los usuarios fueron eliminados exitosamente."));
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserInsertDto userInsert)
        {
            await _userOrchestrator.Create(userInsert);
            return new OkObjectResult(new Wrapper<object>("El usuario ha sido creado exitosamente."));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto userUpdate)
        {
            await _userOrchestrator.Update(userUpdate);
            return new OkObjectResult(new Wrapper<object>("El usuario ha sido actualizado exitosamente."));
        }
        [AllowAnonymous]
        [HttpGet("Access")]
        public async Task<IActionResult> GetAccess(string username, string password)
        {
            var result = await _userOrchestrator.Access(username, password);
            return new OkObjectResult(new Wrapper<LoginResponseDto>(null, result));
        }
    }
}
