namespace NetEvolve.FluentValue;

using System;

internal static class TypeExtensions
{
    internal static object? GetDefault(this Type value)
    {
        if (!value.IsValueType)
        {
            return null;
        }

        var underlying = Nullable.GetUnderlyingType(value);
        if (underlying is not null)
        {
            return null;
        }
        return Activator.CreateInstance(value)!;
    }
}
