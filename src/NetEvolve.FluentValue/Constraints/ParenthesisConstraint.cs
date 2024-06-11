namespace NetEvolve.FluentValue.Constraints;

using System.Text;
using NetEvolve.Arguments;
using NetEvolve.FluentValue;

internal sealed class ParenthesisConstraint : ConstraintBase
{
    private readonly ConstraintBase _constraint;

    public ParenthesisConstraint(IConstraint constraint)
    {
        Argument.ThrowIfNull(constraint);

        _constraint = (ConstraintBase)constraint;
    }

    public override bool IsSatisfiedBy(object? value)
    {
        Argument.ThrowIfNull(_constraint);

        return _constraint.IsSatisfiedBy(value);
    }

    public override void SetDescription(StringBuilder builder)
    {
        _ = builder.Append(" (");
        _constraint.SetDescription(builder);
        _ = builder.Replace("( ", "(").Append(')');
    }
}
