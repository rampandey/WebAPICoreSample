using WebAPICoreSample.Interface;
using WebAPICoreSample.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPICoreSample.Repository
{
    public class EmployeeRepository : IEmployees
    {
        readonly DatabaseContext _dbContext = new();
        public EmployeeRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Employee> GetEmployeeDetails()
        {
            try
            {
                return _dbContext.Employees.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Employee GetEmployeeDetails(int id)
        {
            try
            {
                Employee? employee = _dbContext.Employees.Find(id);
                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                _dbContext.Entry(employee).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Employee DeleteEmployee(int id)
        {
            try
            {
                Employee? employee = _dbContext.Employees.Find(id);

                if (employee != null)
                {
                    _dbContext.Employees.Remove(employee);
                    _dbContext.SaveChanges();
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckEmployee(int id)
        {
            return _dbContext.Employees.Any(e => e.EmployeeID == id);
        }
    }
}
