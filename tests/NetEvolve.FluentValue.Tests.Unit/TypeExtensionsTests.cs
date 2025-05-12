namespace NetEvolve.FluentValue.Tests.Unit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TypeExtensionsTests
{
    [Test]
    [MethodDataSource(nameof(GetDefaultData))]
    public async Task IsNullableType_ShouldReturnTrue_WhenTypeIsNullable(object? expected, Type type)
    {
        // Arrange & Act
        var result = TypeExtensions.GetDefault(type);
        // Assert
        _ = await Assert.That(result).IsEqualTo(expected);
    }

    public static Func<(object?, Type)>[] GetDefaultData() =>
        [
            () => (null, typeof(int?)),
            () => (default(int), typeof(int)),
            () => (null, typeof(string)),
            () => (ConsoleKey.None, typeof(ConsoleKey)),
            () => (null, typeof(ConsoleKey?)),
        ];
}
