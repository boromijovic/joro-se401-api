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
    [Route("patients")]
    [EnableCors("AllowSpecificOrigin")]
    public class PatientsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PatientsController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: pacijenti
        [HttpGet]
        public IActionResult GetAll()
        {
            var pacijenti = _context.Patients
                .Include(x => x.Card)
                        .ToList();
            return Ok(new {pacijenti});
        }
        // POST: pacijenti
        [HttpPost]
        public IActionResult CreateNewPacijent([FromBody] PatientCommand pacijentCommand)
        {
            if (pacijentCommand != null)
            {
                Patient newPacijent = new Patient
                {
                    FirstName = pacijentCommand.FirstName,
                    LastName = pacijentCommand.LastName,
                    LBO = pacijentCommand.LBO,
                    Phone = pacijentCommand.Phone,
                    Adress = pacijentCommand.Adress,
                    Email = pacijentCommand.Email
                };
                Card card = new Card
                {
                    Patient = new Patient
                    {
                        FirstName = pacijentCommand.FirstName,
                        LastName = pacijentCommand.LastName,
                        LBO = pacijentCommand.LBO,
                        Phone = pacijentCommand.Phone,
                        Adress = pacijentCommand.Adress,
                        Email = pacijentCommand.Email
                    },
                    Allergens = new List<Allergen>(),
                    Prescriptions = new List<Prescription>(),
                    Refferals = new List<Refferal>(),
                    Therapies = new List<Therapy>(5)
                };

                _context.Cards.Add(card);

                _context.SaveChanges();
                return Ok(new { message = "Succesfully created patient" });
            }
            else
                return BadRequest();
        }

        // DELETE: pacijent {id}
        [HttpDelete("{id}")]
        public IActionResult DeletePatientById([FromRoute] int id)
        {
            Patient patient = _context.Patients.Where(x => x.Id == id).FirstOrDefault();
            if (patient != null) {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
                return Ok(new { message = "Succesfully deleted patient" });
            } else
                return BadRequest();
        }
        // PUT: pacijenti
        [HttpPut("{id}")]
        public IActionResult UpdatePacijent([FromRoute] int id, [FromBody] UpdatePatientCommand pacijentCommand)
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
            if (_context.Patients.Any(o => o.Id == id))
            {
                return true;
            }
            else
                return false;
        }

    }
}
