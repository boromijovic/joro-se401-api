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
    [Route("cards")]
    [EnableCors("AllowSpecificOrigin")]
    public class CardsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CardsController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: cards
        [HttpGet]
        public IActionResult GetAll()
        {
            var cards = _context.Cards.Include(x => x.Patient)
                .Include(x => x.Therapies)
                .Include(x => x.Prescriptions)
                .Include(x => x.Refferals)
                    .ToList();
            return Ok(new { cards = cards });
        }

        // GET: card
        [HttpGet("{cardId}")]
        public IActionResult GetCardById([FromRoute] int cardId)
        {
            if (CheckIfExists(cardId))
            {
                Card card = _context.Cards.Where(x => x.CardId == cardId).FirstOrDefault();

                return Ok(new { card });
            }
            else
                return BadRequest(new { message = "Could not find card with provided id" });
        }
        // GET: card
        [HttpGet("patient/{patientId}")]
        public IActionResult GetCardByPatientId([FromRoute] int patientId)
        {
            if (CheckIfExists(patientId))
            {
                Card card = _context.Cards.Where(x => x.Patient.Id == patientId)
                    .Include(x => x.Patient)
                    .Include(x => x.Therapies)
                    .FirstOrDefault();

                return Ok(new { card });
            }
            else
                return BadRequest(new { message = "Could not find card with provided patient id" });
        }

        // DELETE: card {cardId}
        [HttpDelete("{cardId}")]
        public IActionResult DeleteCardById([FromRoute] int cardId)
        {
            Card card = _context.Cards.Where(x => x.CardId == cardId).Include(x => x.Patient).FirstOrDefault();
            if (card != null) {
                _context.Cards.Remove(card);
                _context.SaveChanges();
                return Ok(new { message = "Succesfully deleted card" });
            } else
                return BadRequest();
        }

        // POST: therapy
        [HttpPost("patient/{patientId}/add-therapy")]
        public IActionResult AddTherapyToPatient([FromRoute] int patientId, [FromBody] TherapyCommand therapyCommand)
        {
            if (therapyCommand != null)
            {

                Card card = _context.Cards.Where(x => x.Patient.Id == patientId).FirstOrDefault();

                if (card != null)
                {
                    Therapy t = new Therapy
                    {
                        Description = therapyCommand.Description,
                        Symptoms = therapyCommand.Symptoms,
                        Comments = therapyCommand.Comments,
                        DataOfExam = therapyCommand.DataOfExam
                    };

                    card.Therapies = new List<Therapy>
                    {
                        t
                    };

                    _context.SaveChanges();
                    return Ok(new { message = "Succesfully added therapy to patient" });
                }else
                    return BadRequest(new { message = "CARD NULL" });
            }
            else
                return BadRequest(new { message = "COMMAND EMPTY" });
        }

        // POST: therapy
        [HttpPost("patient/{patientId}/add-prescription")]
        public IActionResult AddPrescriptionToPatient([FromRoute] int patientId, [FromBody] PrescriptionCommand prescriptionCommand)
        {
            if (prescriptionCommand != null)
            {

                Card card = _context.Cards.Where(x => x.Patient.Id == patientId).FirstOrDefault();

                if (card != null)
                {
                    Prescription p = new Prescription
                    {
                        DescriptionOfUse = prescriptionCommand.DescriptionOfUse,
                        DrugName = prescriptionCommand.DrugName,
                        DoctorName = prescriptionCommand.DrugName,
                        Note = prescriptionCommand.Note,
                        DataOfExpiry = prescriptionCommand.DataOfExpiry
                    };

                    card.Prescriptions = new List<Prescription>
                    {
                        p
                    };

                    _context.SaveChanges();
                    return Ok(new { message = "Succesfully added prescription to patient" });
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        // POST: therapy
        [HttpPost("patient/{patientId}/add-refferal")]
        public IActionResult AddRefferalToPatient([FromRoute] int patientId, [FromBody] RefferalCommand refferalCommand)
        {
            if (refferalCommand != null)
            {

                Card card = _context.Cards.Where(x => x.Patient.Id == patientId).FirstOrDefault();

                if (card != null)
                {
                    Refferal r = new Refferal
                    {
                        PatientId = patientId,
                        DescriptionOfProblem = refferalCommand.DescriptionOfProblem,
                        DoctorName = refferalCommand.DoctorName,
                        Note = refferalCommand.Note,
                        RefferingMD = refferalCommand.RefferingMD,
                        Date = refferalCommand.Date,
                    };

                    card.Refferals = new List<Refferal>
                    {
                        r
                    };

                    _context.SaveChanges();
                    return Ok(new { message = "Succesfully added refferal to patient" });
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        private bool CheckIfExists(int id)
        {
            if (_context.Cards.Any(o => o.CardId == id))
            {
                return true;
            }
            else
                return false;
        }

    }
}
