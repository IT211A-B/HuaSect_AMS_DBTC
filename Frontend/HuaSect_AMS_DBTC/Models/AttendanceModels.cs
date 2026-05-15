namespace HuaSect_AMS_DBTC.Models
{
    public class Attendance
    {
        public string Day { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Status { get; set; } = string.Empty;
    }

    public class AttendanceTrackerPage
    {
        public required Course Course { get; set; }

        public required Student Student { get; set; }

        public ICollection<Attendance> AttendanceRecords { get; set; } = [];
    }


    public class AttendanceProfilePage
    {
        public required Course Course { get; set; }

        public ICollection<Attendance> AttendanceRecords { get; set;} = [];
    }
}