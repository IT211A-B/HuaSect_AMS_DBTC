namespace HuaSect_AMS_DBTCclasslib;

public class CreateTeacherDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

<<<<<<< HEAD
    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
=======
    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6

    public string Department { get; set; } = "";
}

public class NewlyCreateTeacherDto
{
<<<<<<< HEAD
    public string Id { get; set; } = "";
=======
    public int ID { get; set; }
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

<<<<<<< HEAD
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
=======
    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string FullName { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string Department { get; set; } = "";
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
}

public class UpdateTeacherDto
{
<<<<<<< HEAD
    public int Id { get; set; }
=======
    public int ID { get; set; }
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

<<<<<<< HEAD
    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public string Department { get; set; } = "";

    public UpdateTeacherDto(int id)
    {
        Id = id;
    }
=======
    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string Department { get; set; } = "";
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
}
