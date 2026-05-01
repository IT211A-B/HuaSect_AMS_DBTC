using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTC.Models
{
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
        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

    public class Course
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Code { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Days { get; set; } = string.Empty;

        [MaxLength(50)]
        public string TimeSlot { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int StudentCount { get; set; }
    }

    public class TeacherDashboardViewModel
    {
        public Teacher Teacher { get; set; } = new();
        public List<CourseCardViewModel> Courses { get; set; } = new();
        public int TotalCourses => Courses.Count;
        public int TotalStudents => Courses.Sum(c => c.StudentCount);
        public int CoursesToday { get; set; }
    }

    public class CourseCardViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string FacultyName { get; set; } = string.Empty;
        public string Days { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public string AttendanceUrl => $"/Teacher/Attendance/{CourseId}";
    }

    public class AttendanceHistoryViewModel
    {
        public CourseCardViewModel Course { get; set; } = new();
        public List<AttendanceRecord> Records { get; set; } = new();
        public double AttendanceRate =>
            Records.Count == 0 ? 0 :
            Records.Sum(r => r.Present) * 100.0 /
            Math.Max(1, Records.Sum(r => r.Present + r.Absent + r.Late));
    }

    public class AttendanceSessionRecord
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public DateTime SessionDate { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Late { get; set; }
    }
}