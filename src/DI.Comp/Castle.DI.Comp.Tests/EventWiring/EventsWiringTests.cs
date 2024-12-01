using Castle.Common;
using Castle.Common.Facilities.EventWiring;
using Castle.MicroKernel.Registration;

using DI.Core;

namespace Castle.DI.Comp.Tests.EventWiring;

[TestFixture]
public class EventsWiringTests : TestBase
{
    protected override void Initialize()
    {
        Container.AddFacility<EventWiringFacility>();

        Container.Register(Component.For<IWas>().ImplementedBy<Was>(),
                           Component.For<ISimpleEventHandler>().ImplementedBy<SimpleEventHandler>().Named("s.e.h"),
                           Component.For<ISimpleEventPublisher>().ImplementedBy<SimpleEventPublisher>().Named("s.e.p")
                                     // note a couple ways of wiring up the event
                                     //.PublishEvent("OnPublished", x => x.To<ISimpleEventHandler>("s.e.h", l => l.Handle(null))));
                                    .PublishEvent(p => p.OnPublished += null,
                                                  h => h.To<ISimpleEventHandler>("s.e.h", l => l.Handle(null))));

    }

    [Test]
    public void VerifyHandlesEvent()
    {
        var publisher = IoC.Resolve<ISimpleEventPublisher>();
        publisher.Publish("Tom");

        var was = IoC.Resolve<IWas>();
        Assert.That(was.Called, Is.True);

        // by hand
        ISimpleEventPublisher publisher2 = new SimpleEventPublisher();
        ISimpleEventHandler handler2 = new SimpleEventHandler(new Was());
        publisher2.OnPublished += handler2.Handle;

        publisher2.Publish("TOMMY");
    }
}