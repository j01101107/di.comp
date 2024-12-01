using Castle.Core.Logging;

using DI.Core.Models;
using DI.Core.Utilities;

namespace DI.Core.Services.Impl;


public class SubForwarder : IForwarder
{
    private readonly ILogger _logger;

    public SubForwarder(ILogger logger)
    {
        _logger = logger;
    }

    public void ForwardMessage(IMessage message)
    {
        _logger.Debug($"Forwarding by {nameof(SubForwarder)} at {SystemTime.Now():O} with message {message.Id}");
    }
}


public class PreForwarder : IForwarder
{
    private readonly ILogger _logger;

    public PreForwarder(ILogger logger)
    {
        _logger = logger;
    }

    public void ForwardMessage(IMessage message)
    {
        _logger.Debug($"Forwarding by {nameof(PreForwarder)} at {SystemTime.Now():O} with message {message.Id}");
    }
}


public class PostForwarder : IForwarder
{
    private readonly ILogger _logger;

    public PostForwarder(ILogger logger)
    {
        _logger = logger;
    }

    public void ForwardMessage(IMessage message)
    {
        _logger.Debug($"Forwarding by {nameof(PostForwarder)} at {SystemTime.Now():O} with message {message.Id}");
    }
}