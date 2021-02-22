using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Orchestration.Dto.Employee
{
    public class EmployeeInsertDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Social Security Number is required.")]
        public string SocialSecurityNumber { get; set; }
        public string Phone { get; set; }
    }
}
