using HuaSect_AMS_DBTC.Repository;
using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.Helpers;

namespace HuaSect_AMS_DBTC.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ICourseRepository _courseRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, ICourseRepository courseRepository)
        {
            _attendanceRepository = attendanceRepository;
            _courseRepository = courseRepository;
        }

        public async Task<List<Attendance>> GetAllAttendanceRecordsAsync() => await _attendanceRepository.GetAllAsync();

        public async Task<PagedResult<Attendance>> GetPaginatedAttendanceRecordsAsync(int pageNumber, int pageSize)
        {
            var (total, data) = await _attendanceRepository.GetPaginatedAsync(pageNumber, pageSize);
            return new PagedResult<Attendance>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total
            };
        }

        public async Task<List<Attendance>> GetAllStudentAttendanceRecordsAsync(int id) => await _attendanceRepository.GetAllByStudentAsync(id);

        public async Task<Attendance?> GetAttendanceRecordByIdAsync(int id) => await _attendanceRepository.GetByIdAsync(id);

        public async Task<NewlyCreateAttendanceRecordDto> CreateAttendanceRecordAsync(CreateAttendanceRecordDto dto)
        {
            var course = await _courseRepository.GetByIdAsync(dto.CourseId) ?? throw new Exception("Error: Course couldn't be found");
            var newAttendanceRecord = new Attendance(dto.Date, dto.Status, course);
            await _attendanceRepository.AddAsync(newAttendanceRecord);
            await _attendanceRepository.SaveChangesAsync();

            return new NewlyCreateAttendanceRecordDto
            {
                ID = newAttendanceRecord.ID,
                Date = newAttendanceRecord.Date,
                Status = newAttendanceRecord.Status,
            };
        }

        public async Task UpdateAttendanceRecordAsync(int id, UpdateAttendanceRecordDto dto)
        {
            if (id != dto.ID)
                throw new ArgumentException("Course ID mismatch", nameof(id));

            var attendanceRecord = await _attendanceRepository.GetByIdAsync(id);
            if (attendanceRecord == null)
                throw new KeyNotFoundException($"Attendance Record with id = {id} not found");

            var course = await _courseRepository.GetByIdAsync(dto.CourseId) ?? throw new Exception("Error: Course couldn't be found");
            attendanceRecord.Update(dto.ID, dto.Date, dto.Status, course);
            await _attendanceRepository.SaveChangesAsync();
        }

        public async Task UpdateAttendanceRecordSelectivelyAsync(int id, UpdateAttendanceRecordDto patchedDto)
        {
            var attendanceRecord = await _attendanceRepository.GetByIdAsync(id);
            if (attendanceRecord == null)
                throw new KeyNotFoundException($"Attendance Record with id = {id} not found");

            var course = await _courseRepository.GetByIdAsync(patchedDto.CourseId) ?? throw new Exception("Error: Course couldn't be found");
            attendanceRecord.Update(patchedDto.ID, patchedDto.Date, patchedDto.Status, course);
            await _attendanceRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAttendanceRecordAsync(int id)
        {
            var attendanceRecord = await _attendanceRepository.GetByIdAsync(id);
            if (attendanceRecord == null)
                throw new KeyNotFoundException($"Attendance Record with id = {id} not found");

            await _attendanceRepository.DeleteAsync(attendanceRecord);
            await _attendanceRepository.SaveChangesAsync();
            return true;
        }
    }
}