using Fhwa.Tmas.Traffic.IO;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestStrategies
{
	[Theory]
	[MemberData(nameof(DataLeftZeroFilledString))]
	public void TestWriteLeftZeroFilledString(string expected, int offset, int length, string value)
	{
		var buffer = new char[10];
		Strategies.WriteLeftZeroFilledString(buffer, offset, length, value);
		var result = new string(buffer, 0, buffer.Length);

		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(DataLeftZeroFilledString))]
	public void TestReadLeftZeroFilledString(string value, int offset, int length, string expected)
	{
		var result = Strategies.ReadLeftZeroFilledString(value, offset, length);

		Assert.Equal(expected, result);
	}

	public static IEnumerable<object[]> DataLeftZeroFilledString()
	{
		yield return new object[] { "0000012345", 1, 10, "12345" };
		yield return new object[] { "1234512345", 1, 10, "1234512345" };
		yield return new object[] { "12345\0\0\0\0\0", 1, 5, "12345" };
		yield return new object[] { "\0\0\0\0\012345", 6, 5, "12345" };
		yield return new object[] { "\0\0\012345\0\0", 4, 5, "12345" };
	}

	[Theory]
	[MemberData(nameof(DataDecimal))]
	public void TestWriteDecimal(string expected, int offset, int length, double value, int leftShift)
	{
		var buffer = new char[8];
		Strategies.WriteDecimal(buffer, offset, length, value, leftShift);
		var result = new string(buffer, 0, buffer.Length);

		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(DataDecimal))]
	public void TestReadDecimal(string value, int offset, int length, double expected, int leftShift)
	{
		var result = Strategies.ReadDecimal(value, offset, length, leftShift);

		Assert.Equal(expected, result);
	}

	public static IEnumerable<object[]> DataDecimal()
	{
		yield return new object[] { "\0007856\0", 2, 6, 7.856, 3 };
		yield return new object[] { "39178751", 1, 8, 39.178751, 6 };
	}

	[Theory]
	[MemberData(nameof(DataDate))]
	public void TestWriteDate(string expected, int offset, DateOnly value)
	{
		var buffer = new char[10];
		Strategies.WriteDate(buffer, offset, value);
		var result = new string(buffer, 0, buffer.Length);

		Assert.Equal(expected, result);
	}

	[Theory]
	[MemberData(nameof(DataDate))]
	public void TestReadDate(string value, int offset, DateOnly expected)
	{
		var result = Strategies.ReadDate(value, offset);

		Assert.Equal(expected, result);
	}

	public static IEnumerable<object[]> DataDate()
	{
		yield return new object[] { "\0\020210805", 3, new DateOnly(2021, 8, 5) };
		yield return new object[] { "\020191206\0", 2, new DateOnly(2019, 12, 6) };
		yield return new object[] { "20181102\0\0", 1, new DateOnly(2018, 11, 2) };
	}
}
