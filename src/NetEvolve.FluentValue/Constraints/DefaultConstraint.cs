namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;

internal sealed class DefaultConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) =>
        value?.GetType() switch
        {
            { IsValueType: true } valueType => GetDefault(valueType).Equals(value),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) => builder.Append(" is <default>");

    private static object GetDefault(Type value)
    {
        var underlying = Nullable.GetUnderlyingType(value);
        if (underlying is not null)
        {
            return Activator.CreateInstance(underlying)!;
        }
        return Activator.CreateInstance(value)!;
    }
}
