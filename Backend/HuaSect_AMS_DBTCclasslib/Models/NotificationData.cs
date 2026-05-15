namespace HuaSect_AMS_DBTCclasslib.Models;

public enum NotificationType
{
    Info,
    Success,
    Warning,
    Error
}

public class NotificationData
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; } // info, success, warning, error
    public string TargetUrl { get; set; } // Optional: deep link for click action
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    // Optional: additional context for your app logic
    public Dictionary<string, object> Metadata { get; set; } = new();
}