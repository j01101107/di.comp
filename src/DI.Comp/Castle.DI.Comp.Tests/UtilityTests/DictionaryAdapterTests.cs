using Castle.Components.DictionaryAdapter;
using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Naming;

namespace Castle.DI.Comp.Tests.UtilityTests;

[TestFixture]
public class DictionaryAdapterTests : TestBase
{
    [Test]
    public void SimpleTest()
    {
        var dictionary = new Dictionary<string, object>
                         {
                             { "Name", "Mike" },
                             { "Age", 30 }
                         };

        var factory = new DictionaryAdapterFactory();
        var adapter = factory.GetAdapter<IPerson>(dictionary);

        Assert.That(adapter.Name, Is.EqualTo("Mike"));
        Assert.That(adapter.Age, Is.EqualTo(30));

        Container.Kernel.AddSubSystem(SubSystemConstants.NamingKey, new DefaultNamingSubSystem());
    }
}

public interface IPerson
{
    string Name { get; set; }

    int Age { get; set; }
}