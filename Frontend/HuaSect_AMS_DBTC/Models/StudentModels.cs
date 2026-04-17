using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
    // ── Domain Model ─────────────────────────────────────────────────

    /// <summary>
    /// Represents a student entity stored in the database.
    /// </summary>
    public class StudentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(60)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string StudentId { get; set; } = string.Empty;   // e.g. "2024-00123"

        [Required]
        [MaxLength(20)]
        public string StudentNo { get; set; } = string.Empty;   // roll number

        [Required]
        [MaxLength(10)]
        public string YearLevel { get; set; } = string.Empty;   // e.g. "3"

        [Required]
        [MaxLength(15)]
        public string CourseId { get; set; } = string.Empty;    // e.g. "BSIT"

        [Required]
        [EmailAddress]
        [MaxLength(120)]
        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

        // ── Computed Properties ────────────────────────────────────
        [NotMapped]
        public string FullName =>
            string.IsNullOrWhiteSpace(MiddleName)
                ? $"{FirstName} {LastName}"
                : $"{FirstName} {MiddleName[0]}. {LastName}";

        [NotMapped]
        public string CourseSection => $"{CourseId} - {YearLevel}";
    }

    // ── Attendance Domain Model ───────────────────────────────────────

    /// <summary>
    /// A single attendance record for one student on one date.
    /// </summary>
    public class AttendanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual StudentModel? Student { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = AttendanceStatus.Present;  // "present" | "late" | "absent"

        [MaxLength(200)]
        public string? Subject { get; set; }

        [MaxLength(300)]
        public string? Remarks { get; set; }
    }

    /// <summary>
    /// Attendance status constants.
    /// </summary>
    public static class AttendanceStatus
    {
        public const string Present = "present";
        public const string Late = "late";
        public const string Absent = "absent";
    }

    // ── View Models ───────────────────────────────────────────────────

    /// <summary>
    /// ViewModel for the main Student Management dashboard.
    /// </summary>
    public class StudentViewModel
    {
        public List<StudentModel> Students { get; set; } = new();
        public string? SearchQuery { get; set; }
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// ViewModel for the Student List / bulk attendance view.
    /// </summary>
    public class StudentListViewModel
    {
        public List<StudentModel> Students { get; set; } = new();
        public List<string> Courses { get; set; } = new();
        public string? SelectedCourse { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }

    // ── Input Models (form binding) ───────────────────────────────────

    /// <summary>
    /// Bound from the Add Student / Update Student forms.
    /// </summary>
    public class StudentInputModel
    {
        public int Id { get; set; }   // 0 for new, non-zero for update

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(60)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(60)]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(60)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student ID is required.")]
        [MaxLength(20)]
        public string StudentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student number is required.")]
        [MaxLength(20)]
        public string StudentNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year level is required.")]
        [MaxLength(10)]
        public string YearLevel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course ID is required.")]
        [MaxLength(15)]
        public string CourseId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(120)]
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// Bound from the bulk attendance save form.
    /// </summary>
    public class AttendanceInputModel
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = AttendanceStatus.Present;

        public DateTime Date { get; set; } = DateTime.Today;

        [MaxLength(200)]
        public string? Subject { get; set; }
    }

    // ── DTO (for JSON responses) ──────────────────────────────────────

    /// <summary>
    /// Lightweight attendance record returned as JSON.
    /// </summary>
    public class AttendanceDto
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
