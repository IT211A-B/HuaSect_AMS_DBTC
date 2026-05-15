namespace HuaSect_AMS_DBTC.Models
{
    public class Teacher
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
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
}