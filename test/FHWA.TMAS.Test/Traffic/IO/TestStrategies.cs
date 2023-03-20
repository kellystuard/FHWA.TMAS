using Fhwa.Tmas.Traffic.IO;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestStrategies
{
	[Theory]
	[MemberData(nameof(DataWriteLeftZeroFilledString))]
	public void TestWriteLeftZeroFilledString(string expected, int offset, int length, string value)
	{
		var buffer = new char[10];
		Strategies.WriteLeftZeroFilledString(buffer, offset, length, value);
		var result = new string(buffer, 0, buffer.Length);

		Assert.Equal(expected, result);
	}

	public static IEnumerable<object[]> DataWriteLeftZeroFilledString()
	{
		yield return new object[] { "0000012345", 1, 10, "12345" };
		yield return new object[] { "1234512345", 1, 10, "1234512345" };
		yield return new object[] { "12345\0\0\0\0\0", 1, 5, "12345" };
		yield return new object[] { "\0\0\0\0\012345", 6, 5, "12345" };
		yield return new object[] { "\0\0\012345\0\0", 4, 5, "12345" };
	}

	[Theory]
	[MemberData(nameof(DataWriteDecimal))]
	public void TestWriteDecimal(string expected, int offset, int length, double value, int leftShift)
	{
		var buffer = new char[8];
		Strategies.WriteDecimal(buffer, offset, length, value, leftShift);
		var result = new string(buffer, 0, buffer.Length);

		Assert.Equal(expected, result);
	}

	public static IEnumerable<object[]> DataWriteDecimal()
	{
		yield return new object[] { "\0007856\0", 2, 6, 7.856, 3 };
		yield return new object[] { "39178751", 1, 8, 39.178751, 6 };
	}
}
