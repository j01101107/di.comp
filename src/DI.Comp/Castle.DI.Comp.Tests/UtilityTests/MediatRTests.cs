using Castle.Common;
using Castle.MediatR.DependencyInjection;
using Castle.MicroKernel.Registration;

using MediatR;

namespace Castle.DI.Comp.Tests.UtilityTests;

[TestFixture]
public class MediatRTests : TestBase
{
    protected override void Initialize()
    {
        Container.UseMediatR(Classes.FromAssemblyContaining<MediatRTests>());
    }

    [Test]
    public async Task WhatIsNeededToBeRegistered()
    {
        var mediator = IoC.Resolve<IMediator>();
        await mediator.Publish(new InvalidResetCodeEvent(10, "Test"));

        var response = await mediator.Send(new Ping());
        Console.WriteLine(response); // "Pong"
    }
}

public class Ping : IRequest<string>
{
    public override string ToString()
    {
        return "Ping";
    }
}

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> Handle(Ping request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"I've been {request.ToString()}");
        return Task.FromResult("Pong");
    }
}

public class InvalidResetCodeEventHandler : INotificationHandler<InvalidResetCodeEvent>
{
    public async Task Handle(InvalidResetCodeEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Handling Event!");

        await Task.CompletedTask;
    }
}

public class InvalidResetCodeEvent : INotification
{
    public ulong UserOid { get; }

    public string EventName { get; }

    public InvalidResetCodeEvent(ulong oid, string eventName)
    {
        UserOid = oid;
        EventName = eventName;
    }
}