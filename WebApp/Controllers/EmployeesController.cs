using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Data;
using WebApp.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("employees")]
    [EnableCors("AllowSpecificOrigin")]
    public class EmployeesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmployeesController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: employees
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _context.Employees.Include(x=>x.UserAccounts)
                        .ToList();
            return Ok(new { employees });
        }
        // POST: employee
        [HttpPost]
        public IActionResult CreateNewEmployee([FromBody] EmployeeCommand employeeCommand)
        {
            if (employeeCommand != null)
            {
                User u = new User
                {
                    Username = employeeCommand.FirstName.ToLower(),
                    Password = employeeCommand.FirstName.ToLower() + "123!#",
                    UserType = "user_nurse",
                    Token = System.Guid.NewGuid().ToString()
                };
                Employee newEmployee = new Employee
                {
                    FirstName = employeeCommand.FirstName,
                    LastName = employeeCommand.LastName,
                    Phone = employeeCommand.Phone,
                    Adress = employeeCommand.Adress,
                    Email = employeeCommand.Email,
                    Title = employeeCommand.Title,
                    Position = employeeCommand.Position,
                    DateOfBirth = employeeCommand.DateOfBirth,
                    UserAccounts = new List<User> {
                        u
                    }
                };
              
                _context.Employees.Add(newEmployee);

                _context.SaveChanges();
                return Ok(new { message = "Succesfully created employee" });
            }
            else
                return BadRequest();
        }

        // DELETE: employee {id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeById([FromRoute] int id)
        {
            Employee employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee != null) {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return Ok(new { message = "Succesfully deleted employee" });
            } else
                return BadRequest();
        }
        // PUT: employee
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee([FromRoute] int id, [FromBody] UpdatePatientCommand pacijentCommand)
        {
            if (CheckIfExists(id))
            {
                Patient patientToUpdate = _context.Patients.Where(x => x.Id == id).FirstOrDefault();

                if (patientToUpdate != null)
                {
                    patientToUpdate.FirstName = pacijentCommand.FirstName;
                    patientToUpdate.LastName = pacijentCommand.LastName;
                    patientToUpdate.Phone = pacijentCommand.Phone;
                    patientToUpdate.Adress = pacijentCommand.Adress;
                    patientToUpdate.Email = pacijentCommand.Email;

                    _context.SaveChanges();
                }

                return Ok(new { message = "Succesfully updated patient" });
            }
            else
                return BadRequest(new { message = "Could not find user with provided id" });
        }

        private bool CheckIfExists(int id)
        {
            if (_context.Employees.Any(o => o.Id == id))
            {
                return true;
            }
            else
                return false;
        }

    }
}
