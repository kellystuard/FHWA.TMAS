using System.Collections.Immutable;

namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Handles the formatting to and from disk for <see cref="WeightData"/>.
/// </summary>
public sealed class WeightDataFormatter : ITrafficFormatter<WeightData>
{
	/// <inheritdoc />
	public static string FileExtension => "wgt";

	/// <inheritdoc />
	public WeightData FromLine(in ReadOnlySpan<char> line)
	{
		if (line.Length < 147)
			throw new ArgumentException("Weight data lines must be at least 105 characters long", nameof(line));

		var axleCount = Strategies.ReadInt32(line, 33, 2);
		if (axleCount < 1)
			throw new ArgumentException("Weight data lines must contain at least 1 axle", nameof(line));
		var requiredLength = 39 + ((axleCount - 1) * 10);

		if (line.Length < requiredLength)
			throw new ArgumentException($"Weight data lines with {axleCount} axles must be at least {requiredLength} characters long", nameof(line));

		if (Strategies.ReadChar(line, 1) != 'W')
			throw new ArgumentOutOfRangeException(nameof(line), Strategies.ReadChar(line, 1), "Can only read weight data records");

		var axleList = new int?[13];
		for (int i = 0; i < axleCount; i++)
			axleList[i] = Strategies.ReadInt32(line, 35 + (i * 5 * 2), 5);
		var spacingList = new int?[12];
		for (int i = 0; i < axleCount - 1; i++)
			spacingList[i] = Strategies.ReadInt32(line, 40 + (i * 5 * 2), 5);

		var result = new WeightData()
		{
			StateCode = (Fips.State?)Strategies.ReadInt32(line, 2, 2) ?? throw new NullReferenceException(nameof(WeightData.StateCode) + "is not optional"),
			StationID = Strategies.ReadLeftZeroFilledString(line, 4, 6) ?? throw new NullReferenceException(nameof(WeightData.StationID) + "is not optional"),
			DirectionOfTravelCode = (DirectionOfTravel?)Strategies.ReadChar(line, 10) ?? throw new NullReferenceException(nameof(WeightData.DirectionOfTravelCode) + "is not optional"),
			LaneOfTravelCode = Strategies.ReadNumber(line, 11) ?? throw new NullReferenceException(nameof(WeightData.LaneOfTravelCode) + "is not optional"),
			DateOfData = Strategies.ReadDateTime(line, 12),
			VehicleClass = Strategies.ReadString(line, 22, 2) ?? throw new NullReferenceException(nameof(WeightData.VehicleClass) + "is not optional"),
			Open = Strategies.ReadString(line, 24, 3),
			TotalWeightOfVehicle = Strategies.ReadInt32(line, 27, 6) ?? throw new NullReferenceException(nameof(WeightData.TotalWeightOfVehicle) + "is not optional"),
			NumberOfAxles = Strategies.ReadInt32(line, 33, 2) ?? throw new NullReferenceException(nameof(WeightData.NumberOfAxles) + "is not optional"),
			AxleWeights = axleList.ToImmutableArray(),
			AxleSpacing = spacingList.ToImmutableArray(),
		};

		return result;
	}

	/// <inheritdoc />
	public ReadOnlySpan<char> ToLine(WeightData item)
	{
		var result = new char[155];

		Strategies.WriteChar(result, 1, 'W');
		Strategies.WriteInt32(result, 2, 2, (int)item.StateCode);
		Strategies.WriteLeftZeroFilledString(result, 4, 6, item.StationID);
		Strategies.WriteChar(result, 10, (char)item.DirectionOfTravelCode);
		Strategies.WriteNumber(result, 11, item.LaneOfTravelCode);
		Strategies.WriteDateTime(result, 12, item.DateOfData);
		Strategies.WriteString(result, 22, 2, item.VehicleClass);
		Strategies.WriteString(result, 24, 3, item.Open);
		Strategies.WriteInt32(result, 27, 6, item.TotalWeightOfVehicle);
		Strategies.WriteInt32(result, 33, 2, item.NumberOfAxles);
		for (int i = 0; i < item.AxleWeights.Count; i++)
			Strategies.WriteInt32(result, 35 + (i * 5 * 2), 5, item.AxleWeights[i]);
		for (int i = 0; i < item.AxleSpacing.Count; i++)
			Strategies.WriteInt32(result, 40 + (i * 5 * 2), 5, item.AxleSpacing[i]);

		return result;
	}
}
