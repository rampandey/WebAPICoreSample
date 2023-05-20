using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICoreSample.Interface;
using WebAPICoreSample.Models;

namespace WebAPICoreSample.Controllers
{
    /// <summary>
    /// employee 
    /// </summary>
    [Authorize]
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployees _IEmployee;

        public EmployeeController(IEmployees IEmployee)
        {
            _IEmployee = IEmployee;
        }

        /// <summary>
        /// get employee list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            return await Task.FromResult(_IEmployee.GetEmployeeDetails());
        }

        /// <summary>
        /// get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employees = await Task.FromResult(_IEmployee.GetEmployeeDetails(id));
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        /// <summary>
        /// add new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            _IEmployee.AddEmployee(employee);
            return await Task.FromResult(employee);
        }

        /// <summary>
        /// update employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Put(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }
            try
            {
                _IEmployee.UpdateEmployee(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(employee);
        }

        /// <summary>
        /// delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> Delete(int id)
        {
            var employee = _IEmployee.DeleteEmployee(id);
            return await Task.FromResult(employee);
        }

        /// <summary>
        /// employee exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool EmployeeExists(int id)
        {
            return _IEmployee.CheckEmployee(id);
        }
    }
}
