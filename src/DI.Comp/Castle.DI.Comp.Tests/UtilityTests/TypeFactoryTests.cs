using Castle.Common;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;

using DI.Core.Models;
using DI.Core.Services;
using DI.Core.Services.Impl;
using DI.Core.Utilities;

namespace Castle.DI.Comp.Tests.UtilityTests;

[TestFixture]
public class TypeFactoryTests : TestBase
{
    protected override void Initialize()
    {
        Container.AddFacility<TypedFactoryFacility>();

        Container.Register(Component.For<IGenericComponent>()
                                    .ImplementedBy<GenericComponent>(),
                           Component.For<IForwarder>()
                                    .ImplementedBy<SubForwarder>()
                                    .Named("SubForwarder")
                                    .LifeStyle.Transient,
                           Component.For<IForwarder>()
                                    .ImplementedBy<PreForwarder>()
                                    .Named("PreForwarder")
                                    .LifeStyle.Transient,
                           Component.For<IForwarder>()
                                    .ImplementedBy<PostForwarder>()
                                    .Named("PostForwarder")
                                    .LifeStyle.Transient,
                           Component.For<IForwarderComponentFactory>()
                                    .AsFactory(),
                           Component.For<IGenericFactory>()
                                    .AsFactory());

        SystemTime.Now = () => new DateTime(2017,1,2);
    }

    [Test]
    public void GenericTypeFactoryTest()
    {
        var factory = IoC.Resolve<IForwarderComponentFactory>();
        var genFactory = IoC.Resolve<IGenericFactory>();

        var message = new MiscResponse
                      {
                          Id = Guid.NewGuid(),
                          CorrelationId = "Test1",
                          Message = "OK"
                      };

        var genericComponent = genFactory.Create<IGenericComponent>();

        genericComponent.Execute(factory.GetSubForwarder(), message);

        genericComponent.Execute(factory.GetPreForwarder(),message);

        genericComponent.Execute(factory.GetPostForwarder(),message);
    }
}

public interface IForwarderComponentFactory
{
    IForwarder GetSubForwarder();
    IForwarder GetPreForwarder();
    IForwarder GetPostForwarder();

    void Release(IForwarder component);
}

public interface IGenericFactory
{
    T Create<T>();
}

public interface IGenericComponent
{
    void Execute(IForwarder forwarder, IMessage message);
}

public class GenericComponent : IGenericComponent
{
    public void Execute(IForwarder forwarder, IMessage message)
    {
        forwarder.ForwardMessage(message);
    }
}