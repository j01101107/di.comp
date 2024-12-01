using Castle.Common;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;

using DI.Core.Services;
using DI.Core.Services.Impl;
using DI.Core.Utilities;

namespace Castle.DI.Comp.Tests.StrategyTests;

[TestFixture]
public class NotificationAttributedNamedTests : TestBase
{
    protected override void Initialize()
    {
        Container.Register(Classes.FromAssemblyContaining<NotificationAttribute>()
                                  .BasedOn<INotificationService>()
                                  .WithService.FirstInterface()
                                  .LifestyleTransient()
                                  .Configure(component => component.Named(((NotificationAttribute) component.Implementation.GetCustomAttributes(typeof(NotificationAttribute), false).First()).Name)));
        Container.Register(Component.For<IMetricsPublisher>().ImplementedBy<MetricsPublisher>().LifestyleTransient(),
                           Component.For<IInterceptor>().ImplementedBy<MonitorInterceptor>().LifestyleTransient(),
                           Component.For<ISystemOperation>().ImplementedBy<SystemOperation>().Interceptors<MonitorInterceptor>().LifestyleTransient()
                                    .DependsOn(Dependency.OnComponent("notificationService", "sms")));
    }


    [Test]
    public void TestCallingCorrectImplementation()
    {
        var service = IoC.Resolve<ISystemOperation>();
        service.DoSystemOperation("Test");
    }
    
    [Test]
    public void HowManyAreThere()
    {
        var notificationServices = IoC.ResolveAll<INotificationService>();
        Assert.That(notificationServices.Length, Is.EqualTo(2));
        foreach (var notificationService in notificationServices)
        {
            notificationService.Notify("Testing");
        }
    }
}