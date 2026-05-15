using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuaSect_AMS_DBTC.Models
{   
    public class StudentDashboardPage
    {
        public ICollection<Course> Courses { get; set; } = [];
    }

    public class Student
    {
        public string Name { get; set; } = string.Empty;

        public int Year { get; set; }

        public string Field { get; set; } = string.Empty;
    }

    public class StudentManagementPage
    {
        public required Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; } = [];
    }

    public class RegisterStudentModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Optional field
        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        // Optional field
        [Display(Name = "Suffix")]
        public string? Suffix { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Student number is required.")]
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; }

        [Required(ErrorMessage = "Year level is required.")]
        [Range(1, 5, ErrorMessage = "Year level must be between 1 and 5.")]
        [Display(Name = "Year Level")]
        public int YearLevel { get; set; }
    }
}