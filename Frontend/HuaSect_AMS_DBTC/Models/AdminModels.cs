using System.ComponentModel.DataAnnotations;

namespace HuaSect_AMS_DBTC.Models
{
    public class UserManagementPage
    {
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}

