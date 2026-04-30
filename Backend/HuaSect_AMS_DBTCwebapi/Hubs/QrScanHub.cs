using Microsoft.AspNetCore.SignalR;

namespace HuaSect_AMS_DBTC.Hubs;

public class QrScanHub : Hub
{
    public async Task ScanReceived(string qrData, string deviceId)
    {
        await Clients.Group("attendance-admins")
            .SendAsync("QrCodeScanned", new
            {
                QrData = qrData,
                DeviceId = deviceId,
                Timestamp = DateTime.UtcNow
            });

        await ProcessAttendanceAsync(qrData);
    }

    public async Task JoinAdminGroup()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "attendance-admins");
    }

    private async Task ProcessAttendanceAsync(string qrData)
    {
    }
}