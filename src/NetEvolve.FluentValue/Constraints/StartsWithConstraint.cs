namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;

internal sealed class StartsWithConstraint : ConstraintBase
{
    private readonly object? _compareValue;
    private readonly StringComparison? _comparison;

    public StartsWithConstraint(char compareValue) => _compareValue = compareValue;

    public StartsWithConstraint(string compareValue, StringComparison comparison)
    {
        _compareValue = compareValue;
        _comparison = comparison;
    }

    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            string stringValue when _compareValue is char compareValue
                => stringValue.StartsWith(compareValue),
            string stringValue when _compareValue is string compareValue
                => stringValue.StartsWith(compareValue, _comparison ?? default),
            _ => false
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" starts with `").Append(_compareValue).Append('`');
}
