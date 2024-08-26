namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;

internal sealed class EqualToConstraint : ConstraintBase
{
    private readonly object? _compareValue;
    private readonly StringComparison? _comparison;

    public EqualToConstraint(object? compareValue) => _compareValue = compareValue;

    public EqualToConstraint(string compareValue, StringComparison comparison)
    {
        _compareValue = compareValue;
        _comparison = comparison;
    }

    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            string stringValue when _compareValue is string compareValue => stringValue.Equals(
                compareValue,
                _comparison ?? default
            ),
            string stringValue when _compareValue is IConvertible convertible => stringValue.Equals(
                convertible.ToString(),
                _comparison ?? default
            ),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" is equal to `").Append(_compareValue).Append('`');
}
