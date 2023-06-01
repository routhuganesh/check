using EmployeeManagementAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI
{
    public class EmployeeDto
    {
        public string Name { get; set; } 
        public int Age { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
