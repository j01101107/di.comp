using Castle.Core;
using Castle.Core.Logging;

using DI.Core.Utilities;

namespace DI.Core.Services.Impl;

[Notification("email")]
public class EmailNotificationService(ILogger logger) : INotificationService
{
    [Interceptor(typeof(MonitorInterceptor))]
    public bool Notify(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            logger.Error("Message is empty in EmailNotificationService");
            return false;
        }

        logger.Info($"Email notification sent: {message}");
        return true;
    }
}