using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTC.Models
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Username or email is required.")]
        [Display(Name = "Username / Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "student"; // Matches your hidden input's default value
    }
}

