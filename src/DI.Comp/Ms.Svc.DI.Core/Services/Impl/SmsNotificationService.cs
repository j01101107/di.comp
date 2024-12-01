using Microsoft.Extensions.Logging;

namespace Ms.Svc.DI.Core.Services.Impl;

public class SmsNotificationService(ILogger<SmsNotificationService> logger) : INotificationService
{
    public bool Notify(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            logger.LogError("Message is empty in SmsNotificationService");
            return false;
        }

        logger.LogInformation($"Sms notification sent: {message}");
        return true;
    }
}