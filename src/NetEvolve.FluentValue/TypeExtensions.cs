namespace NetEvolve.FluentValue;

using System;

internal static class TypeExtensions
{
    internal static object? GetDefault(this Type value)
    {
        // Nullable<T> yields null from Activator.CreateInstance, so no explicit handling is needed.
        return value.IsValueType ? Activator.CreateInstance(value) : null;
    }
}
