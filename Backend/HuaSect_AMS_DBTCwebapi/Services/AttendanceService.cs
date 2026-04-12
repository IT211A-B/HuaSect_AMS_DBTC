using HuaSect_AMS_DBTC.Repository;
using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.Helpers;
using HuaSect_AMS_DBTC.Service;

namespace HuaSect_AMS_DBTC.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;

        public AttendanceService(IAttendanceRepository repository) => _repository = repository;

        public async Task<List<Attendance>> GetAllAttendanceRecordsAsync() => await _repository.GetAllAsync();

        public async Task<PagedResult<Attendance>> GetPaginatedAttendanceRecordsAsync(int pageNumber, int pageSize)
        {
            var (total, data) = await _repository.GetPaginatedAsync(pageNumber, pageSize);
            return new PagedResult<Attendance>
            {
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = total
            };
        }

        public async Task<Attendance?> GetAttendanceRecordByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<NewlyCreateAttendanceRecordDto> CreateAttendanceRecordAsync(CreateAttendanceRecordDto dto)
        {
            var newAttendanceRecord = new Attendance(dto.Date, dto.Status);
            await _repository.AddAsync(newAttendanceRecord);
            await _repository.SaveChangesAsync();

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

            var attendanceRecord = await _repository.GetByIdAsync(id);
            if (attendanceRecord == null)
                throw new KeyNotFoundException($"Attendance Record with id = {id} not found");

            attendanceRecord.Update(dto.ID, dto.Date, dto.Status);
            await _repository.SaveChangesAsync();
        }

        // This method assumes validation & patch application happened in the controller
        public async Task UpdateAttendanceRecordSelectivelyAsync(int id, UpdateAttendanceRecordDto patchedDto)
        {
            var attendanceRecord = await _repository.GetByIdAsync(id);
            if (attendanceRecord == null)
                throw new KeyNotFoundException($"Attendance Record with id = {id} not found");

            attendanceRecord.Update(patchedDto.ID, patchedDto.Date, patchedDto.Status);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAttendanceRecordAsync(int id)
        {
            var attendanceRecord = await _repository.GetByIdAsync(id);
            if (attendanceRecord == null)
                throw new KeyNotFoundException($"Attendance Record with id = {id} not found");

            await _repository.DeleteAsync(attendanceRecord);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}