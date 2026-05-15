using HuaSect_AMS_DBTC.Models;
namespace HuaSect_AMS_DBTC.Services
{
    public interface IAuthService
    {
        Task<LogInResponseModel> LoginAsync(LogInModel model);

        Task<LogInResponseModel> RegisterStudentAsync(LogInModel model);

        Task<LogInResponseModel> RegisterTeacherAsync(LogInModel model);

        Task<LogInResponseModel> ConfirmEmailAsync(LogInModel model);
    }
}