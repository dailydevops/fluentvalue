namespace NetEvolve.FluentValue.Constraints;

using System.Text;
using NetEvolve.FluentValue;

internal sealed class DefaultConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) =>
        value?.GetType() switch
        {
            { IsValueType: true } valueType => Equals(TypeExtensions.GetDefault(valueType), value),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) => builder.Append(" is <default>");
}
