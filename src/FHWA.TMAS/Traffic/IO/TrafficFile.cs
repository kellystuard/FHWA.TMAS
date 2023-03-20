namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Holds the fields necessary to generate the filename used to write traffic data.
/// </summary>
/// <param name="StateCode">FIPS State Codes</param>
/// <param name="StationID">Station Identification</param>
/// <param name="DataMonth">Month of Data</param>
/// <param name="DataYear">Year of Data</param>
public record TrafficFile(
	Fips.State StateCode,
	string StationID,
	int DataMonth,
	int DataYear
)
{
	/// <summary>
	/// Generates the correct file name for writing the associated traffic data.
	/// </summary>
	/// <remarks>
	/// <paramref name="extension"/> generally passed in by <see cref="ITrafficFormatter{T}.FileExtension"/>.
	/// </remarks>
	/// <param name="extension">File extension</param>
	/// <returns></returns>
	/// <exception cref="ArgumentException"></exception>
	public string GenerateFileName(string extension)
	{
		if (extension.Length != 3)
			throw new ArgumentException("Must be 3 characters in length", nameof(extension));
		if (StationID.Length > 6)
			throw new ArgumentException($"Length cannot be longer than 6 characters", nameof(StationID));
		if (DataMonth is < 1 or > 12)
			throw new ArgumentException($"Must be between 1 and 12", nameof(DataMonth));
		if (DataYear is < 0 or > 9999)
			throw new ArgumentException($"Must be between 0 and 9999", nameof(DataYear));

		// ssabcxyzmmyyyy.ext
		return string.Concat(
			StateCode.ToString("d").PadLeft(2, '0'),
			StationID.PadLeft(6, '0'),
			DataMonth.ToString().PadLeft(2, '0'),
			DataYear.ToString().PadLeft(4, '0'),
			'.',
			extension
		);
	}
}
