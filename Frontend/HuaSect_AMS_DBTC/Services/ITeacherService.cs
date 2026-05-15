using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public interface ITeacherService
    {
        Task<Teacher?> GetTeacherByIdAsync(int id);

        Task<ICollection<Teacher>> GetAllTeachersAsync();
    }
}