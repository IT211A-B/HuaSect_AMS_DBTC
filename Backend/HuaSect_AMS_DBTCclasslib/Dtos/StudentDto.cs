namespace HuaSect_AMS_DBTCclasslib.Dtos;

public class CreateStudentDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public string StudentNumber { get; set; } = "";

    public int YearLevel { get; set; }
}

public class NewlyCreateStudentDto
{
    public int Id { get; set; }

    public string StudentNumber { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public int YearLevel { get; set; }

    public NewlyCreateStudentDto(int id, int yearLevel)
    {
        Id = id;
        YearLevel = yearLevel;
    }
}

public class UpdateStudentDto
{
    public int Id { get; set; }

    public string StudentNumber { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; } = "";

    public string? Suffix { get; set; }

    public int YearLevel { get; set; }
}
