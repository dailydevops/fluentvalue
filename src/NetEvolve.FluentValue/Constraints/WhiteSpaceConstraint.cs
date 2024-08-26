namespace NetEvolve.FluentValue.Constraints;

using System.Text;

internal sealed class WhiteSpaceConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            string stringValue => string.IsNullOrWhiteSpace(stringValue),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" is <whitespace>");
}
