namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Collections;
using System.Linq;
using System.Text;

internal sealed class ContainsConstraint : ConstraintBase
{
    private readonly object? _compareValue;
    private readonly StringComparison? _comparison;

    internal ContainsConstraint(char compareValue, StringComparison comparison)
    {
        _compareValue = compareValue;
        _comparison = comparison;
    }

    internal ContainsConstraint(string compareValue, StringComparison comparison)
    {
        _compareValue = compareValue;
        _comparison = comparison;
    }

    public ContainsConstraint(object? compareValue) => _compareValue = compareValue;

    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            null => false,
            string stringValue when _compareValue is string compareValue => stringValue.Contains(
                compareValue,
                _comparison ?? default
            ),
            string stringValue when _compareValue is char compareValue => stringValue.Contains(
                compareValue,
                _comparison ?? default
            ),
            IDictionary dictionary => dictionary.Contains(_compareValue!),
            IList list => list.Contains(_compareValue),
            IEnumerable enumerable => enumerable.Cast<object?>().Contains(_compareValue),
            _ => throw new NotSupportedException($"Invalid type `{value!.GetType().FullName}`."),
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" contains `").Append(_compareValue).Append('`');
}
