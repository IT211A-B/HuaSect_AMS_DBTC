namespace HuaSect_AMS_DBTCclasslib;

public class CreateAttendanceRecordDto
{
    public DateTime Date { get; set; } = DateTime.Now;

    public bool Status { get; set; }
}

public class NewlyCreateAttendanceRecordDto
{
    public int ID { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;

    public bool Status { get; set; }
}

public class UpdateAttendanceRecordDto
{
    public int ID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public bool Status { get; set; }
}
