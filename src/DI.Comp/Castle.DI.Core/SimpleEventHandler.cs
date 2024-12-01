using DI.Core.Utilities;

namespace DI.Core;

public interface ISimpleEventPublisher
{
    event Action<string> OnPublished;

    void Publish(string name);
}

public class SimpleEventPublisher : ISimpleEventPublisher
{
    public event Action<string> OnPublished;

    public void Publish(string name)
    {
        OnPublished.Raise(name);
    }
}

public interface IWas
{
    bool Called { get; set; }
}

public class Was : IWas
{
    public bool Called { get; set; }
}

public interface ISimpleEventHandler
{
    void Handle(string name);
}

public class SimpleEventHandler : ISimpleEventHandler
{
    private readonly IWas _was;

    public SimpleEventHandler(IWas was)
    {
        _was = was;
    }

    public void Handle(string name)
    {
        Console.WriteLine($"Handler called with {name}");
        _was.Called = true;
    }
}