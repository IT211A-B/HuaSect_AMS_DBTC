using HuaSect_AMS_DBTC.Models;

namespace HuaSect_AMS_DBTC.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public StudentService(HttpClient httpClient, IConfiguration config) {
            _httpClient = httpClient;
            _config = config;
        }
    }
}
