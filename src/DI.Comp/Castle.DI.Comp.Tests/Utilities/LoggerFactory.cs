namespace Castle.DI.Comp.Tests.Utilities;

public class LoggerFactory : ILoggerFactory
{
    public static ILogger Logger { get; set; } = new NullLogger();

    public ILogger Create(Type type)
    {
        return Logger;
    }

    public ILogger Create(string name)
    {
        return Logger;
    }

    public ILogger Create(Type type, LoggerLevel level)
    {
        return Logger;
    }

    public ILogger Create(string name, LoggerLevel level)
    {
        return Logger;
    }
}