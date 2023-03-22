namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Various strategies for formatting fields and data during reading and writing traffic data.
/// </summary>
public static class Strategies
{
	/// <summary>
	/// Parses a portion of a line.
	/// </summary>
	/// <param name="line">Line to be parsed.</param>
	/// <param name="offset">1-based character index into line for beginning result.</param>
	/// <param name="length">Number of characters result occupies.</param>
	/// <returns>Integer at location or <see langword="null"/> if blank.</returns>
	public static int? ReadInt32(in ReadOnlySpan<char> line, int offset, int length)
		=> line[offset - 1] == ' '
			? null : int.Parse(line.Slice(offset - 1, length));

	/// <summary>
	/// Inserts value into a portion of a line. Zero-fills extra characters.
	/// </summary>
	/// <param name="result">Line to insert the value into.</param>
	/// <param name="offset">1-based character index into line for beginning of <paramref name="value"/>.</param>
	/// <param name="length">Number of characters <paramref name="value"/> occupies.</param>
	/// <param name="value">Value to insert. If <see langword="null"/>, inserts all blanks.</param>
	public static void WriteInt32(char[] result, int offset, int length, int? value)
	{
		if (value.HasValue)
			value.Value.ToString("D" + length).CopyTo(0, result, offset - 1, length);
		else
			Array.Fill(result, ' ', offset - 1, length);

	}

	/// <summary>
	/// Parses a portion of a line.
	/// </summary>
	/// <param name="line">Line to be parsed.</param>
	/// <param name="offset">1-based character index into line for beginning result.</param>
	/// <param name="length">Number of characters result occupies.</param>
	/// <returns>String at location or <see langword="null"/> if blank.</returns>
	public static string? ReadString(in ReadOnlySpan<char> line, int offset, int length)
	{
		var result = line.Slice(offset - 1, length).Trim(' ').ToString();
		return result == string.Empty ? null : result;
	}

	/// <summary>
	/// Inserts value into a portion of a line. Blank-fills extra characters.
	/// </summary>
	/// <param name="result">Line to insert the value into.</param>
	/// <param name="offset">1-based character index into line for beginning of <paramref name="value"/>.</param>
	/// <param name="length">Number of characters <paramref name="value"/> occupies.</param>
	/// <param name="value">Value to insert. If <see langword="null"/>, inserts all blanks.</param>
	public static void WriteString(char[] result, int offset, int length, string? value)
	{
		Array.Fill(result, ' ', offset - 1, length);
		value?.CopyTo(0, result, offset - 1, value.Length);
	}


	/// <summary>
	/// Parses a portion of a line.
	/// </summary>
	/// <param name="line">Line to be parsed.</param>
	/// <param name="offset">1-based character index into line for beginning result.</param>
	/// <param name="length">Number of characters result occupies.</param>
	/// <returns>String at location or <see langword="null"/> if blank.</returns>
	public static string? ReadLeftZeroFilledString(in ReadOnlySpan<char> line, int offset, int length)
		=> (line[offset - 1] == ' ')
			? null : line.Slice(offset - 1, length).TrimStart('0').ToString();

	/// <summary>
	/// Inserts value into a portion of a line. Right-justifies and fills extra characters with 0's.
	/// </summary>
	/// <param name="result">Line to insert the value into.</param>
	/// <param name="offset">1-based character index into line for beginning of <paramref name="value"/>.</param>
	/// <param name="length">Number of characters <paramref name="value"/> occupies.</param>
	/// <param name="value">Value to insert. If <see langword="null"/>, inserts all blanks.</param>
	public static void WriteLeftZeroFilledString(char[] result, int offset, int length, string? value)
	{
		if (value == null)
			Array.Fill(result, ' ', offset - 1, length);
		else
		{
			var zeroCount = length - value.Length;
			Array.Fill(result, '0', offset - 1, zeroCount);
			value.CopyTo(0, result, offset - 1 + zeroCount, value.Length);
		}
	}

