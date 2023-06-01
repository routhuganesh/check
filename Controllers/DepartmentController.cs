using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DepartmentController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Department>> GetAllDepartment()
        {
            var departments = _dbContext.Departments.ToList();
            return Ok(departments);
        }
        //[HttpGet("{Id}")]
        //public ActionResult<Department> GetDepartmentById(int id)
        //{
        //    var department = _dbContext.Departments.Find(id);
        //    if (department == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(department);
        //}
        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            var department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        [HttpPost]
        public ActionResult<Department> AddDepartment(DepartmentDto department)
        {
            var departmentToAdd = new DepartmentDto()
            {
                Name = department.Name

            };
            var departmentPayload = _mapper.Map<Department>(departmentToAdd);
            _dbContext.Add(departmentPayload);
            _dbContext.SaveChanges();
            return Ok(departmentPayload);
        }
        [HttpPut("{id}")]
        public ActionResult<Department> UpdateDepartment(int id, DepartmentDto department)
        {
            var existingDepartment = _dbContext.Departments.Find(id);
            if (existingDepartment == null)
            {
                return NotFound();
            }
            existingDepartment.Name = department.Name;
            
            _dbContext.Update(existingDepartment);
            _dbContext.SaveChanges();
            return Ok(existingDepartment);

        }

        [HttpDelete]
        public ActionResult<Department> DeleteEmployee(int id)
        {
            var department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            _dbContext.Remove(department);
            _dbContext.SaveChanges();
            return Ok(department);
        }

    }
}
