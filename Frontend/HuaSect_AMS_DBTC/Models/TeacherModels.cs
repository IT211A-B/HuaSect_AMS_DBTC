namespace HuaSect_AMS_DBTC.Models
{
    public class Student
    {
        public string Name { get; set; } = string.Empty;

        public int Year { get; set; }

        public string Field { get; set; } = string.Empty;
    }

    public class StudentManagementPage
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = [];
    }

    public class Course
    {
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Schedule { get; set; } = string.Empty;
    }

    public class Attendance
    {
        public string Day { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Status { get; set; } = string.Empty;
    }

    public class AttendanceTrackerPage
    {
        public required Course Course { get; set; }

        public required Student Student { get; set; }

        public ICollection<Attendance> AttendanceRecords { get; set; } = [];
    }

    public class CourseListPage
    {
        public ICollection<Course> Courses { get; set; } = [];

        public ICollection<Student> Students { get; set; } = [];

        public ICollection<Attendance> AttendanceRecords { get; set; } = [];
    }
}