namespace NetEvolve.FluentValue;

using System;
using System.Text.RegularExpressions;
using NetEvolve.FluentValue.Constraints;
using NetEvolve.FluentValue.Operators;

/// <summary>
/// Entry point for building validation constraints.
/// </summary>
public static class Value
{
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
    public static IConstraint Contains(char compareValue, StringComparison comparison = default) =>
        new ContainsConstraint(compareValue, comparison);

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
    public static IConstraint Contains(
        string compareValue,
        StringComparison comparison = default
    ) => new ContainsConstraint(compareValue, comparison);

    /// <summary>
    /// Appends a constraint that the value contains a <see langword="object"/>.
    /// </summary>
    /// <param name="compareValue">
    /// The object to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    public static IConstraint Contains(object? compareValue) =>
        new ContainsConstraint(compareValue);

    /// <summary>
    /// Appends a constraint that the value is the default value.
    /// </summary>
    public static IConstraint Default => new DefaultConstraint();

    /// <summary>
    /// Appends a constraint that the value is empty.
    /// </summary>
    public static IConstraint Empty => new EmptyConstraint();

    /// <summary>
    /// Appends a constraint that the value ends with the specified character.
    /// </summary>
    /// <param name="compareValue">
    /// The character to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    public static IConstraint EndsWith(char compareValue) => new EndsWithConstraint(compareValue);

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
    public static IConstraint EndsWith(
        string compareValue,
        StringComparison comparison = default
    ) => new EndsWithConstraint(compareValue, comparison);

    /// <summary>
    /// Appends a constraint that the value is equal to the specified <see langword="object"/>.
    /// </summary>
    /// <param name="compareValue">
    /// The <see langword="object"/> to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    public static IConstraint EqualTo(object? compareValue) => new EqualToConstraint(compareValue);

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
    public static IConstraint EqualTo(string compareValue, StringComparison comparison) =>
        new EqualToConstraint(compareValue, comparison);

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
    public static IConstraint Matches(
#if NET7_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.StringSyntax(
            System.Diagnostics.CodeAnalysis.StringSyntaxAttribute.Regex
        )]
#endif
        string pattern,
        RegexOptions? options = null
    ) => new MatchesConstraint(pattern, options);

    /// <summary>
    /// Appends a negation operator.
    /// </summary>
    public static IOperator Not => new NotOperator();

    /// <summary>
    /// Appends a constraint that the value is <see langword="null"/>.
    /// </summary>
    public static IConstraint Null => new NullConstraint();

    /// <summary>
    /// Appends a constraint that the value is enclosed in parenthesis.
    /// </summary>
    /// <param name="constraint">
    /// The constraint to enclose.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    public static IConstraint Parenthesis(IConstraint constraint) =>
        new ParenthesisConstraint(constraint);

    /// <summary>
    /// Appends a constraint that the value starts with the specified character.
    /// </summary>
    /// <param name="compareValue">
    /// The character to compare.
    /// </param>
    /// <returns>
    /// The current instance.
    /// </returns>
    public static IConstraint StartsWith(char compareValue) =>
        new StartsWithConstraint(compareValue);

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
    public static IConstraint StartsWith(
        string compareValue,
        StringComparison comparison = default
    ) => new StartsWithConstraint(compareValue, comparison);

    /// <summary>
    /// Appends a constraint that the value is a whitespace character.
    /// </summary>
    public static IConstraint WhiteSpace => new WhiteSpaceConstraint();
}
