using System.Collections.Immutable;

namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Handles the formatting to and from disk for <see cref="ClassificationData"/>.
/// </summary>
public sealed class ClassificationDataFormatter : ITrafficFormatter<ClassificationData>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ClassificationDataFormatter"/> class.
	/// </summary>
	/// <param name="stationDescription">Station description used to determine the <see cref="ClassificationGroupings"/>.</param>
	public ClassificationDataFormatter(in StationDescription stationDescription)
		: this(stationDescription.ClassificationGroupings)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ClassificationDataFormatter"/> class.
	/// </summary>
	/// <param name="classificationGroupings">
	/// Classification groupings to use for <see cref="ClassificationData.ClassBins"/> count. The recommended default 
	/// value is 13, which indicates that the standard FHWA 13 vehicle category classification system is being used.
	/// </param>
	public ClassificationDataFormatter(ClassificationGroupings? classificationGroupings)
	{
		_classificationGroupings = classificationGroupings ?? ClassificationGroupings.Thirteen;
	}

	/// <inheritdoc />
	public static string FileExtension => "cla";

	/// <inheritdoc />
	public ClassificationData FromLine(in ReadOnlySpan<char> line)
	{
		if (line.Length < 38)
			throw new ArgumentException("Speed data lines must be at least 105 characters long", nameof(line));

		var bins = (int)_classificationGroupings;
		var requiredLength = 28 + (bins * 5);

		if (line.Length < requiredLength)
			throw new ArgumentException($"Speed data lines with {_classificationGroupings} classifications must be at least {requiredLength} characters long", nameof(line));

		if (Strategies.ReadChar(line, 1) != 'C')
			throw new ArgumentOutOfRangeException(nameof(line), Strategies.ReadChar(line, 1), "Can only read classification data records");

		var list = new int?[13];
		for (int i = 0; i < bins; i++)
			list[i] = Strategies.ReadInt32(line, 29 + (i * 5), 5);

		var result = new ClassificationData()
		{
			StateCode = (Fips.State?)Strategies.ReadInt32(line, 2, 2) ?? throw new NullReferenceException(),
			StationID = Strategies.ReadLeftZeroFilledString(line, 4, 6) ?? throw new NullReferenceException(),
			DirectionOfTravelCode = (DirectionOfTravel?)Strategies.ReadChar(line, 10) ?? throw new NullReferenceException(),
			LaneOfTravelCode = Strategies.ReadNumber(line, 11) ?? throw new NullReferenceException(nameof(ClassificationData.LaneOfTravelCode) + "is not optional"),
			DateOfData = Strategies.ReadDateTime(line, 12),
			TimeInterval = (TimeInterval?)Strategies.ReadChar(line, 22),
			TotalIntervalVolume = Strategies.ReadInt32(line, 23, 5),
			Restrictions = (Restrictions?)Strategies.ReadNumber(line, 28) ?? throw new NullReferenceException(nameof(ClassificationData.Restrictions) + "is not optional"),
			ClassBins = list.ToImmutableArray(),
		};

		return result;
	}

	/// <inheritdoc />
	public ReadOnlySpan<char> ToLine(ClassificationData item)
	{
		var result = new char[93];

		Strategies.WriteChar(result, 1, 'C');
		Strategies.WriteInt32(result, 2, 2, (int)item.StateCode);
		Strategies.WriteLeftZeroFilledString(result, 4, 6, item.StationID);
		Strategies.WriteChar(result, 10, (char)item.DirectionOfTravelCode);
		Strategies.WriteNumber(result, 11, item.LaneOfTravelCode);
		Strategies.WriteDateTime(result, 12, item.DateOfData);
		Strategies.WriteChar(result, 22, (char?)item.TimeInterval);
		Strategies.WriteInt32(result, 23, 5, item.TotalIntervalVolume);
		Strategies.WriteNumber(result, 28, (int)item.Restrictions);
		for (int i = 0; i < item.ClassBins.Count; i++)
			Strategies.WriteInt32(result, 28 + (i * 5), 5, item.ClassBins[i]);

		return result;
	}

	private readonly ClassificationGroupings _classificationGroupings;
}
