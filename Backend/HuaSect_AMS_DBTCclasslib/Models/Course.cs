namespace HuaSect_AMS_DBTCclasslib;

public class Course
{
    public int ID { get; set; }

<<<<<<< HEAD
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
=======
    public string Name { get; set; } = "";

    public int Units { get; set; }

    public Teacher Teacher { get; set; } = new Teacher("", "", "", "", "", "", "");

    public string Schedule { get; set; } = "";

    public Course(string name, int units, string schedule)
    {
        Name = name;
        Units = units;
        Schedule = schedule;
    }

    public void Update(int id, string name, string schedule, int units)
    {
        ID = id;
        Name = name;
        Schedule = schedule;
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
        Units = units;
    }
}
