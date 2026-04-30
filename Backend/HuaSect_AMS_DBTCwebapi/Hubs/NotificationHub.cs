using Microsoft.AspNetCore.SignalR;

namespace HuaSect_AMS_DBTC.Hubs;

public class NotificationHub : Hub
{
    public async Task SendToUser(string userId, NotificationData data)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", data);
    }
    
    public async Task SendToAdmins(NotificationData data)
    {
        await Clients.Group("admins").SendAsync("ReceiveNotification", data);
    }
}