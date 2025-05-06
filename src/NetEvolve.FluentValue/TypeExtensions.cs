namespace NetEvolve.FluentValue;

using System;

internal static class TypeExtensions
{
    internal static object GetDefault(this Type value)
    {
        var underlying = Nullable.GetUnderlyingType(value);
        if (underlying is not null)
        {
            return Activator.CreateInstance(underlying)!;
        }
        return Activator.CreateInstance(value)!;
    }
}
