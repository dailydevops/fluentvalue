namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;
using NetEvolve.FluentValue;

internal abstract class ConstraintBase : IConstraint
{
    [ThreadStatic]
    private static StringBuilder? _builder;

    private const int DefaultCapacity = 1024;

    public abstract bool IsSatisfiedBy(object? value);

    public abstract void SetDescription(StringBuilder builder);

    public override string ToString()
    {
        var builder = _builder ?? new StringBuilder(capacity: DefaultCapacity);
#pragma warning disable S2696 // Instance members should not write to "static" fields
        _builder = null;
#pragma warning restore S2696 // Instance members should not write to "static" fields
        try
        {
            _ = builder.Append('"').Append("{Value}");

            SetDescription(builder);

            return builder.Append('.').Append('"').ToString();
        }
        finally
        {
            _ = builder.Clear();
            if (builder.Capacity > DefaultCapacity)
            {
                builder.Capacity = DefaultCapacity;
            }
            _builder = builder;
        }
    }
}
