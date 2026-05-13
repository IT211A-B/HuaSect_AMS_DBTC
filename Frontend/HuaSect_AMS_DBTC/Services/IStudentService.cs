using HuaSect_AMS_DBTC.Models;
namespace HuaSect_AMS_DBTC.Services
{
    public interface IStudentService
    {
        Task<ICollection<Student>> GetAllStudentsAsync();

        Task<Student?> GetStudentByIdAsync(int id);
    }
}