using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTC.Models
{

    public class LogInModel
    {

        [Required(ErrorMessage = "Email or username is required.")]
        [Display(Name = "Email / Username")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "Username must be between 3 and 150 characters.")]
        public string Username { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(128, MinimumLength = 6,
            ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;


        [Required]
        [RegularExpression("student|teacher",
            ErrorMessage = "Role must be 'student' or 'teacher'.")]
        public string Role { get; set; } = "student";


        public bool RememberMe { get; set; } = false;
    }


    public class AuthResult
    {
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }
        public AppUser? User { get; set; }
    }

    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "student"; // "student" | "teacher"
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }


    public class GoogleUserProfile
    {
        public string GoogleId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PictureUrl { get; set; }
    }
}

