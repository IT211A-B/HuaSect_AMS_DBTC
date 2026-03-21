namespace HuaSect_AMS_DBTCclasslib;

public class CreateTeacherDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string Department { get; set; } = "";
}

public class NewlyCreateTeacherDto
{
    public int ID { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string FullName { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string Department { get; set; } = "";
}

public class UpdateTeacherDto
{
    public int ID { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string Department { get; set; } = "";
}
