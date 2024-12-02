namespace NetEvolve.FluentValue.Tests.Unit;

using System;
using System.Collections.Generic;
using System.Linq;
using NetEvolve.FluentValue;
using Xunit;
using static System.StringComparison;

public class ConstraintTests
{
    [Fact]
    public void Value_NotNot_NoFurtherConstraint_ThrowsInvalidOperationException() =>
        _ = Assert.Throws<InvalidOperationException>(() => Value.Not.Not);

    [Fact]
    public void Value_MultipleOperators_ThrowsInvalidOperationException() =>
        Assert.Multiple(
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.And.And),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.And.Or),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.And.Xor),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.Or.And),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.Or.Or),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.Or.Xor),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.Xor.And),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.Xor.Or),
            () => _ = Assert.Throws<InvalidOperationException>(() => Value.Null.Xor.Xor)
        );

    [Fact]
    public void Value_Contains_Object_ThrowsNotSupportedException() =>
        _ = Assert.Throws<NotSupportedException>(
            () => Value.Contains(new object()).IsSatisfiedBy(new object())
        );

    [Theory]
    [MemberData(nameof(InvalidConstraintData))]
    public void Value_InvalidConstraint_ThrowsInvalidOperationException(IConstraint constraint) =>
        _ = Assert.Throws<InvalidOperationException>(() => constraint.IsSatisfiedBy(true));

    public static TheoryData<IConstraint> InvalidConstraintData =>
        [
            Value.Not,
            Value.Null.And,
            Value.Null.Or,
            Value.Null.Xor,
            Value.Not.Null.And,
            Value.Not.Null.Or,
            Value.Not.Null.Xor,
        ];

    [Theory]
    [MemberData(nameof(ConstraintValueData))]
    public void Value_Theory_Expected(bool expected, IConstraint constraint, object? value)
    {
        Assert.NotNull(constraint);

        var result = constraint.IsSatisfiedBy(value);

        Assert.Equal(expected, result);
    }

    public static TheoryData<bool, IConstraint, object?> ConstraintValueData =>
        new TheoryData<bool, IConstraint, object?>
        {
            // .Contains
            { false, Value.Contains("Null"), null },
            { false, Value.Contains("Hallo", Ordinal), "Hello World!" },
            { true, Value.Contains("Hello"), "Hello World!" },
            { false, Value.Contains('Z', Ordinal), "Hello World!" },
            { true, Value.Contains('W'), "Hello World!" },
            {
                false,
                Value.Contains(2),
                new Dictionary<string, object> { { "Hello", "World!" } }
            },
            {
                true,
                Value.Contains("Hello"),
                new Dictionary<string, object> { { "Hello", "World!" } }
            },
            {
                false,
                Value.Contains(2),
                new List<string> { "Hello", "World!" }
            },
            {
                true,
                Value.Contains("World!"),
                new List<string> { "Hello", "World!" }
            },
            { false, Value.Contains(2), Enumerable.Range(10, 10) },
            { true, Value.Contains(11), Enumerable.Range(10, 10) },
            // .Default
            { false, Value.Default, 1 },
            { false, Value.Default, null },
            { false, Value.Default, (int?)3 },
            { true, Value.Default, 0 },
            // .Empty
            { false, Value.Empty, "Hello World!" },
            { true, Value.Empty, string.Empty },
            { false, Value.Empty, Guid.NewGuid() },
            { true, Value.Empty, Guid.Empty },
            {
                false,
                Value.Empty,
                new List<string> { "Hello", "World!" }
            },
            { true, Value.Empty, new List<string>() },
            { false, Value.Empty, Enumerable.Range(10, 10) },
            { true, Value.Empty, Enumerable.Empty<int>() },
            { false, Value.Empty, null },
            // .EndsWith
            { false, Value.EndsWith("Welt!"), "Hello World!" },
            { true, Value.EndsWith("World!"), "Hello World!" },
            { false, Value.EndsWith('?'), "Hello World!" },
            { true, Value.EndsWith('!'), "Hello World!" },
            { true, Value.EndsWith("world!", OrdinalIgnoreCase), "Hello World!" },
            { false, Value.EndsWith("123"), 123 },
            // .EqualTo
            { false, Value.EqualTo("World!"), "Hello World!" },
            { true, Value.EqualTo("Hello World!"), "Hello World!" },
            { false, Value.EqualTo('H'), "Hello World!" },
            { true, Value.EqualTo(123456), "123456" },
            { true, Value.EqualTo("hello world!", OrdinalIgnoreCase), "Hello World!" },
            { true, Value.EqualTo(123), 123 },
            { true, Value.EqualTo(null), null },
            // .Matches
            { false, Value.Matches(@"\d+"), null },
            { true, Value.Matches(@"\d+"), 123456 },
            { false, Value.Matches(@"\d+"), "Hello World!" },
            { true, Value.Matches(@"\w+\s\w+"), "Hello World!" },
            // .Null
            { false, Value.Null, 1 },
            { true, Value.Null, null },
            // .StartsWith
            { false, Value.StartsWith("World"), "Hello World!" },
            { false, Value.StartsWith('?'), "Hello World!" },
            { true, Value.StartsWith('H'), "Hello World!" },
            { true, Value.StartsWith("hello", OrdinalIgnoreCase), "Hello World!" },
            { false, Value.StartsWith("123"), 123 },
            // .WhiteSpace
            { false, Value.WhiteSpace, "Hello World!" },
            { false, Value.WhiteSpace, null },
            { true, Value.WhiteSpace, " " },
            { true, Value.WhiteSpace, "\t" },
            { true, Value.WhiteSpace, "\n" },
            { true, Value.WhiteSpace, '\t' },
            { true, Value.WhiteSpace, '\n' },
            { false, Value.WhiteSpace, 1 },
            // .And Operators
            {
                false,
                Value.Contains("Hello", OrdinalIgnoreCase).And.Contains("Welt!", Ordinal),
                "Hello World!"
            },
            {
                true,
                Value.Contains("Hello", OrdinalIgnoreCase).And.Contains("World!", Ordinal),
                "Hello World!"
            },
            // .Or Operators
            { false, Value.Null.Or.Empty, "Hello World!" },
            { true, Value.Null.Or.Empty, null },
            { true, Value.Null.Or.Empty, string.Empty },
            // .Xor Operators
            { false, Value.Null.Xor.Empty, "Hello World!" },
            { true, Value.Null.Xor.Empty, string.Empty },
            {
                false,
                Value.Contains("Hello", OrdinalIgnoreCase).Xor.Contains("World!", Ordinal),
                "Hello World!"
            },
            {
                true,
                Value.Contains("Hello", OrdinalIgnoreCase).Xor.Not.Contains("World!", Ordinal),
                "Hello World!"
            },
            // .Not Operators
            { false, Value.Not.EqualTo(2), "2" },
            { false, Value.Not.EqualTo("Hello World!"), "Hello World!" },
            { false, Value.Not.Null, null },
            { false, Value.Not.Empty, string.Empty },
            { true, Value.Not.Empty, "Hello World!" },
            { true, Value.Not.Contains("Hallo", Ordinal), "Hello World!" },
            { false, Value.Not.StartsWith("Hello"), "Hello World!" },
            { false, Value.Not.EndsWith("World!"), "Hello World!" },
            { false, Value.Not.WhiteSpace, " " },
            { false, Value.Not.Matches(@"\d+"), 123456 },
            { true, Value.Not.Default, 1 },
            { true, Value.Not.Contains('Z'), "Hello World!" },
            { true, Value.Not.Contains(123), "Hello World!" },
            { true, Value.Not.StartsWith('W'), "Hello World!" },
            { true, Value.Not.EndsWith('W'), "Hello World!" },
            // .Parenthesis
            { true, Value.Not.Parenthesis(Value.Null.Or.Empty), "Hello World!" },
            { false, Value.Not.Parenthesis(Value.Null.Or.Empty), null },
            { false, Value.Not.Parenthesis(Value.Null.Or.Empty), string.Empty },
            { false, Value.Parenthesis(Value.Null.Or.Empty), "Hello World!" },
            { true, Value.Parenthesis(Value.Null.Or.Empty), null },
            { true, Value.Parenthesis(Value.Null.Or.Empty), string.Empty },
            // .And.Not
            { true, Value.Not.Null.And.Not.Empty, "Hello World!" },
            { false, Value.Not.Null.And.Not.Empty, null },
            { false, Value.Not.Null.And.Not.Empty, string.Empty },
            // .Or.Not
            { false, Value.StartsWith('A').Or.Not.StartsWith('H'), "Hello World!" },
        };

    [Theory]
    [MemberData(nameof(ConstraintToStringData))]
    public void ToString_Theory_Expected(string expected, IConstraint constraint)
    {
        Assert.NotNull(expected);
        Assert.NotNull(constraint);

        var result = constraint.ToString();

        Assert.Equal(expected, result);
    }

    public static TheoryData<string, IConstraint> ConstraintToStringData =>
        new TheoryData<string, IConstraint>
        {
            // .Contains
            { "\"{Value} contains `Hello`.\"", Value.Contains("Hello") },
            { "\"{Value} contains `W`.\"", Value.Contains('W') },
            { "\"{Value} contains `2`.\"", Value.Contains(2) },
            // .Default
            { "\"{Value} is <default>.\"", Value.Default },
            // .Empty
            { "\"{Value} is <empty>.\"", Value.Empty },
            // .EndsWith
            { "\"{Value} ends with `World!`.\"", Value.EndsWith("World!") },
            { "\"{Value} ends with `!`.\"", Value.EndsWith('!') },
            // .EqualTo
            { "\"{Value} is equal to `Hello World!`.\"", Value.EqualTo("Hello World!") },
            { "\"{Value} is equal to `123456`.\"", Value.EqualTo(123456) },
            // .Matches
            { "\"{Value} matches `\\d+`.\"", Value.Matches(@"\d+") },
            { "\"{Value} matches `\\w+\\s\\w+`.\"", Value.Matches(@"\w+\s\w+") },
            // .Null
            { "\"{Value} is <null>.\"", Value.Null },
            // .StartsWith
            { "\"{Value} starts with `Hello`.\"", Value.StartsWith("Hello") },
            { "\"{Value} starts with `H`.\"", Value.StartsWith('H') },
            // .WhiteSpace
            { "\"{Value} is <whitespace>.\"", Value.WhiteSpace },
            // .And Operators
            {
                "\"{Value} contains `Hello` and contains `World!`.\"",
                Value.Contains("Hello").And.Contains("World!")
            },
            // .Or Operators
            { "\"{Value} is <null> or is <empty>.\"", Value.Null.Or.Empty },
            // .Not.Null
            { "\"{Value} not is <null>.\"", Value.Not.Null },
            // Extreme case
            {
                "\"{Value} not is <null> and (starts with `H` and ends with `!`).\"",
                Value.Not.Null.And.Parenthesis(Value.StartsWith('H').And.EndsWith('!'))
            },
        };
}
