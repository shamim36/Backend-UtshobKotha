using System.ComponentModel.DataAnnotations; // Required for [Key]

namespace Backend_UtshobKotha.Models.Accounts
{
    public class SignUp
    {

        [Key]
        public required string Email { get; set; }
        public int UserID { get; set; }
        public required string Name { get; set; }
        
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
