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

        public async Task AddStudentAsync(StudentModel student)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_config["BackendUrl"]}/api/Student", student);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_config["BackendUrl"]}/api/Student/{id}"); // potentially dangerous because of injection vulnerability
        }

        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync()
        {
            var response = await _httpClient.GetAsync($"{_config["BackendUrl"]}/api/Student");

            response.EnsureSuccessStatusCode();

            var students = await response.Content.ReadFromJsonAsync<IEnumerable<StudentModel>>();

            return students ?? Enumerable.Empty<StudentModel>();

        }

        public async Task<IEnumerable<AttendanceRecord>> GetAttendanceByStudentIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentModel?> GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentListViewModel> GetStudentListViewModelAsync(string course, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudentModel>> GetStudentsByCourseAsync(string courseId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAttendanceAsync(List<AttendanceInputModel> records)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStudentAsync(StudentModel student)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_config["BackendUrl"]}/api/Student/{student.Id}", student);
        }
    }
}
