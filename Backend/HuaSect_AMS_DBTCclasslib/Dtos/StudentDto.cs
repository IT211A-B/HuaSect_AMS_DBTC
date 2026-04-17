namespace HuaSect_AMS_DBTCclasslib.Dtos;

public class CreateStudentDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public int YearLevel { get; set; }
}

public class NewlyCreateStudentDto
{
    public int ID { get; set; }
    
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public string FullName { get; set; } = "";

    public int YearLevel { get; set; }
}

public class UpdateStudentDto
{
    public int ID { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public int YearLevel { get; set; }
}