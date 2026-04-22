namespace HuaSect_AMS_DBTCclasslib.Dtos;

public class CreateStudentDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

<<<<<<< HEAD
    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public string StudentNumber { get; set; } = "";

=======
    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
    public int YearLevel { get; set; }
}

public class NewlyCreateStudentDto
{
<<<<<<< HEAD
    public int Id { get; set; }

    public string StudentNumber { get; set; } = "";

=======
    public int ID { get; set; }
    
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

<<<<<<< HEAD
    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public int YearLevel { get; set; }

    public NewlyCreateStudentDto(int id, int yearLevel)
    {
        Id = id;
        YearLevel = yearLevel;
    }
=======
    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";

    public string FullName { get; set; } = "";

    public int YearLevel { get; set; }
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6
}

public class UpdateStudentDto
{
<<<<<<< HEAD
    public int Id { get; set; }

    public string StudentNumber { get; set; } = "";
=======
    public int ID { get; set; }
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

<<<<<<< HEAD
    public string? MiddleName { get; set; } = "";

    public string? Suffix { get; set; }
=======
    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string Email { get; set; } = "";
>>>>>>> 624762897acc0c0f9d7ec50ea297351c211aeea6

    public int YearLevel { get; set; }
}