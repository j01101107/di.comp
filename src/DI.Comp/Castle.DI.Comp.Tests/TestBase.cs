using Castle.Common;
using Castle.DI.Comp.Tests.Utilities;
using Castle.Facilities.Logging;
using Castle.Windsor;

namespace Castle.DI.Comp.Tests;

public class TestBase
{
    protected static IWindsorContainer Container { get; private set; } = null!;

    protected static ILogger Logger { get; private set; } = null!;

    [SetUp]
    public void Setup()
    {
        PrepareLogger();

        Container = CreateContainer();
        Container.AddFacility<LoggingFacility>(facility => facility.LogUsing<LoggerFactory>());

        Assert.That(Container, Is.Not.Null, "Container cannot be null");

        IoC.Initialize(Container);

        Initialize();
    }


    protected virtual void Initialize()
    {
        /* no-op */
    }

    [TearDown]
    public void TearDown()
    {
        CleanUp();

        Container?.Dispose();
    }

    protected virtual void CleanUp()
    {
        /* no-op */
    }

    protected virtual IWindsorContainer CreateContainer()
    {
        return new WindsorContainer();
    }

    protected  virtual void PrepareLogger()
    {
        Logger = new FauxLogger();
        LoggerFactory.Logger = Logger;
    }
}