using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IDbContextSeeder
    {
        Task SeedAsync(ApplicationDbContext context);
    }
    public class ApplicationDbContextSeeder : IDbContextSeeder
    {
        public async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Departments.Any())
            {
                var departments = new List<Department>
            {
                new Department { Name = "Marketing" },
                new Department { Name = "Sales" },
                new Department { Name = "Finance" }
            };

                await context.Departments.AddRangeAsync(departments);
                await context.SaveChangesAsync();
            }

            if (!context.Employees.Any())
            {
                var department = context.Departments.FirstOrDefault(d => d.Name == "Marketing");

                var employees = new List<Employee>
            {
                new Employee { Name = "John", Age = 22, Salary = 123456789, DepartmentId = department.Id },
                new Employee { Name = "Jane", Age = 25, Salary = 987654321, DepartmentId = department.Id }
            };

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}
