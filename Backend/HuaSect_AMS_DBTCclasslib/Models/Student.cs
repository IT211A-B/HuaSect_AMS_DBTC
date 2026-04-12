using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTCclasslib.Models;

public class Student
{
    public int ID { get; set; }

    public string Number { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public int YearLevel { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";

    public Course Course { get; set; } = new Course("", "", "", 0);

    public Student(string number, string firstName, string lastName, string middleName, int yearLevel, string email)
    {
        Number = number;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        YearLevel = yearLevel;
        Email = email;
    }

    public void Update(int id, string number, string firstName, string lastName, string middleName, int yearLevel, string email)
    {
        ID = id;
        Number = number;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        YearLevel = yearLevel;
        Email = email;
    }
}
