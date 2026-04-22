using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTCclasslib;

public class TeacherProfile
{
    public int ID { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public string Department { get; set; } = "";

    public TeacherProfile(string department)
    {
        Department = department;
    }

    public void Update(int id, string department)
    {
        ID = id;
        Department = department;
    }
}
