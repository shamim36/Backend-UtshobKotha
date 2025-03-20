using Backend_UtshobKotha.Data;
using Backend_UtshobKotha.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Backend_UtshobKotha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(UtshobKothaDbContext context) : ControllerBase
    {
        private readonly UtshobKothaDbContext _context = context;

        [HttpPost]
        public async Task<ActionResult> PostLogin(Login loginRequest)
        {
            // Validate the login request
            if (loginRequest is null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
                return BadRequest("Email and password are required.");

            // Fetch the user from the database using the provided email
            var user = await _context.NewUserRegistration
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            // Check if the user exists
            if (user is null)
                return NotFound("User not found.");

            // Validate the password (assuming the password is hashed in the database)
            if (user.Password != loginRequest.Password) 
                return Unauthorized("Invalid password.");


            // If email and password are valid, return a success response
            return Ok(new { Message = "Login successful!", User = user });
        }
    }
}