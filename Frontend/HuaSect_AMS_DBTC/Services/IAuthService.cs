using HuaSect_AMS_DBTC.Models;
namespace HuaSect_AMS_DBTC.Services
{
    public interface IAuthService
    {
        Task<LogInResponseModel> LoginAsync(LogInModel model);

        Task RegisterStudentAsync(RegisterStudentModel model);

        Task RegisterTeacherAsync(RegisterTeacherModel model);

        Task ConfirmEmailAsync(string userId, string token);
    }
}