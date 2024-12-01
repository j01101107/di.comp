using Microsoft.Extensions.DependencyInjection;

using Ms.Svc.DI.Core.Services;
using Ms.Svc.DI.Core.Services.Impl;

namespace Ms.Svc.DI.Comp.Tests.StrategyTests;

[TestFixture]
public class NotificationKeyedTests : TestBase
{
    protected override void Register()
    {
        ServiceCollection.AddKeyedSingleton<INotificationService, EmailNotificationService>("email");
        ServiceCollection.AddKeyedSingleton<INotificationService, SmsNotificationService>("sms");
        ServiceCollection.AddSingleton<ISystemOperation, SystemOperation>();
    }

    /// <summary>
    ///     This is about the extent of the coolness of the Keyed DI.
    /// </summary>
    [Test]
    public void TestCallingCorrectImplementation()
    {
        var operation = ServiceProvider.GetRequiredService<ISystemOperation>();
        operation.DoSystemOperation("Test");
    }
}