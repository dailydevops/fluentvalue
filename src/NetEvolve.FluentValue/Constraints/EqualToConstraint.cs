namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;

internal sealed class EqualToConstraint : ConstraintBase
{
    private readonly object? _compareValue;
    private readonly StringComparison? _comparison;

    internal EqualToConstraint(object? compareValue) => _compareValue = compareValue;

    internal EqualToConstraint(string compareValue, StringComparison comparison)
    {
        _compareValue = compareValue;
        _comparison = comparison;
    }

    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            null => _compareValue is null,
            string stringValue when _compareValue is string compareValue => stringValue.Equals(
                compareValue,
                _comparison ?? default
            ),
            string stringValue when _compareValue is IConvertible convertible => stringValue.Equals(
                convertible.ToString(),
                _comparison ?? default
            ),
            _ => value.Equals(_compareValue),
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" is equal to `").Append(_compareValue).Append('`');
}
