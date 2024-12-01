using System.Diagnostics;

namespace DI.Core.Services.Impl;

public class MetricsPublisher : IMetricsPublisher
{
    private readonly string _paramName;
    private readonly Stopwatch _stopwatch;

    public MetricsPublisher(string paramName)
    {
        _paramName = paramName;

        _stopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        Console.WriteLine($"Metrics event sent: {_paramName} - {_stopwatch.ElapsedMilliseconds}");
    }
}