using AutoMapper.QueryableExtensions;
using Core.Entity;
using Core.Interface;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Orchestration.Common;
using Orchestration.Dto.Employee;
using Orchestration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestration
{
    public class EmployeeOrchestrator : BasicOrchestrator, IEmployeeOrchestrator
    {
        private IRepository<Employee, int> _employeeRepository;
        public EmployeeOrchestrator(IServiceProvider serviceProvider,
            IRepository<Employee, int> employeeRepository) : base(serviceProvider)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> Add(EmployeeInsertDto employee)
        {
            var entity = _mapper.Map<Employee>(employee);
            var validationResult = await _employeeRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(entity);
        }

        public async Task<Pagination<EmployeeDto>> List(Paginator<EmployeeFilterDto> paginator)
        {
            var paginationResult = new Pagination<EmployeeDto>();
            var predicate = PredicateBuilder.New<Employee>();
            predicate.DefaultExpression = t => true;
            if (paginator.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(paginator.Filter.FirstName)) predicate = predicate.And(x => x.FirstName.StartsWith(paginator.Filter.FirstName));
                if (!string.IsNullOrWhiteSpace(paginator.Filter.LastName)) predicate = predicate.And(x => x.LastName.StartsWith(paginator.Filter.LastName));
                if (!string.IsNullOrWhiteSpace(paginator.Filter.SocialSecurityNumber)) predicate = predicate.And(x => x.SocialSecurityNumber.StartsWith(paginator.Filter.SocialSecurityNumber));
                if (!string.IsNullOrWhiteSpace(paginator.Filter.Phone)) predicate = predicate.And(x => x.Phone.StartsWith(paginator.Filter.Phone));
            }
            var employees = await _employeeRepository.Find(predicate).ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync();
            paginationResult.TotalCount = employees.Count;
            paginationResult.Elements = employees.Skip(paginator.Page * paginator.Count)
                .Take(paginator.Count).ToList();
            return paginationResult;
        }
    }
}
