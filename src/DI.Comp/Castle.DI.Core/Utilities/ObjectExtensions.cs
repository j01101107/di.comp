namespace DI.Core.Utilities;

public static class ObjectExtensions
{
    public static void Raise(this Action action)
    {
        var a = action;
        if (a != null)
        {
            a();
        }
    }

    public static void Raise<T>(this Action<T> action, T argument)
    {
        var a = action;
        if (a != null)
        {
            a(argument);
        }
    }

    public static void Raise<T, U>(this Action<T, U> action, T t, U u)
    {
        var a = action;
        if (a != null)
        {
            a(t, u);
        }
    }

    public static void Raise<T, U, V>(this Action<T, U, V> action, T t, U u, V v)
    {
        var a = action;
        if (a != null)
        {
            a(t, u, v);
        }
    }

    public static void Raise<T, U, V, W>(this Action<T, U, V, W> action, T t, U u, V v, W w)
    {
        var a = action;
        if (a != null)
        {
            a(t, u, v, w);
        }
    }
}