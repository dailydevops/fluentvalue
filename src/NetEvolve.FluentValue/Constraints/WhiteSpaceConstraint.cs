namespace NetEvolve.FluentValue.Constraints;

using System.Text;

internal sealed class WhiteSpaceConstraint : ConstraintBase
{
    public override bool IsSatisfiedBy(object? value) =>
        value switch
        {
            char charValue => char.IsWhiteSpace(charValue),
            string stringValue => IsWhiteSpace(stringValue),
            _ => false,
        };

    public override void SetDescription(StringBuilder builder) =>
        builder.Append(" is <whitespace>");

    private static bool IsWhiteSpace(ReadOnlySpan<char> value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (!char.IsWhiteSpace(value[i]))
            {
                return false;
            }
        }
        return true;
    }
}
