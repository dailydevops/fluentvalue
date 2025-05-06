namespace NetEvolve.FluentValue.Constraints;

using System.Text;

internal sealed class NotNullConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) => value is not null;

    public override void SetDescription(StringBuilder builder) => builder.Append(" is not <null>");
}
