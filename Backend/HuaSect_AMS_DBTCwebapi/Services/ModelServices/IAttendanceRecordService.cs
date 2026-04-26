using HuaSect_AMS_DBTCclasslib;
using HuaSect_AMS_DBTCclasslib.Helpers;

namespace HuaSect_AMS_DBTC.Service
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAllAttendanceRecordsAsync();
        Task<PagedResult<Attendance>> GetPaginatedAttendanceRecordsAsync(int pageNumber, int pageSize);
        Task<Attendance?> GetAttendanceRecordByIdAsync(int id);
        Task<NewlyCreateAttendanceRecordDto> CreateAttendanceRecordAsync(CreateAttendanceRecordDto dto);
        Task UpdateAttendanceRecordAsync(int id, UpdateAttendanceRecordDto dto);
        Task UpdateAttendanceRecordSelectivelyAsync(int id, UpdateAttendanceRecordDto patchedDto);
        Task<bool> DeleteAttendanceRecordAsync(int id);
    }
}