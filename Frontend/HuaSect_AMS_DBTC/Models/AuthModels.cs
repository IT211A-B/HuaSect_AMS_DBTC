using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTC.Models
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string  Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "student"; // Matches your hidden input's default value
    }

    public class LogInResponseModel
    {
        public string Token { get; set; } = string.Empty;
    }

    public class RegisterStudentModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Suffix { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; } = "student"; // Matches your hidden input's default value
    }
}

