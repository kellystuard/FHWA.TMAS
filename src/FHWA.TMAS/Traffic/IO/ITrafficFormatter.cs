namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Handles the formatting to and from disk, from concrete traffic data types.
/// </summary>
/// <typeparam name="T">Type of traffic data to format.</typeparam>
public interface ITrafficFormatter<T> where T : ITrafficData
{
	/// <summary>
	/// File extension that the implemented traffic data should use, when written to disk.
	/// </summary>
	static abstract string FileExtension { get; }

	/// <summary>
	/// Parses a single line from file into an instance of the concrete traffic data type.
	/// </summary>
	/// <param name="line">Single line from file.</param>
	/// <returns>Instance of the concrete traffic data type.</returns>
	T FromLine(ReadOnlySpan<char> line);

	/// <summary>
	/// Formats an instance of the concrete traffic data type to a single line to write to file.
	/// </summary>
	/// <param name="item">Instance of the concrete traffic data type.</param>
	/// <returns>Single line to file.</returns>
	ReadOnlySpan<char> ToLine(T item);
}
