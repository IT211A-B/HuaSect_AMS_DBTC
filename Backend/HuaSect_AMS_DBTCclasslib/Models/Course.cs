namespace HuaSect_AMS_DBTCclasslib;

public class Course
{
    public int ID { get; set; }

    public string Name { get; set; } = "";

    public int Units { get; set; }

    public Teacher Teacher { get; set; } = new Teacher { };

    public string Schedule { get; set; } = "";
}
