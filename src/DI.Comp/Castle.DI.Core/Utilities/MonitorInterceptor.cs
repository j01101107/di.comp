using Castle.Core.Logging;
using Castle.DynamicProxy;

using DI.Core.Services.Impl;

namespace DI.Core.Utilities;

// a very cool use of interceptors is to use it as a cache mechanism
public class MonitorInterceptor : IInterceptor
{
    private readonly ILogger _logger;

    public MonitorInterceptor(ILogger logger)
    {
        _logger = logger;
    }
    public void Intercept(IInvocation invocation)
    {
        _logger.Debug("Enters");
        try
        {
            var parameterInfo = invocation.MethodInvocationTarget.GetParameters().FirstOrDefault(pi => pi.Position == 0);
            using (new MetricsPublisher(parameterInfo?.Name ?? "Missing"))
            {
                invocation.Proceed();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message, ex);
            throw;
        }

        _logger.Debug("Exits");
    }
}