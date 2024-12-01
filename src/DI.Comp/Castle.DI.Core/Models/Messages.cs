namespace DI.Core.Models;

public interface IMessage
{
    Guid Id { get; set; }

    string CorrelationId { get; set; }

    string Message { get; set; }
}

public class AdjustmentExceptionResponse : IMessage
{
    public string Name => "IRanIntoAnException";

    public Guid Id { get; set; }

    public string CorrelationId { get; set; }

    public string Message { get; set; }
}

public class ActionResponse : IMessage
{
    public string Name => "WellDoSomething";

    public Guid Id { get; set; }

    public string CorrelationId { get; set; }

    public string Message { get; set; }
}

public class MiscResponse : IMessage
{
    public Guid Id { get; set; }

    public string CorrelationId { get; set; }

    public string Message { get; set; }
}

public class DefaultResponse : IMessage
{
    public Guid Id { get; set; }

    public string CorrelationId { get; set; }

    public string Message { get; set; }
}

public class DataRequest : IMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CorrelationId { get; set; } = Guid.NewGuid().ToString("N").ToUpper();

    public string Message { get; set; } = Guid.NewGuid().ToString("N");
}