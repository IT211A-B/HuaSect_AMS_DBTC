using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YourApp.Models
{
    // ─────────────────────────────────────────
    // Domain Models
    // ─────────────────────────────────────────

    /// <summary>Represents a teacher/faculty member.</summary>
    public class Teacher
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        /// <summary>Initials used in the welcome avatar.</summary>
        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    /// <summary>Represents an academic course assigned to a teacher.</summary>
    public class Course
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        /// <summary>e.g. "IT 210A"</summary>
        [Required, MaxLength(20)]
        public string Code { get; set; } = string.Empty;

        /// <summary>e.g. "Mon &amp; Sat"</summary>
        [MaxLength(100)]
        public string Days { get; set; } = string.Empty;

        /// <summary>e.g. "8:00 AM – 9:00 AM"</summary>
        [MaxLength(50)]
        public string TimeSlot { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        /// <summary>Total students enrolled.</summary>
        public int StudentCount { get; set; }
    }

    /// <summary>Attendance record for a single class session.</summary>
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public DateTime SessionDate { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Late { get; set; }
    }

    // ─────────────────────────────────────────
    // View Models
    // ─────────────────────────────────────────

    /// <summary>ViewModel for the teacher dashboard / course list page.</summary>
    public class TeacherDashboardViewModel
    {
        public Teacher Teacher { get; set; } = new();
        public List<CourseCardViewModel> Courses { get; set; } = new();

        // Quick stats displayed at the top of the dashboard
        public int TotalCourses => Courses.Count;
        public int TotalStudents => Courses.Sum(c => c.StudentCount);
        public int CoursesToday { get; set; }
    }

    /// <summary>Flattened model consumed by each course card in the view.</summary>
    public class CourseCardViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string FacultyName { get; set; } = string.Empty;
        public string Days { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
        public int StudentCount { get; set; }

        /// <summary>
        /// Convenience URL for the "View Attendance" button,
        /// e.g. /Teacher/Attendance/5
        /// </summary>
        public string AttendanceUrl => $"/Teacher/Attendance/{CourseId}";
    }

    /// <summary>ViewModel for the attendance history page of a single course.</summary>
    public class AttendanceHistoryViewModel
    {
        public CourseCardViewModel Course { get; set; } = new();
        public List<AttendanceRecord> Records { get; set; } = new();
        public double AttendanceRate =>
            Records.Count == 0 ? 0 :
            Records.Sum(r => r.Present) * 100.0 /
            Math.Max(1, Records.Sum(r => r.Present + r.Absent + r.Late));
    }
}
