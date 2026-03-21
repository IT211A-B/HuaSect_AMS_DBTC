using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTCclasslib.Models;

public class Student
{
    public int ID { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string MiddleName { get; set; } = "";

    public string Suffix { get; set; } = "";

    public string FullName
    {
        get
        {
            return $"{FirstName}{$" {MiddleName[0]}. "}{LastName}{$" {Suffix}"}";
        }
    }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";

    public int YearLevel { get; set; }

    public Course Course { get; set; } = new Course("", 0, "");

    public Student(string email, string firstName, string lastName, string middleName, string suffix, int yearLevel)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Suffix = suffix;
        YearLevel = yearLevel;
    }

    public void Update(int id, string email, string firstName, string lastName, string middleName, string suffix, int yearLevel)
    {
        ID = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Suffix = suffix;
        YearLevel = yearLevel;
    }
}
