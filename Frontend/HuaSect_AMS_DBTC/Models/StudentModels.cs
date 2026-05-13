using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuaSect_AMS_DBTC.Models
{   
    public class StudentDashboardPage
    {
        public ICollection<Course> Courses { get; set; } = [];
    }

    public class AttendanceProfilePage
    {
        public required Course Course { get; set; }

        public ICollection<Attendance> AttendanceRecords { get; set;} = [];
    }
}