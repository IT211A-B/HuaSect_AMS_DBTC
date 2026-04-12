using System.ComponentModel.DataAnnotations;
using HuaSect_AMS_DBTCclasslib.Models;

namespace HuaSect_AMS_DBTCclasslib;

public class Attendance
{
    public int ID { get; set; }

    public Student Student { get; set; } = new Student("", "", "", "", 0, "");

    public Course Course { get; set; } = new Course("", "", "", 0);

    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Now;

    public bool Status { get; set; }

    public Attendance(DateTime date, bool status)
    {
        Date = date;
        Status = status;
    }

    public void Update(int id, DateTime date, bool status)
    {
        ID = id;
        Date = date;
        Status = status;
    }
}
