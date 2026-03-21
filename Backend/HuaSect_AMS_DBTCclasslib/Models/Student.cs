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

    public Course Course { get; set; } = new Course {};
}
