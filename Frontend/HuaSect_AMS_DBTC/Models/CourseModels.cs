namespace HuaSect_AMS_DBTC.Models
{
    public class Course
    {
        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Schedule { get; set; } = string.Empty;
    }

    public class CourseListPage
    {
        public ICollection<Course> Courses { get; set; } = [];

        public ICollection<Student> Students { get; set; } = [];

        public ICollection<Attendance> AttendanceRecords { get; set; } = [];
    }

     public class CreateCourseModel
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Units { get; set; }
    }
}