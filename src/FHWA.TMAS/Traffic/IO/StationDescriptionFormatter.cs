namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Handles the formatting to and from disk for <see cref="StationDescription"/>.
/// </summary>
public sealed class StationDescriptionFormatter : ITrafficFormatter<StationDescription>
{
	/// <inheritdoc />
	public static string FileExtension => "sta";

	/// <inheritdoc />
	public StationDescription FromLine(in ReadOnlySpan<char> line)
	{
		if(line.Length != 213)
			throw new ArgumentException("Station description lines must be exactly 213 characters long", nameof(line));
		if (Strategies.ReadChar(line, 1) != 'S')
			throw new ArgumentOutOfRangeException(nameof(line), Strategies.ReadChar(line, 1), "Can only read station description records");

		var result = new StationDescription
		{
			StateCode = (Fips.State?)Strategies.ReadInt32(line, 2, 2) ?? throw new NullReferenceException(),
			StationID = Strategies.ReadLeftZeroFilledString(line, 4, 6) ?? throw new NullReferenceException(),
			DirectionOfTravelCode = (DirectionOfTravel?)Strategies.ReadChar(line, 10) ?? throw new NullReferenceException(),
			LaneOfTravelCode = Strategies.ReadNumber(line, 11),
			YearOfData = Strategies.ReadInt32(line, 12, 4) ?? throw new NullReferenceException(),
			FunctionalPurpose = (FunctionalPurpose?)Strategies.ReadChar(line, 16) ?? throw new NullReferenceException(),
			FunctionalTypeCode = (FunctionalType?)Strategies.ReadChar(line, 17) ?? throw new NullReferenceException(),
			Lanes = Strategies.ReadNumber(line, 18),
			StationUsedForTmas = Strategies.ReadChar(line, 19) == 'Y',
			LanesMonitoredForVolume = Strategies.ReadNumber(line, 20),
			MethodOfVolumeCounting = (MethodOfVolumeCounting?)Strategies.ReadChar(line, 21) ?? throw new NullReferenceException(),
			LanesMonitoredForClassificationAndOrSpeed = Strategies.ReadNumber(line, 22),
			MechanismOfClassificationOrSpeed = (MechanismOfClassificationOrSpeed?)Strategies.ReadChar(line, 23),
			MethodOfClassification = (MethodOfClassification?)Strategies.ReadChar(line, 24),
			ClassificationGroupings = (ClassificationGroupings?)Strategies.ReadInt32(line, 25, 2),
			LanesMonitoredForTruckWeight = Strategies.ReadNumber(line, 27),
			MethodOfTruckWeighing = (MethodOfTruckWeighing?)Strategies.ReadChar(line, 28),
			CalibrationOfWeighing = (CalibrationOfWeighing?)Strategies.ReadChar(line, 29),
			MethodOfDataRetrieval = (MethodOfDataRetrieval?)Strategies.ReadChar(line, 30),
			TypeOfSensor1 = (TypeOfSensor?)Strategies.ReadChar(line, 31) ?? throw new NullReferenceException(),
			TypeOfSensor2 = (TypeOfSensor?)Strategies.ReadChar(line, 32),
			PrimaryPurpose = (StationPurpose?)Strategies.ReadChar(line, 33) ?? throw new NullReferenceException(),
			LrsRouteID = Strategies.ReadLeftZeroFilledString(line, 34, 60) ?? throw new NullReferenceException(),
			LrsLocation = Strategies.ReadDecimal(line, 94, 8, leftShift: 3),
			Latitude = Strategies.ReadDecimal(line, 102, 8, leftShift: 6),
			Longitude = Strategies.ReadDecimal(line, 110, 9, leftShift: 6) * -1,
			LttpSiteID = Strategies.ReadString(line, 119, 4),
			PreviousStationID = Strategies.ReadLeftZeroFilledString(line, 123, 6),
			YearEstablished = Strategies.ReadInt32(line, 129, 4),
			YearDiscontinued = Strategies.ReadInt32(line, 133, 4),
			CountyCode = Strategies.ReadInt32(line, 137, 3) ?? throw new NullReferenceException(),
			HpmsSampleType = Strategies.ReadChar(line, 140) == 'Y',
			HpmsSampleID = Strategies.ReadLeftZeroFilledString(line, 141, 12),
			NationalHighwaySystem = Strategies.ReadChar(line, 153) == 'Y',
			PostedRouteSigning = (PostedRouteSigning?)Strategies.ReadInt32(line, 154, 2) ?? throw new NullReferenceException(),
			PostedRouteNumber = Strategies.ReadLeftZeroFilledString(line, 156, 8),
			StationLocation = Strategies.ReadString(line, 164, 50),
		};

		return result;
	}

