using Castle.Core;
using Castle.Core.Logging;

using DI.Core.Utilities;

namespace DI.Core.Services.Impl;

public class SystemOperation : ISystemOperation
{
    private readonly INotificationService _notificationService;
    private readonly ILogger _logger;

    public SystemOperation(INotificationService notificationService, ILogger logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    [Interceptor(typeof(MonitorInterceptor))]
    public void DoSystemOperation(string name)
    {
        _logger.Info($"System operation started: {name}");
        _notificationService.Notify($"System operation started: {name}");
        _logger.Info($"System operation completed: {name}");
    }
}