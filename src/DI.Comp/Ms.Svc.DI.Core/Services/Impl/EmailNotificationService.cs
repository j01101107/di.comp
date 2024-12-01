using Microsoft.Extensions.Logging;

namespace Ms.Svc.DI.Core.Services.Impl;

public class EmailNotificationService(ILogger<EmailNotificationService> logger) : INotificationService
{
    public bool Notify(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            logger.LogError("Message is empty in EmailNotificationService");
            return false;
        }

        logger.LogInformation($"Email notification sent: {message}");
        return true;
    }
}