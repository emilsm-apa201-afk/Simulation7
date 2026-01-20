using System.ComponentModel.DataAnnotations;

namespace Simulation7.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please enter fullname.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter email."), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}
