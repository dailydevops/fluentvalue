namespace NetEvolve.FluentValue.Constraints;

using System.Text;
using NetEvolve.FluentValue;

internal sealed class NotDefaultConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) =>
        value?.GetType() switch
        {
            { IsValueType: true } valueType => !TypeExtensions.GetDefault(valueType)?.Equals(value) ?? false,
            _ => true,
        };

    public override void SetDescription(StringBuilder builder) => builder.Append(" is not <default>");
}
