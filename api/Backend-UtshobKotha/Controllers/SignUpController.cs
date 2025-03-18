using Backend_UtshobKotha.Data;
using Backend_UtshobKotha.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Backend_UtshobKotha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController(UtshobKothaDbContext context) : ControllerBase
    {
        private readonly UtshobKothaDbContext _context = context;
       
        [HttpPost]
        public async Task<ActionResult<SignUp>> PostSignUp(SignUp signUpNewUser)
        {
            // Checking if the provided SignUp object is null
            if (signUpNewUser is null)
                return BadRequest("User data cannot be null.");

            // Validate the email (making sure it's not null or empty)
            if (string.IsNullOrEmpty(signUpNewUser.Email))
                return BadRequest("Email is required.");



            _context.NewUserRegistration.Add(signUpNewUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PostSignUp), new { id = signUpNewUser.UserID }, signUpNewUser);

        }
    }
}
