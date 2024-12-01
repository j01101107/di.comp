namespace DI.Core.Utilities;

public class NotificationAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}