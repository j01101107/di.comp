using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ms.Svc.DI.Comp.Tests;

public abstract class TestBase
{
    public IServiceCollection ServiceCollection { get; private set; } = null!;

    public IServiceProvider ServiceProvider { get; private set; } = null!;

    [SetUp]
    public void Setup()
    {
        ServiceCollection = new ServiceCollection();

        ServiceCollection.AddLogging(cfg => cfg.AddConsole())
                         .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Trace);

        Register();

        ServiceProvider = ServiceCollection.BuildServiceProvider();

        Initialize();
    }

    /// <summary>
    /// For implementations to do registration
    /// </summary>
    protected virtual void Register()
    {
        /* no-op */
    }

    /// <summary>
    /// For implementations to initialize from the provider
    /// Yeh.. 2 steps. Wonderful.
    /// </summary>
    protected virtual void Initialize()
    {
        /* no-op */
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();

        ServiceCollection?.Clear();

        ((IDisposable)ServiceProvider)?.Dispose();
    }

    protected virtual void CleanUp()
    {
        /* no-op */
    }
}