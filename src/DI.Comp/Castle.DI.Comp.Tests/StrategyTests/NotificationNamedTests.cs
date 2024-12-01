

using Castle.Common;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;

using DI.Core.Services;
using DI.Core.Services.Impl;
using DI.Core.Utilities;

namespace Castle.DI.Comp.Tests.StrategyTests;

[TestFixture]
public class NotificationNamedTests : TestBase
{
    protected override void Initialize()
    {
        Container.Register(Component.For<IMetricsPublisher>().ImplementedBy<MetricsPublisher>().LifestyleTransient(),
                           Component.For<IInterceptor>().ImplementedBy<MonitorInterceptor>().LifestyleTransient(),
                           Component.For<INotificationService>().ImplementedBy<EmailNotificationService>().Interceptors<MonitorInterceptor>().Named("email").LifestyleTransient(),
                           Component.For<INotificationService>().ImplementedBy<SmsNotificationService>().Interceptors<MonitorInterceptor>().Named("sms").LifestyleTransient(),
                           Component.For<ISystemOperation>().ImplementedBy<SystemOperation>().Interceptors<MonitorInterceptor>().LifestyleTransient()
                                    .DependsOn(Dependency.OnComponent("notificationService", "sms"))); // change to "email" to test the other implementation
    }


    [Test]
    public void TestCallingCorrectImplementation()
    {
        var service = IoC.Resolve<ISystemOperation>();
        service.DoSystemOperation("Test");
    }
}