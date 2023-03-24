namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Handles the formatting to and from disk for <see cref="HourlyTrafficVolume"/>.
/// </summary>
public sealed class HourlyTrafficVolumeFormatter : ITrafficFormatter<HourlyTrafficVolume>
{
	/// <inheritdoc />
	public static string FileExtension => "vol";

	/// <inheritdoc />
	public HourlyTrafficVolume FromLine(in ReadOnlySpan<char> line)
	{
		if (line.Length != 143)
			throw new ArgumentException("Hourly traffic lines must be exactly 143 characters long", nameof(line));
		if (Strategies.ReadChar(line, 1) != '3')
			throw new ArgumentOutOfRangeException(nameof(line), Strategies.ReadChar(line, 1), "Can only read hourly traffic volume records");

		var result = new HourlyTrafficVolume()
		{
			StateCode = (Fips.State?)Strategies.ReadInt32(line, 2, 2) ?? throw new NullReferenceException(nameof(HourlyTrafficVolume.StateCode) + "is not optional"),
			FunctionalPurpose = (FunctionalPurpose?)Strategies.ReadChar(line, 4) ?? throw new NullReferenceException(nameof(HourlyTrafficVolume.FunctionalPurpose) + "is not optional"),
			FunctionalTypeCode = (FunctionalType?)Strategies.ReadChar(line, 5) ?? throw new NullReferenceException(nameof(HourlyTrafficVolume.FunctionalTypeCode) + "is not optional"),
			StationID = Strategies.ReadLeftZeroFilledString(line, 6, 6) ?? throw new NullReferenceException(nameof(HourlyTrafficVolume.StationID) + "is not optional"),
			DirectionOfTravelCode = (DirectionOfTravel?)Strategies.ReadChar(line, 12) ?? throw new NullReferenceException(nameof(HourlyTrafficVolume.DirectionOfTravelCode) + "is not optional"),
			LaneOfTravelCode = Strategies.ReadNumber(line, 13) ?? throw new NullReferenceException(nameof(HourlyTrafficVolume.LaneOfTravelCode) + "is not optional"),
			DateOfData = Strategies.ReadDate(line, 14),
			VolumeCounted = new int?[24],
			Restrictions = (Restrictions?)Strategies.ReadChar(line, 143) ?? throw new NullReferenceException(nameof(HourlyTrafficVolume.Restrictions) + "is not optional"),
		};

		for (int i = 0; i < 24; i++)
			result.VolumeCounted[i] = Strategies.ReadInt32(line, 23 + (i * 5), 5);

		return result;
	}

	/// <inheritdoc />
	public ReadOnlySpan<char> ToLine(HourlyTrafficVolume item)
	{
		var result = new char[143];

		Strategies.WriteChar(result, 1, '3');
		Strategies.WriteInt32(result, 2, 2, (int)item.StateCode);
		Strategies.WriteChar(result, 4, (char)item.FunctionalPurpose);
		Strategies.WriteChar(result, 5, (char)item.FunctionalTypeCode);
		Strategies.WriteLeftZeroFilledString(result, 6, 6, item.StationID);
		Strategies.WriteChar(result, 12, (char)item.DirectionOfTravelCode);
		Strategies.WriteNumber(result, 13, item.LaneOfTravelCode);
		Strategies.WriteDate(result, 14, item.DateOfData);
		Strategies.WriteNumber(result, 22, (int)item.DayOfWeek + 1);
		for (int i = 0; i < 24; i++)
			Strategies.WriteInt32(result, 23 + (i * 5), 5, item.VolumeCounted[i]);
		Strategies.WriteChar(result, 143, (char)item.Restrictions);

		return result;
	}
}
