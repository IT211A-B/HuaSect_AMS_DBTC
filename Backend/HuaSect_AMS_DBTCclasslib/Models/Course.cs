namespace HuaSect_AMS_DBTCclasslib;

public class Course
{
    public int ID { get; set; }

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
        Units = units;
    }
}
