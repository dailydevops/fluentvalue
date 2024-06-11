namespace NetEvolve.FluentValue.Operators;

using System;
using System.Text;
using NetEvolve.Arguments;
using NetEvolve.FluentValue;

internal sealed class NotOperator : OperatorBase
{
    internal IConstraint? _constraint;

    public NotOperator() { }

    public NotOperator(IConstraint constraint) => _constraint = constraint;

    public override bool IsSatisfiedBy(object? value)
    {
        if (_constraint is null)
        {
            throw new InvalidOperationException();
        }

        return !_constraint.IsSatisfiedBy(value);
    }

    public override IConstraint SetConstraint(IConstraint constraint)
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
