using ReSale.Web.Components;

namespace ReSale.Web.Services;

public class NotificationService
{
#pragma warning disable CA1003
    public event Func<string, string, Notification.NotificationType, Task>? OnShow;
#pragma warning restore CA1003

    public Task Show(string title, string message, Notification.NotificationType type) => OnShow?.Invoke(title, message, type) ?? Task.CompletedTask;
}
