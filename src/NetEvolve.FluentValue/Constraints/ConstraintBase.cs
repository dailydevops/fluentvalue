namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NetEvolve.FluentValue;

[SuppressMessage("Info Code Smell", "S1133:Deprecated code should be removed", Justification = "As designed.")]
[SuppressMessage(
    "Blocker Code Smell",
    "S3877:Exceptions should not be thrown from unexpected methods",
    Justification = "As designed."
)]
internal abstract class ConstraintBase : IConstraint
{
    [ThreadStatic]
    private static StringBuilder? _builder;

    private const int DefaultCapacity = 1024;

    /// <inheritdoc />
    public abstract bool IsSatisfiedBy(object? value);

    /// <inheritdoc />
    public abstract void SetDescription(StringBuilder builder);

    /// <inhertdoc />
    [Obsolete("This is base `object` method that should not be called.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerHidden]
    public new bool Equals(object? obj) =>
        throw new NotSupportedException("This is base `object` method that should not be called.");

    /// <inheritdoc/>
    [Obsolete("This is base `object` method that should not be called.", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerHidden]
    public new int GetHashCode() =>
        throw new NotSupportedException("This is base `object` method that should not be called.");

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
