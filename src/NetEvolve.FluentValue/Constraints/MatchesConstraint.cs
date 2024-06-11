namespace NetEvolve.FluentValue.Constraints;

using System;
using System.Text;
using System.Text.RegularExpressions;

internal sealed class MatchesConstraint : ConstraintBase
{
    private readonly string _pattern;
    private readonly Regex _regex;

    public MatchesConstraint(string pattern, RegexOptions? options)
    {
        _pattern = pattern;
        _regex = new Regex(pattern, options ?? default);
    }

    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            string stringValue => _regex.IsMatch(stringValue),
            IConvertible convertible => _regex.IsMatch(convertible.ToString()!),
            _ => false
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" matches `").Append(_pattern).Append('`');
}
