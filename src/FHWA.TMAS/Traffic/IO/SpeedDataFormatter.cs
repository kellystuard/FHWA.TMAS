using System.Collections.Immutable;

namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Handles the formatting to and from disk for <see cref="SpeedData"/>.
/// </summary>
public sealed class SpeedDataFormatter : ITrafficFormatter<SpeedData>
{
	/// <inheritdoc />
	public static string FileExtension => "spd";

	/// <inheritdoc />
	public SpeedData FromLine(in ReadOnlySpan<char> line)
	{
		if (line.Length < 105)
			throw new ArgumentException("Speed data lines must be at least 105 characters long", nameof(line));

		var binCount = Strategies.ReadInt32(line, 24, 2);
		var requiredLength = 30 + (binCount * 5);

		if (line.Length < requiredLength)
			throw new ArgumentException($"Speed data lines with {binCount} bins must be at least {requiredLength} characters long", nameof(line));

		if (Strategies.ReadChar(line, 1) != 'T')
			throw new ArgumentOutOfRangeException(nameof(line), Strategies.ReadChar(line, 1), "Can only read speed data records");

		var list = new int?[25];
		for (int i = 0; i < list.Length; i++)
			list[i] = Strategies.ReadInt32(line, 31 + (i * 5), 5);

		var result = new SpeedData()
		{
			StateCode = (Fips.State?)Strategies.ReadInt32(line, 2, 2) ?? throw new NullReferenceException(),
			StationID = Strategies.ReadLeftZeroFilledString(line, 4, 6) ?? throw new NullReferenceException(),
			DirectionOfTravelCode = (DirectionOfTravel?)Strategies.ReadChar(line, 10) ?? throw new NullReferenceException(),
			LaneOfTravelCode = Strategies.ReadNumber(line, 11) ?? throw new NullReferenceException(nameof(SpeedData.LaneOfTravelCode) + "is not optional"),
			DateOfData = Strategies.ReadDateTime(line, 12),
			TimeInterval = (TimeInterval?)Strategies.ReadChar(line, 22),
			DefinitionOfFirstSpeedBin = Strategies.ReadNumber(line, 23),
			NumberOfSpeedBins = Strategies.ReadInt32(line, 24, 2),
			TotalIntervalVolume = Strategies.ReadInt32(line, 26, 5),
			SpeedBins = list.ToImmutableArray(),
		};

		return result;
	}

	/// <inheritdoc />
	public ReadOnlySpan<char> ToLine(SpeedData item)
	{
		var result = new char[155];

		Strategies.WriteChar(result, 1, 'T');
		Strategies.WriteInt32(result, 2, 2, (int)item.StateCode);
		Strategies.WriteLeftZeroFilledString(result, 4, 6, item.StationID);
		Strategies.WriteChar(result, 10, (char)item.DirectionOfTravelCode);
		Strategies.WriteNumber(result, 11, item.LaneOfTravelCode);
		Strategies.WriteDateTime(result, 12, item.DateOfData);
		Strategies.WriteChar(result, 22, (char?)item.TimeInterval);
		Strategies.WriteNumber(result, 23, item.DefinitionOfFirstSpeedBin);
		Strategies.WriteInt32(result, 24, 2, item.NumberOfSpeedBins);
		Strategies.WriteInt32(result, 26, 5, item.TotalIntervalVolume);
		for (int i = 0; i < 25; i++)
			Strategies.WriteInt32(result, 31 + (i * 5), 5, item.SpeedBins[i]);

		return result;
	}
}
