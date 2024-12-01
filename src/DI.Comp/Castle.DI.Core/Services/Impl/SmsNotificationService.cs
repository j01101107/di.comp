using Castle.Core;
using Castle.Core.Logging;

using DI.Core.Utilities;

namespace DI.Core.Services.Impl;

[Notification("sms")]
public class SmsNotificationService(ILogger logger) : INotificationService
{
    [Interceptor(typeof(MonitorInterceptor))]
    public bool Notify(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            logger.Error("Sms is empty in EmailNotificationService");
            return false;
        }

        logger.Info($"Sms notification sent: {message}");
        return true;
    }
}