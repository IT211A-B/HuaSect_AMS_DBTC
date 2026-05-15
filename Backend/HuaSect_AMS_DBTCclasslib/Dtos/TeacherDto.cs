namespace HuaSect_AMS_DBTCclasslib;

public class CreateTeacherDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public string Department { get; set; } = "";
}

public class NewlyCreateTeacherDto
{
    public string Id { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public string Department { get; set; } = "";

    public NewlyCreateTeacherDto(string id, string firstName, string lastName, string? middleName, string? suffix, string department)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Suffix = suffix;
        Department = department;
    }
}

public class UpdateTeacherDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public string Department { get; set; } = "";

    public UpdateTeacherDto(int id)
    {
        Id = id;
    }
}
