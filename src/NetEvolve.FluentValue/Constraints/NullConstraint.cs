namespace NetEvolve.FluentValue.Constraints;

using System.Text;

internal sealed class NullConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) => value is null;

    public override void SetDescription(StringBuilder builder) => builder.Append(" is <null>");
}
