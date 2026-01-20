using System.ComponentModel.DataAnnotations;

namespace Simulation7.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please enter email."), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}
