using Orchestration.Common;
using Orchestration.Dto.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orchestration.Interface
{
    public interface IEmployeeOrchestrator
    {
        Task<EmployeeDto> Add(EmployeeInsertDto employee);
        Task<Pagination<EmployeeDto>> List(Paginator<EmployeeFilterDto> data);
    }
}
