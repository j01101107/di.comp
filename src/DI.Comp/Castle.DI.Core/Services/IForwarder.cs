using DI.Core.Models;

namespace DI.Core.Services;

public interface IForwarder
{
    void ForwardMessage(IMessage message);
}