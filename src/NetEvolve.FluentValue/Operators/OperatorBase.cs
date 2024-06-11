namespace NetEvolve.FluentValue.Operators;

using NetEvolve.FluentValue;
using NetEvolve.FluentValue.Constraints;

internal abstract class OperatorBase : ConstraintBase, IOperator
{
    public abstract IConstraint SetConstraint(IConstraint constraint);
}
