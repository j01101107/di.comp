namespace DI.Core.Utilities;

public static class SystemTime
{
    /// <summary>
    /// Defaults to DateTime.Now;
    ///
    /// For testing:
    /// SystemTime.Now = () => new DateTime(2000,1,1);
    /// </summary>
    public static Func<DateTime> Now = () => DateTime.Now;

    /// <summary>
    /// Defaults to DateTime.UtcNow;
    /// </summary>
    public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
}