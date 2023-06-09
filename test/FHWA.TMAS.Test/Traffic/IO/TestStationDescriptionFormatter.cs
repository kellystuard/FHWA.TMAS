﻿using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestStationDescriptionFormatter
{
	[Theory]
	[MemberData(nameof(DataStationDescription))]
	public void TestStationDescriptionReading(string line, in StationDescription expected)
	{
		var formatter = new StationDescriptionFormatter();
		var actual = formatter.FromLine(line);

		Assert.Equal(expected, actual);
	}

	[Theory]
	[MemberData(nameof(DataStationDescription))]
	public void TestStationDescriptionWriting(string expected, in StationDescription stationDescription)
	{
		var formatter = new StationDescriptionFormatter();
		var actual = formatter.ToLine(stationDescription);

		Assert.Equal(expected, new string(actual));
	}

	public static IEnumerable<object[]> DataStationDescription()
	{
		var common = new StationDescription()
		{
			StateCode = State.Illinois,
			YearOfData = 2012,
			FunctionalPurpose = FunctionalPurpose.Interstate,
			FunctionalTypeCode = FunctionalType.Rural,
			StationUsedForTmas = true,
			MethodOfVolumeCounting = MethodOfVolumeCounting.PermanentDevice,
			MethodOfDataRetrieval = MethodOfDataRetrieval.Automated,
			LrsLocation = 785.600d,
			Latitude = 39.178751d,
			Longitude = -88.352540d,
			LttpSiteID = null,
			PreviousStationID = null,
			YearDiscontinued = null,
			NationalHighwaySystem = true,
			PostedRouteSigning = PostedRouteSigning.Interstate,
		};

		// VOLUME SITE (4 LANE SITE WITH ALL LANES AND DIRECTIONS COMBINED) STATION FILE
		yield return new object[]
		{
			"S1701810A9020121R2Y230    0  2L P00000000000000000000000000000000000000000000000867K65T4RE3480078560039178751088352540          2001    035Y000012345000Y0200000404.6 miles east of milepost 105 interchange         ",
			common with
			{
				StationID = "1810A",
				DirectionOfTravelCode = DirectionOfTravel.NSorNESWCombined,
				LaneOfTravelCode = 0,
				Lanes = 2,
				LanesMonitoredForVolume = 2,
				LanesMonitoredForClassificationAndOrSpeed = 0,
				MechanismOfClassificationOrSpeed = null,
				MethodOfClassification = null,
				ClassificationGroupings = null,
				LanesMonitoredForTruckWeight = 0,
				MethodOfTruckWeighing = null,
				CalibrationOfWeighing = null,
				TypeOfSensor1 = TypeOfSensor.InductiveLoop,
				TypeOfSensor2 = null,
				PrimaryPurpose = StationPurpose.Planning,
				LrsRouteID = "867K65T4RE348",
				YearEstablished = 2001,
				CountyCode = 35,
				HpmsSampleType = true,
				HpmsSampleID = "12345000",
				PostedRouteNumber = "404",
				StationLocation = ".6 miles east of milepost 105 interchange",
			},
		};

		// CLASSIFICATION (8 LANE SITE WITH ONLY LANES COMBINED) SITE STATION FILE (line 1)
		yield return new object[]
		{
			"S1701811B1020121R4Y4323F130  2LLP00000000000000000000000000000000000000000000000880IR5T4RE3480078560039178751088352540          1945    049N            Y0200000708.5 miles past Steven City near County Line Road   ",
			common with
			{
				StationID = "1811B",
				DirectionOfTravelCode = DirectionOfTravel.North,
				LaneOfTravelCode = 0,
				Lanes = 4,
				LanesMonitoredForVolume = 4,
				LanesMonitoredForClassificationAndOrSpeed = 2,
				MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
				MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
				ClassificationGroupings = ClassificationGroupings.Thirteen,
				LanesMonitoredForTruckWeight = 0,
				MethodOfTruckWeighing = null,
				CalibrationOfWeighing = null,
				TypeOfSensor1 = TypeOfSensor.InductiveLoop,
				TypeOfSensor2 = TypeOfSensor.InductiveLoop,
				PrimaryPurpose = StationPurpose.Planning,
				LrsRouteID = "880IR5T4RE348",
				YearEstablished = 1945,
				CountyCode = 49,
				HpmsSampleType = false,
				HpmsSampleID = null,
				NationalHighwaySystem = true,
				PostedRouteNumber = "708",
				StationLocation = ".5 miles past Steven City near County Line Road",
			},
		};

		// CLASSIFICATION (8 LANE SITE WITH ONLY LANES COMBINED) SITE STATION FILE (line 2)
		yield return new object[]
		{
			"S1701811B5020121R4Y4323F130  2LLP00000000000000000000000000000000000000000000000880IR5T4RE3480078560039178751088352540          1945    049N            Y0200000708.5 miles past Steven City near County Line Road   ",
			common with
			{
				StationID = "1811B",
				DirectionOfTravelCode = DirectionOfTravel.South,
				LaneOfTravelCode = 0,
				Lanes = 4,
				LanesMonitoredForVolume = 4,
				LanesMonitoredForClassificationAndOrSpeed = 2,
				MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
				MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
				ClassificationGroupings = ClassificationGroupings.Thirteen,
				LanesMonitoredForTruckWeight = 0,
				MethodOfTruckWeighing = null,
				CalibrationOfWeighing = null,
				TypeOfSensor1 = TypeOfSensor.InductiveLoop,
				TypeOfSensor2 = TypeOfSensor.InductiveLoop,
				PrimaryPurpose = StationPurpose.Planning,
				LrsRouteID = "880IR5T4RE348",
				YearEstablished = 1945,
				CountyCode = 49,
				HpmsSampleType = false,
				HpmsSampleID = null,
				PostedRouteNumber = "708",
				StationLocation = ".5 miles past Steven City near County Line Road",
			},
		};

		// WEIGHT SITE (2 LANE SITE) STATION FILE (line 1)
		yield return new object[]
		{
			"S1701812C1120121R1Y4313F1315A2QLL00000000000000000000000000000000000000000000000890IR5T4RE3480078560039178751088352540          1965    049Y008905673459Y0200000708.5 miles past Steven City near County Line Road   ",
			common with
			{
				StationID = "1812C",
				DirectionOfTravelCode = DirectionOfTravel.North,
				LaneOfTravelCode = 1,
				Lanes = 1,
				LanesMonitoredForVolume = 4,
				LanesMonitoredForClassificationAndOrSpeed = 1,
				MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
				MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
				ClassificationGroupings = ClassificationGroupings.Thirteen,
				LanesMonitoredForTruckWeight = 1,
				MethodOfTruckWeighing = MethodOfTruckWeighing.PermanentWIM,
				CalibrationOfWeighing = CalibrationOfWeighing.E1318,
				TypeOfSensor1 = TypeOfSensor.QuartzPiezoelectric,
				TypeOfSensor2 = TypeOfSensor.InductiveLoop,
				PrimaryPurpose = StationPurpose.LoadData,
				LrsRouteID = "890IR5T4RE348",
				YearEstablished = 1965,
				CountyCode = 49,
				HpmsSampleType = true,
				HpmsSampleID = "8905673459",
				PostedRouteNumber = "708",
				StationLocation = ".5 miles past Steven City near County Line Road",
			},
		};

		// WEIGHT SITE (2 LANE SITE) STATION FILE (line 1)
		yield return new object[]
		{
			"S1701812C5120121R1Y4313F1315A2QLL00000000000000000000000000000000000000000000000890IR5T4RE3480078560039178751088352540          1965    049Y008905673459Y0200000708.5 miles past Steven City near County Line Road   ",
			common with
			{
				StationID = "1812C",
				DirectionOfTravelCode = DirectionOfTravel.South,
				LaneOfTravelCode = 1,
				Lanes = 1,
				LanesMonitoredForVolume = 4,
				LanesMonitoredForClassificationAndOrSpeed = 1,
				MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
				MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
				ClassificationGroupings = ClassificationGroupings.Thirteen,
				LanesMonitoredForTruckWeight = 1,
				MethodOfTruckWeighing = MethodOfTruckWeighing.PermanentWIM,
				CalibrationOfWeighing = CalibrationOfWeighing.E1318,
				TypeOfSensor1 = TypeOfSensor.QuartzPiezoelectric,
				TypeOfSensor2 = TypeOfSensor.InductiveLoop,
				PrimaryPurpose = StationPurpose.LoadData,
				LrsRouteID = "890IR5T4RE348",
				YearEstablished = 1965,
				CountyCode = 49,
				HpmsSampleType = true,
				HpmsSampleID = "8905673459",
				PostedRouteNumber = "708",
				StationLocation = ".5 miles past Steven City near County Line Road",
			},
		};
	}
}
