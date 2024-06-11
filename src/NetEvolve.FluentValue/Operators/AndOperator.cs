namespace NetEvolve.FluentValue.Operators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetEvolve.Arguments;
using NetEvolve.FluentValue;

internal sealed class AndOperator : OperatorBase
{
    private readonly IConstraint _left;

    private readonly List<IConstraint> _constraints = [];

    public AndOperator(IConstraint left)
    {
        Argument.ThrowIfNull(left);

        _left = left;
    }

    public override bool IsSatisfiedBy(object? value)
    {
        if (_constraints.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return _left.IsSatisfiedBy(value) && _constraints.TrueForAll(x => x.IsSatisfiedBy(value));
    }

    public override IConstraint SetConstraint(IConstraint constraint)
    {
        Argument.ThrowIfNull(constraint);

        var lastConstraint = _constraints.LastOrDefault();
        if (lastConstraint is NotOperator notOperator)
        {
            _ = notOperator.SetConstraint(constraint);
            return this;
        }

        _constraints.Add(constraint);

        return this;
    }

    public override void SetDescription(StringBuilder builder)
    {
        _left.SetDescription(builder);

        foreach (var constraint in _constraints)
        {
            _ = builder.Append(" and");
            constraint.SetDescription(builder);
        }
    }
}
