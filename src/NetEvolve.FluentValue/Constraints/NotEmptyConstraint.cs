namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Collections;
using System.Text;

internal sealed class NotEmptyConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            string stringValue => stringValue.Length > 0,
            Guid guidValue => guidValue != Guid.Empty,
            ICollection collection => collection.Count > 0,
            IEnumerable enumerable => enumerable.GetEnumerator().MoveNext(),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) => builder.Append(" is not <empty>");
}
