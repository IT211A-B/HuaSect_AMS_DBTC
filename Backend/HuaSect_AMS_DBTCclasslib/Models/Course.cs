namespace HuaSect_AMS_DBTCclasslib;

public class Course
{
    public int ID { get; set; }

    public string Code { get; set; } = "";

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int Units { get; set; }

    public TeacherProfile TeacherProfile { get; set; } = new TeacherProfile("");

    public Course(string code, string name, string description, int units)
    {
        Code = code;
        Name = name;
        Description = description;
        Units = units;
    }

    public void Update(int id, string code, string name, string description, int units)
    {
        ID = id;
        Code = code;
        Name = name;
        Description = description;
        Units = units;
    }
}
