namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;
using NetEvolve.FluentValue;

internal sealed class DefaultConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) =>
        value?.GetType() switch
        {
            { IsValueType: true } valueType => TypeExtensions.GetDefault(valueType).Equals(value),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) => builder.Append(" is <default>");
}
