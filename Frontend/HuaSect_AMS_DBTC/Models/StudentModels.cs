using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuaSect_AMS_DBTC.Models
{
    [Table("Students")]
    public class StudentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(60)]
        public string? MiddleName { get; set; }

        [Required, MaxLength(60)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string StudentNo { get; set; } = string.Empty;

        [Required]
        public int RollNumber { get; set; }

        [Required, MaxLength(10)]
        public string YearLevel { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string CourseId { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Course { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(120)]
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

        [NotMapped]
        public string FullName =>
            string.IsNullOrWhiteSpace(MiddleName)
                ? $"{FirstName} {LastName}"
                : $"{FirstName} {MiddleName[0]}. {LastName}";

        [NotMapped]
        public string CourseSection => $"{CourseId} - {YearLevel}";
    }

    public class StudentViewModel
    {
        public List<StudentModel> Students { get; set; } = new();
        public string? SearchQuery { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class StudentListViewModel
    {
        public List<StudentModel> Students { get; set; } = new();
        public List<string> Courses { get; set; } = new();
        public string? SelectedCourse { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }

    public class StudentInputModel
    {
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(60)]
        public string? MiddleName { get; set; }

        [Required, MaxLength(60)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string StudentNo { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string YearLevel { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string CourseId { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(120)]
        public string Email { get; set; } = string.Empty;
    }

    public class AttendanceInputModel
    {
        [Required]
        public int StudentId { get; set; }

        [Required, MaxLength(10)]
        public string Status { get; set; } = "present";

        public DateTime Date { get; set; } = DateTime.Today;

        [MaxLength(200)]
        public string? Subject { get; set; }
    }

    public class AttendanceDto
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}