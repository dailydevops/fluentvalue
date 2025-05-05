namespace NetEvolve.FluentValue;

using System.Diagnostics.CodeAnalysis;
using System.Text;
using NetEvolve.FluentValue.Operators;

/// <summary>
/// Public interface for constraints, which can be used to build complex expressions.
/// </summary>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "As designed.")]
public interface IConstraint
{
    /// <summary>
    /// Determines if the constraint is satisfied by the given value.
    /// </summary>
    /// <param name="value">
    /// The value to check the constraint against.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the constraint is satisfied by the given value; otherwise, <see langword="false"/>.
    /// </returns>
    bool IsSatisfiedBy(object? value);

    /// <summary>
    /// Internal method to build a string representation of the constraint.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="StringBuilder"/> to append the description to.
    /// </param>
    void SetDescription(StringBuilder builder);

    /// <summary>
    /// Combines the current constraint with the given constraint using a logical AND operation.
    /// </summary>
    [SuppressMessage(
        "Blocker Code Smell",
        "S3060:\"is\" should not be used with \"this\"",
        Justification = "As designed."
    )]
    IOperator And
    {
        get
        {
            if (this is IOperator op && op is not NotOperator)
            {
                throw new InvalidOperationException("Cannot chain multiple operators.");
            }

            return new AndOperator(this);
        }
    }

    /// <summary>
    /// Combines the current constraint with the given constraint using a logical OR operation.
    /// </summary>
    [SuppressMessage(
        "Blocker Code Smell",
        "S3060:\"is\" should not be used with \"this\"",
        Justification = "As designed."
    )]
    IOperator Or
    {
        get
        {
            if (this is IOperator op && op is not NotOperator)
            {
                throw new InvalidOperationException("Cannot chain multiple operators.");
            }

            return new OrOperator(this);
        }
    }

    /// <summary>
    /// Combines the current constraint with the given constraint using a logical XOR operation.
    /// </summary>
    [SuppressMessage(
        "Blocker Code Smell",
        "S3060:\"is\" should not be used with \"this\"",
        Justification = "As designed."
    )]
    IOperator Xor
    {
        get
        {
            if (this is IOperator op && op is not NotOperator)
            {
                throw new InvalidOperationException("Cannot chain multiple operators.");
            }

            return new XorOperator(this);
        }
    }
}
