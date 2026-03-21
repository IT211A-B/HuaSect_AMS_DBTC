namespace HuaSect_AMS_DBTCclasslib;

public class CreateCourseDto
{
    public string Name { get; set; } = "";

    public int Units { get; set; }

    public string Schedule { get; set; } = "";
}

public class NewlyCreateCourseDto
{
    public int ID { get; set; }
    
    public string Name { get; set; } = "";

    public int Units { get; set; }

    public string Schedule { get; set; } = "";
}

public class UpdateCourseDto
{
    public int ID { get; set; }

    public string Name { get; set; } = "";

    public int Units { get; set; }

    public string Schedule { get; set; } = "";
}
