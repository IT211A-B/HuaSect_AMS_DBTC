using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTCclasslib.Models;

public class StudentProfile
{
    public int ID { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public string StudentNumber { get; set; } = "";

    public int YearLevel { get; set; }

    public Course Course { get; set; } = new Course("", "", "", 0);

    public StudentProfile(string studentNumber, int yearLevel)
    {
        StudentNumber = studentNumber;
        YearLevel = yearLevel;
    }

    public void Update(int id, string studentNumber, int yearLevel)
    {
        ID = id;
        StudentNumber = studentNumber;
        YearLevel = yearLevel;
    }
}
