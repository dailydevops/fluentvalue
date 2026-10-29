namespace NetEvolve.FluentValue;

using System;

internal static class TypeExtensions
{
    // Nullable<T> yields null from Activator.CreateInstance, so no explicit handling is needed.
    internal static object? GetDefault(this Type value) => value.IsValueType ? Activator.CreateInstance(value) : null;
}
