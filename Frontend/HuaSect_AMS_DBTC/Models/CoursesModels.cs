using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuaSect_AMS_DBTC.Models
{
    [Table("Courses")]
    public class CourseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required, MaxLength(20)]
        public string Code { get; set; } = string.Empty;

        [Required, MaxLength(120)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
        public virtual ICollection<AttendanceRecord> Attendances { get; set; } = new List<AttendanceRecord>();
    }

    public class CourseListViewModel
    {
        public IEnumerable<CourseModel> AvailableCourses { get; set; } = new List<CourseModel>();
        public IEnumerable<StudentModel> Students { get; set; } = new List<StudentModel>();
        public string? SelectedCourseId { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Today;
        public Dictionary<string, AttendanceStatus> AttendanceRecords { get; set; } = new();
        public string FormattedDate => SelectedDate.ToString("MMMM d, yyyy");
        public string SelectedCourseName(string courseId) =>
            AvailableCourses is not null
                ? System.Linq.Enumerable.FirstOrDefault(AvailableCourses, c => c.Id == courseId)?.Name ?? string.Empty
                : string.Empty;
    }

    public class AttendanceSaveRequest
    {
        public string CourseId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Dictionary<string, string> Attendance { get; set; } = new();
    }

    public class StudentSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int RollNumber { get; set; }
        public string Course { get; set; } = string.Empty;
    }
}