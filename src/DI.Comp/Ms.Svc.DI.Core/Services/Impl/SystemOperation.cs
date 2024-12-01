using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ms.Svc.DI.Core.Services.Impl;

public class SystemOperation : ISystemOperation
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<SystemOperation> _logger;

    // note the [FromKeyedServices("sms")] attribute which is a recent improvement in the MS implementation
    public SystemOperation([FromKeyedServices("sms")] INotificationService notificationService, ILogger<SystemOperation> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    // I can't do interception naively here because it's not supported. You can roll your own interception mechanism though.
    public void DoSystemOperation(string name)
    {
        _logger.LogInformation($"System operation started: {name}");
        _notificationService.Notify($"System operation started: {name}");
        _logger.LogInformation($"System operation completed: {name}");
    }
}