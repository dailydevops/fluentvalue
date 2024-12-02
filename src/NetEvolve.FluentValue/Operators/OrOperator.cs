namespace NetEvolve.FluentValue.Operators;

using System;
using System.Text;
using NetEvolve.Arguments;
using NetEvolve.FluentValue;

internal sealed class OrOperator : OperatorBase
{
    private readonly IConstraint _left;
    private IConstraint? _right;

    internal OrOperator(IConstraint left)
    {
        Argument.ThrowIfNull(left);

        _left = left;
    }

    public override bool IsSatisfiedBy(object? value)
    {
        if (_right is null)
        {
            throw new InvalidOperationException();
        }

        return _left.IsSatisfiedBy(value) || _right.IsSatisfiedBy(value);
    }

    public override IConstraint SetConstraint(IConstraint constraint)
    {
        Argument.ThrowIfNull(constraint);

        if (_right is NotOperator notOperator)
        {
            _ = notOperator.SetConstraint(constraint);
            return this;
        }

        _right = constraint;

        return this;
    }

    public override void SetDescription(StringBuilder builder)
    {
        _left.SetDescription(builder);
        if (_right is null)
        {
            return;
        }
        _ = builder.Append(" or");
        _right.SetDescription(builder);
    }
}
