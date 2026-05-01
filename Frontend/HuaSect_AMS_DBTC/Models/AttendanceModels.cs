using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuaSect_AMS_DBTC.Models
{
    public enum AttendanceStatus
    {
        Unknown = 0, Present = 1, Late = 2, Absent = 3,
    }

    [Table("AttendanceRecords")]
    public class AttendanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string StudentId { get; set; } = string.Empty;

        [Required]
        public string CourseId { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Unknown;

        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        public long Present { get; set; }

        public long Absent { get; set; }

        public long Late { get; set; }

        [MaxLength(300)]
        public string? Remarks { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual StudentModel? Student { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual CourseModel? Course { get; set; }
    }
}