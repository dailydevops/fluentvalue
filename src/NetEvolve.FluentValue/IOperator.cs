namespace NetEvolve.FluentValue;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using NetEvolve.FluentValue.Constraints;
using NetEvolve.FluentValue.Operators;

/// <summary>
/// Public interface for operators, which can be used to build complex expressions.
/// </summary>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "As designed.")]
public interface IOperator : IConstraint
{
    /// <summary>
    /// Internal method to set the constraint.
    /// </summary>
    /// <param name="constraint">
    /// The constraint to set.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint SetConstraint(IConstraint constraint);

    /// <summary>
    /// Appends a constraint that the value contains the specified character.
    /// </summary>
    /// <param name="compareValue">
    /// The character to compare.
    /// </param>
    /// <param name="comparison">
    /// The comparison type.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint Contains(char compareValue, StringComparison comparison = default) =>
        SetConstraint(new ContainsConstraint(compareValue, comparison));

    /// <summary>
    /// Appends a constraint that the value contains a <see langword="object"/>.
    /// </summary>
    /// <param name="compareValue">
    /// The object to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint Contains(object? compareValue) => SetConstraint(new ContainsConstraint(compareValue));

    /// <summary>
    /// Appends a constraint that the value contains the specified string.
    /// </summary>
    /// <param name="compareValue">
    /// The string to compare.
    /// </param>
    /// <param name="comparison">
    /// The comparison type.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint Contains(string compareValue, StringComparison comparison = default) =>
        SetConstraint(new ContainsConstraint(compareValue, comparison));

    /// <summary>
    /// Appends a constraint that the value is the default value.
    /// </summary>
    IConstraint Default => SetConstraint(new DefaultConstraint());

    /// <summary>
    /// Appends a constraint that the value is empty.
    /// </summary>
    IConstraint Empty => SetConstraint(new EmptyConstraint());

    /// <summary>
    /// Appends a constraint that the value ends with the specified character.
    /// </summary>
    /// <param name="compareValue">
    /// The character to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint EndsWith(char compareValue) => SetConstraint(new EndsWithConstraint(compareValue));

    /// <summary>
    /// Appends a constraint that the value ends with the specified string.
    /// </summary>
    /// <param name="compareValue">
    /// The string to compare.
    /// </param>
    /// <param name="comparison">
    /// The comparison type.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint EndsWith(string compareValue, StringComparison comparison = default) =>
        SetConstraint(new EndsWithConstraint(compareValue, comparison));

    /// <summary>
    /// Appends a constraint that the value is equal to the specified <see langword="object"/>.
    /// </summary>
    /// <param name="compareValue">
    /// The <see langword="object"/> to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint EqualTo(object? compareValue) => SetConstraint(new EqualToConstraint(compareValue));

    /// <summary>
    /// Appends a constraint that the value is equal to the specified string.
    /// </summary>
    /// <param name="compareValue">
    /// The string to compare.
    /// </param>
    /// <param name="comparison">
    /// The comparison type.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint EqualTo(string compareValue, StringComparison comparison = default) =>
        SetConstraint(new EqualToConstraint(compareValue, comparison));

    /// <summary>
    /// Appends a constraint that the value matches the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">
    /// The regular expression pattern to match.
    /// </param>
    /// <param name="options">
    /// The regular expression options.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint Matches([StringSyntax(StringSyntaxAttribute.Regex)] string pattern, RegexOptions? options = null) =>
        SetConstraint(new MatchesConstraint(pattern, options));

    /// <summary>
    /// Appends a negation operator.
    /// </summary>
    [SuppressMessage(
        "Blocker Code Smell",
        "S3060:\"is\" should not be used with \"this\"",
        Justification = "As designed."
    )]
    IOperator Not
    {
        get
        {
            if (this is NotOperator)
            {
                throw new InvalidOperationException("Cannot nest multiple NOT operators.");
            }

            return (IOperator)SetConstraint(new NotOperator());
        }
    }

    /// <summary>
    /// Appends a constraint that the value is <see langword="not"/> the <see langword="default"/> value.
    /// </summary>
    IConstraint NotDefault => SetConstraint(new NotDefaultConstraint());

    /// <summary>
    /// Appends a constraint that the value is <see langword="not"/> empty.
    /// </summary>
    IConstraint NotEmpty => SetConstraint(new NotEmptyConstraint());

    /// <summary>
    /// Appends a constraint that the value is <see langword="not"/> <see langword="null"/>.
    /// </summary>
    IConstraint NotNull => SetConstraint(new NotNullConstraint());

    /// <summary>
    /// Appends a constraint that the value is <see langword="null"/>.
    /// </summary>
    IConstraint Null => SetConstraint(new NullConstraint());

    /// <summary>
    /// Appends a constraint that the value is enclosed in parenthesis.
    /// </summary>
    /// <param name="constraint">
    /// The constraint to enclose.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint Parenthesis(IConstraint constraint) => SetConstraint(new ParenthesisConstraint(constraint));

    /// <summary>
    /// Appends a constraint that the value starts with the specified character.
    /// </summary>
    /// <param name="compareValue">
    /// The character to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint StartsWith(char compareValue) => SetConstraint(new StartsWithConstraint(compareValue));

    /// <summary>
    /// Appends a constraint that the value starts with the specified string.
    /// </summary>
    /// <param name="compareValue">
    /// The string to compare.
    /// </param>
    /// <param name="comparison">
    /// The comparison type.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    IConstraint StartsWith(string compareValue, StringComparison comparison = default) =>
        SetConstraint(new StartsWithConstraint(compareValue, comparison));

    /// <summary>
    /// Appends a constraint that the value is a whitespace character.
    /// </summary>
    IConstraint WhiteSpace => SetConstraint(new WhiteSpaceConstraint());
}
