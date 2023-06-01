using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        //public ICollection<Employee> Employees { get; set; }
    }
}
