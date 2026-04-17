using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    // ══════════════════════════════════════════════════════════════
    //  Enumerations
    // ══════════════════════════════════════════════════════════════

    /// <summary>Attendance status for a student on a given day.</summary>
    public enum AttendanceStatus
    {
        Unknown = 0,
        Present = 1,
        Late = 2,
        Absent = 3,
    }

    // ══════════════════════════════════════════════════════════════
    //  Domain Models  (map to database tables)
    // ══════════════════════════════════════════════════════════════

    /// <summary>Represents an academic course offered by the institution.</summary>
    [Table("Courses")]
    public class CourseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required, MaxLength(20)]
        [Display(Name = "Course Code")]
        public string Code { get; set; }                  // e.g. "BSIT", "ME"

        [Required, MaxLength(120)]
        [Display(Name = "Course Name")]
        public string Name { get; set; }                  // e.g. "Information Management"

        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public virtual ICollection<AttendanceRecord> Attendances { get; set; } = new List<AttendanceRecord>();
    }

    /// <summary>Represents a student enrolled in the system.</summary>
    [Table("Students")]
    public class StudentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required, MaxLength(60)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(60)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required, MaxLength(60)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName =>
            string.IsNullOrWhiteSpace(MiddleName)
                ? $"{LastName}, {FirstName}"
                : $"{LastName}, {FirstName} {MiddleName[0]}.";

        [Required]
        [Display(Name = "Roll Number")]
        public int RollNumber { get; set; }

        [Required, MaxLength(20)]
        [Display(Name = "Course")]
        public string Course { get; set; }           // e.g. "BSIT"

        [ForeignKey(nameof(CourseModel))]
        public string CourseId { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // Navigation
        public virtual CourseModel CourseNav { get; set; }
        public virtual ICollection<AttendanceRecord> Attendances { get; set; } = new List<AttendanceRecord>();
    }

    /// <summary>Represents a single attendance record (student + date + status).</summary>
    [Table("AttendanceRecords")]
    public class AttendanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public string CourseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Unknown;

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(300)]
        public string Remarks { get; set; }

        // Navigation
        [ForeignKey(nameof(StudentId))]
        public virtual StudentModel Student { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual CourseModel Course { get; set; }
    }

    // ══════════════════════════════════════════════════════════════
    //  View Models  (shape data for the Razor views)
    // ══════════════════════════════════════════════════════════════

    /// <summary>View model for the Course List / Attendance main page.</summary>
    public class CourseListViewModel
    {
        public IEnumerable<CourseModel> AvailableCourses { get; set; } = new List<CourseModel>();
        public IEnumerable<StudentModel> Students { get; set; } = new List<StudentModel>();

        public string SelectedCourseId { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Today;

        /// <summary>Pre-loaded attendance map: StudentId → Status string.</summary>
        public Dictionary<string, AttendanceStatus> AttendanceRecords { get; set; } = new();

        // Derived helpers used in the view
        public string FormattedDate =>
            SelectedDate.ToString("MMMM d, yyyy");    // "February 15, 2026"

        public string SelectedCourseName(string courseId) =>
            AvailableCourses is not null
                ? System.Linq.Enumerable.FirstOrDefault(AvailableCourses, c => c.Id == courseId)?.Name ?? string.Empty
                : string.Empty;
    }

    // ══════════════════════════════════════════════════════════════
    //  Request / Response DTOs  (used by AJAX endpoints)
    // ══════════════════════════════════════════════════════════════

    /// <summary>JSON payload sent by the client when saving attendance.</summary>
    public class AttendanceSaveRequest
    {
        public string CourseId { get; set; }
        public DateTime Date { get; set; }
        /// <summary>Key: StudentId, Value: "present" | "late" | "absent"</summary>
        public Dictionary<string, string> Attendance { get; set; } = new();
    }

    /// <summary>Lightweight course summary returned by the FilterStudents endpoint.</summary>
    public class StudentSummaryDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int RollNumber { get; set; }
        public string Course { get; set; }
    }
}
