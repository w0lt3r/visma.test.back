using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orchestration.Common;
using Orchestration.Dto.Employee;
using Orchestration.Interface;
using Surface.Model;

namespace Surface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeOrchestrator _employeeOrchestrator;
        public EmployeeController(IEmployeeOrchestrator employeeOrchestrator)
        {
            _employeeOrchestrator = employeeOrchestrator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeInsertDto employee)
        {
            var response = await _employeeOrchestrator.Add(employee);
            return new OkObjectResult(new Wrapper<EmployeeDto>("The employee was created successfully", response));
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] Paginator<EmployeeFilterDto> paginator)
        {
            var response = await _employeeOrchestrator.List(paginator);
            return new OkObjectResult(response);
        }
    }
}
