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
    [Route("authentication")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }
        // POST: login
        [HttpPost("login")]
        public IActionResult Authentication([FromBody] LoginCommand loginCommand)
        {
            if (loginCommand != null)
            {
                User user = _context.Users.Where(c => 
                   c.Username == loginCommand.Username).FirstOrDefault();

                if (user != null && user.Password.Equals(loginCommand.Password))
                {
                    return Ok(new {
                            user = user.Username,
                            userType = user.UserType,
                            token = user.Token
                        }
                    );
                }else
                    return BadRequest(new { message = "CARD NULL" });
            }
            else
                return BadRequest(new { message = "COMMAND EMPTY" });
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
