using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public interface ITeacherService
    {
        Task<ICollection<Teacher>> GetAllTeachersAsync(string jwtCookie);
        Task<Teacher?> GetTeacherByIdAsync(int id, string jwtCookie);
    }
}