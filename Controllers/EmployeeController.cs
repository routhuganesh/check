using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        //[HttpGet]
        //public ActionResult<List<Employee>> GetAllEmployee()
        //{
        //    var employees = _dbContext.Departments.ToList();
        //    return Ok(employees);
        //}
        //[HttpGet("Id")]
        //public ActionResult<Employee> GetEmployeeById(int id)
        //{
        //    var employee = _dbContext.Departments.Find(id);
        //    if(employee != null)
        //    {
        //        return Ok(employee);
        //    }
        //    return BadRequest();
        //}
        //[HttpPost]
        //public ActionResult<Employee> AddEmployee( [FromBody] Employee employeeToAdd)
        //{
        //    //var employeeToAdd = new Employee()
        //    //{
        //    //    Name = employee.Name,
        //    //    Age = employee.Age,
        //    //    Salary = employee.Salary
        //    //};

        //    _dbContext.Add(employeeToAdd);
        //    _dbContext.SaveChanges();
        //    return Ok(employeeToAdd);
        //}
        //[HttpPut]
        //public ActionResult<Employee> UpdateEmployee(int id,Employee employee)
        //{
        //    var existingEmployee = _dbContext.Employees.Find(id);
        //    if(existingEmployee == null)
        //    {
        //        return NotFound();
        //    }
        //    existingEmployee.Name = employee.Name;
        //    existingEmployee.Salary = employee.Salary;
        //    existingEmployee.Age = employee.Age;
        //    existingEmployee.DepartmentId = employee.DepartmentId;
        //    _dbContext.Update(existingEmployee);
        //    _dbContext.SaveChanges();
        //    return Ok(existingEmployee);

        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        //{
        //    return await _context.Employees.Include(e => e.Department).ToListAsync();
        //}

        //// GET: api/Employee/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(int id)
        //{
        //    var employee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        //// POST: api/Employee
        //[HttpPost]
        //public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        //}

        //// PUT: api/Employee/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
        //[HttpDelete]
        //public ActionResult<Employee> DeleteEmployee(int id)
        //{
        //    var employee = _context.Departments.Find(id);
        //    if(employee == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Remove(employee);
        //    _context.SaveChanges();
        //    return Ok(employee);
        //}
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _context.Employees
            .Include(_ => _.Department).ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto employeePayload)
        {
            var newEmployee = _mapper.Map<Employee>(employeePayload);
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return Created($"/{newEmployee.Id}", newEmployee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UPdateById(int id, EmployeeDto employeeToUpdate)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = employeeToUpdate.Name;
            employee.Age = employeeToUpdate.Age;
            employee.Salary = employeeToUpdate.Salary;
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteById(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            else
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return Ok(employee);
            }
        }

    }
}
