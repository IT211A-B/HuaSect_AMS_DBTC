using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTCclasslib;

public class Teacher
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

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = "";

    public string Department { get; set; } = "";

    public Teacher(string firstName, string lastName, string middleName, string suffix, string email, string phoneNumber, string department)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Suffix = suffix;
        Email = email;
        PhoneNumber = phoneNumber;
        Department = department;
    }

    public void Update(int id, string department, string email, string firstName, string lastName, string middleName, string suffix, string phoneNumber)
    {
        ID = id;
        Department = department;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Suffix = suffix;
        PhoneNumber = phoneNumber;
    }
}
