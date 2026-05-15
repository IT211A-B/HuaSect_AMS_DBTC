using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTCclasslib.Models;

public class StudentProfile
{
    public int ID { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public string StudentNumber { get; set; } = "";

    public int YearLevel { get; set; }

    public ICollection<Course> Courses { get; set; } = [];

    public StudentProfile(string studentNumber, int yearLevel)
    {
        StudentNumber = studentNumber;
        YearLevel = yearLevel;
    }

    public StudentProfile(string studentNumber, int yearLevel, ApplicationUser user)
    {
        StudentNumber = studentNumber;
        YearLevel = yearLevel;
        User = user;
    }

    public void Update(int id, string studentNumber, int yearLevel)
    {
        ID = id;
        StudentNumber = studentNumber;
        YearLevel = yearLevel;
    }
}