	/// <summary>
	/// Parses a single character of a line.
	/// </summary>
	/// <param name="line">Line to be parsed.</param>
	/// <param name="offset">1-based character index into line for beginning result.</param>
	/// <returns>Character at location or <see langword="null"/> if blank.</returns>
	public static char? ReadChar(in ReadOnlySpan<char> line, int offset)
	{
		var result = line[offset - 1];
		return result == ' ' ? null : result;
	}

	/// <summary>
	/// Inserts value into a single character of a line.
	/// </summary>
	/// <param name="result">Line to insert the value into.</param>
	/// <param name="offset">1-based character index into line for beginning of <paramref name="value"/>.</param>
	/// <param name="value">Value to insert. If <see langword="null"/>, inserts a blank.</param>
	public static void WriteChar(char[] result, int offset, char? value)
		=> result[offset - 1] = value ?? ' ';

	/// <summary>
	/// Parses a single-digit number from a single character of a line.
	/// </summary>
	/// <param name="line">Line to be parsed.</param>
	/// <param name="offset">1-based character index into line for number.</param>
	/// <returns>Number at location.</returns>
	public static int ReadNumber(in ReadOnlySpan<char> line, int offset)
		=> line[offset - 1] - '0';

	/// <summary>
	/// Inserts a single-digit number into a single character of a line.
	/// </summary>
	/// <param name="result">Line to insert the value into.</param>
	/// <param name="offset">1-based character index into line for beginning of <paramref name="value"/>.</param>
	/// <param name="value">Value to insert.</param>
	public static void WriteNumber(char[] result, int offset, int value)
		=> result[offset - 1] = (char)(value + '0');

	/// <summary>
	/// Parses a portion of a line.
	/// </summary>
	/// <param name="line">Line to be parsed.</param>
	/// <param name="offset">1-based character index into line for beginning result.</param>
	/// <param name="length">Number of characters result occupies.</param>
	/// <param name="leftShift">How far left the decimal point is shifted, before writing to disk.</param>
	/// <returns>Double at location.</returns>
	public static double ReadDecimal(in ReadOnlySpan<char> line, int offset, int length, int leftShift)
		=> double.Parse(line.Slice(offset - 1, length)) / Math.Pow(10, leftShift);

	/// <summary>
	/// Inserts value into a portion of a line. Blank-fills extra characters.
	/// </summary>
	/// <param name="result">Line to insert the value into.</param>
	/// <param name="offset">1-based character index into line for beginning of <paramref name="value"/>.</param>
	/// <param name="length">Number of characters <paramref name="value"/> occupies.</param>
	/// <param name="value">Value to insert. If <see langword="null"/>, inserts all blanks.</param>
	/// <param name="leftShift">How far left the decimal point is shifted, before writing to disk.</param>
	public static void WriteDecimal(char[] result, int offset, int length, double value, int leftShift)
		=> WriteLeftZeroFilledString(result, offset, length,
			(value * Math.Pow(10, leftShift)).ToString("#"));

	/// <summary>
	/// Parses a portion of a line.
	/// </summary>
	/// <param name="line">Line to be parsed.</param>
	/// <param name="offset">1-based character index into line for beginning result.</param>
	/// <returns>Date at location.</returns>
	public static DateOnly ReadDate(in ReadOnlySpan<char> line, int offset)
		=> DateOnly.ParseExact(line.Slice(offset - 1, 8), "yyyyMMdd");

	/// <summary>
	/// Inserts value into a portion of a line. Blank-fills extra characters.
	/// </summary>
	/// <param name="result">Line to insert the value into.</param>
	/// <param name="offset">1-based character index into line for beginning of <paramref name="value"/>.</param>
	/// <param name="value">Value to insert. If <see langword="null"/>, inserts all blanks.</param>
	public static void WriteDate(char[] result, int offset, DateOnly value)
		=> value.ToString("yyyyMMdd").CopyTo(0, result, offset - 1, 8);
}
