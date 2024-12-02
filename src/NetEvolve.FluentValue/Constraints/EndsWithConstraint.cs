namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;

internal sealed class EndsWithConstraint : ConstraintBase
{
    private readonly object? _compareValue;
    private readonly StringComparison? _comparison;

    internal EndsWithConstraint(char compareValue) => _compareValue = compareValue;

    internal EndsWithConstraint(string compareValue, StringComparison comparison)
    {
        _compareValue = compareValue;
        _comparison = comparison;
    }

    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            string stringValue when _compareValue is string compare => stringValue.EndsWith(
                compare,
                _comparison ?? default
            ),
            string stringValue when _compareValue is char compare => stringValue.EndsWith(compare),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" ends with `").Append(_compareValue).Append('`');
}