	/// <inheritdoc />
	public ReadOnlySpan<char> ToLine(StationDescription item)
	{
		var result = new char[213];

		Strategies.WriteChar(result, 1, 'S');
		Strategies.WriteInt32(result, 2, 2, (int)item.StateCode);
		Strategies.WriteLeftZeroFilledString(result, 4, 6, item.StationID);
		Strategies.WriteChar(result, 10, (char)item.DirectionOfTravelCode);
		Strategies.WriteNumber(result, 11, item.LaneOfTravelCode);
		Strategies.WriteInt32(result, 12, 4, item.YearOfData);
		Strategies.WriteChar(result, 16, (char)item.FunctionalPurpose);
		Strategies.WriteChar(result, 17, (char)item.FunctionalTypeCode);
		Strategies.WriteNumber(result, 18, item.Lanes);
		Strategies.WriteChar(result, 19, item.StationUsedForTmas ? 'Y' : 'N');
		Strategies.WriteNumber(result, 20, item.LanesMonitoredForVolume);
		Strategies.WriteChar(result, 21, (char)item.MethodOfVolumeCounting);
		Strategies.WriteNumber(result, 22, item.LanesMonitoredForClassificationAndOrSpeed);
		Strategies.WriteChar(result, 23, (char?)item.MechanismOfClassificationOrSpeed);
		Strategies.WriteChar(result, 24, (char?)item.MethodOfClassification);
		Strategies.WriteInt32(result, 25, 2, (int?)item.ClassificationGroupings);
		Strategies.WriteNumber(result, 27, item.LanesMonitoredForTruckWeight);
		Strategies.WriteChar(result, 28, (char?)item.MethodOfTruckWeighing);
		Strategies.WriteChar(result, 29, (char?)item.CalibrationOfWeighing);
		Strategies.WriteChar(result, 30, (char?)item.MethodOfDataRetrieval);
		Strategies.WriteChar(result, 31, (char)item.TypeOfSensor1);
		Strategies.WriteChar(result, 32, (char?)item.TypeOfSensor2);
		Strategies.WriteChar(result, 33, (char)item.PrimaryPurpose);
		Strategies.WriteLeftZeroFilledString(result, 34, 60, item.LrsRouteID);
		Strategies.WriteDecimal(result, 94, 8, item.LrsLocation, leftShift: 3);
		Strategies.WriteDecimal(result, 102, 8, item.Latitude, leftShift: 6);
		Strategies.WriteDecimal(result, 110, 9, item.Longitude * -1, leftShift: 6);
		Strategies.WriteString(result, 119, 4, item.LttpSiteID);
		Strategies.WriteLeftZeroFilledString(result, 123, 6, item.PreviousStationID);
		Strategies.WriteInt32(result, 129, 4, item.YearEstablished);
		Strategies.WriteInt32(result, 133, 4, item.YearDiscontinued);
		Strategies.WriteInt32(result, 137, 3, item.CountyCode);
		Strategies.WriteChar(result, 140, item.HpmsSampleType ? 'Y' : 'N');
		Strategies.WriteLeftZeroFilledString(result, 141, 12, item.HpmsSampleID);
		Strategies.WriteChar(result, 153, item.NationalHighwaySystem ? 'Y' : 'N');
		Strategies.WriteInt32(result, 154, 2, (int)item.PostedRouteSigning);
		Strategies.WriteLeftZeroFilledString(result, 156, 8, item.PostedRouteNumber);
		Strategies.WriteString(result, 164, 50, item.StationLocation);

		return result;
	}
}
