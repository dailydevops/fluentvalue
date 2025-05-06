namespace NetEvolve.FluentValue.Operators;

using System;
using System.Text;
using NetEvolve.Arguments;
using NetEvolve.FluentValue;
using NetEvolve.FluentValue.Constraints;

internal sealed class NotOperator : ConstraintBase, IOperator
{
    internal IConstraint? _constraint;

    internal NotOperator() { }

    public override bool IsSatisfiedBy(object? value)
    {
        if (_constraint is null)
        {
            throw new InvalidOperationException();
        }

        return !_constraint.IsSatisfiedBy(value);
    }

    public IConstraint SetConstraint(IConstraint constraint)
    {
        Argument.ThrowIfNull(constraint);

        _constraint = constraint;

        return this;
    }

    public override void SetDescription(StringBuilder builder)
    {
        _ = builder.Append(" not");
        _constraint?.SetDescription(builder);
    }
}
