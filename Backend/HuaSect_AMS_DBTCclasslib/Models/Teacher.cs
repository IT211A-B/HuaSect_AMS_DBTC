using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTCclasslib;

public class Teacher
{
    public int ID { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";

    public string Department { get; set; } = "";

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = "";

    public Teacher(string firstName, string lastName, string email, string department, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Department = department;
        PhoneNumber = phoneNumber;
    }

    public void Update(int id, string firstName, string lastName, string email, string department, string phoneNumber)
    {
        ID = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Department = department;
        PhoneNumber = phoneNumber;
    }
}
