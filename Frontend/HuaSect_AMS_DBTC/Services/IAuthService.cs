using HuaSect_AMS_DBTC.Models;
namespace HuaSect_AMS_DBTC.Services
{
    public interface IAuthService
    {
        Task<LogInResponseModel> LoginAsync(LogInModel model);

        Task<LogInResponseModel> RegisterStudentAsync(RegisterStudentModel model);

        Task<LogInResponseModel> RegisterTeacherAsync(RegisterTeacherModel model);

        Task<LogInResponseModel> ConfirmEmailAsync(LogInModel model);
    }
}