namespace HuaSect_AMS_DBTCclasslib.Dtos;

public class CreateCourseDto
{
    public string Code { get; set; } = "";

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int Units { get; set; }
}

public class NewlyCreateCourseDto
{
    public int ID { get; set; }
    
    public string Code { get; set; } = "";

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int Units { get; set; }
}

public class UpdateCourseDto
{
    public int ID { get; set; }

    public string Code { get; set; } = "";

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int Units { get; set; }
}
