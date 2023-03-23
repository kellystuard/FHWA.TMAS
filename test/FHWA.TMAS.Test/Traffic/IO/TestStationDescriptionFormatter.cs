using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestStationDescriptionFormatter
{
	[Fact]
	public void TestVolumeSiteReading()
	{
		// VOLUME SITE (4 LANE SITE WITH ALL LANES AND DIRECTIONS COMBINED) STATION FILE
		var formatter = new StationDescriptionFormatter();
		var actual = formatter.FromLine("S1701810A9020121R2Y230    0  2L P00000000000000000000000000000000000000000000000867K65T4RE3480078560039178751088352540          2001    035Y000012345000Y0200000404.6 miles east of milepost 105 interchange         ");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal("1810A", actual.StationID);
		Assert.Equal(DirectionOfTravel.NSorNESWCombined, actual.DirectionOfTravelCode);
		Assert.Equal(0, actual.LaneOfTravelCode);
		Assert.Equal(2012, actual.YearOfData);
		Assert.Equal(FunctionalPurpose.Interstate, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal(2, actual.Lanes);
		Assert.True(actual.StationUsedForTmas);
		Assert.Equal(2, actual.LanesMonitoredForVolume);
		Assert.Equal(MethodOfVolumeCounting.PermanentDevice, actual.MethodOfVolumeCounting);
		Assert.Equal(0, actual.LanesMonitoredForClassificationAndOrSpeed);
		Assert.Null(actual.MechanismOfClassificationOrSpeed);
		Assert.Null(actual.MethodOfClassification);
		Assert.Null(actual.ClassificationGroupings);
		Assert.Equal(0, actual.LanesMonitoredForTruckWeight);
		Assert.Null(actual.MethodOfTruckWeighing);
		Assert.Null(actual.CalibrationOfWeighing);
		Assert.Equal(MethodOfDataRetrieval.Automated, actual.MethodOfDataRetrieval);
		Assert.Equal(TypeOfSensor.InductiveLoop, actual.TypeOfSensor1);
		Assert.Null(actual.TypeOfSensor2);
		Assert.Equal(StationPurpose.Planning, actual.PrimaryPurpose);
		Assert.Equal("867K65T4RE348", actual.LrsRouteID);
		Assert.Equal(785.600d, actual.LrsLocation);
		Assert.Equal(39.178751d, actual.Latitude);
		Assert.Equal(-88.352540d, actual.Longitude);
		Assert.Null(actual.LttpSiteID);
		Assert.Null(actual.PreviousStationID);
		Assert.Equal(2001, actual.YearEstablished);
		Assert.Null(actual.YearDiscontinued);
		Assert.Equal(35, actual.CountyCode);
		Assert.True(actual.HpmsSampleType);
		Assert.Equal("12345000", actual.HpmsSampleID);
		Assert.True(actual.NationalHighwaySystem);
		Assert.Equal(PostedRouteSigning.Interstate, actual.PostedRouteSigning);
		Assert.Equal("404", actual.PostedRouteNumber);
		Assert.Equal(".6 miles east of milepost 105 interchange", actual.StationLocation);
	}

	[Fact]
	public void TestVolumeSiteWriting()
	{
		// VOLUME SITE (4 LANE SITE WITH ALL LANES AND DIRECTIONS COMBINED) STATION FILE
		var formatter = new StationDescriptionFormatter();
		var stationDescription = new StationDescription()
		{
			StateCode = State.Illinois,
			StationID = "1810A",
			DirectionOfTravelCode = DirectionOfTravel.NSorNESWCombined,
			LaneOfTravelCode = 0,
			YearOfData = 2012,
			FunctionalPurpose = FunctionalPurpose.Interstate,
			FunctionalTypeCode = FunctionalType.Rural,
			Lanes = 2,
			StationUsedForTmas = true,
			LanesMonitoredForVolume = 2,
			MethodOfVolumeCounting = MethodOfVolumeCounting.PermanentDevice,
			LanesMonitoredForClassificationAndOrSpeed = 0,
			MechanismOfClassificationOrSpeed = null,
			MethodOfClassification = null,
			ClassificationGroupings = null,
			LanesMonitoredForTruckWeight = 0,
			MethodOfTruckWeighing = null,
			CalibrationOfWeighing = null,
			MethodOfDataRetrieval = MethodOfDataRetrieval.Automated,
			TypeOfSensor1 = TypeOfSensor.InductiveLoop,
			TypeOfSensor2 = null,
			PrimaryPurpose = StationPurpose.Planning,
			LrsRouteID = "867K65T4RE348",
			LrsLocation = 785.600d,
			Latitude = 39.178751d,
			Longitude = -88.352540d,
			LttpSiteID = null,
			PreviousStationID = null,
			YearEstablished = 2001,
			YearDiscontinued = null,
			CountyCode = 35,
			HpmsSampleType = true,
			HpmsSampleID = "12345000",
			NationalHighwaySystem = true,
			PostedRouteSigning = PostedRouteSigning.Interstate,
			PostedRouteNumber = "404",
			StationLocation = ".6 miles east of milepost 105 interchange",
		};

		var expected = "S1701810A9020121R2Y230    0  2L P00000000000000000000000000000000000000000000000867K65T4RE3480078560039178751088352540          2001    035Y000012345000Y0200000404.6 miles east of milepost 105 interchange         ";
		var actual = formatter.ToLine(stationDescription);

		Assert.Equal(expected, new string(actual));
	}

	[Fact]
	public void TestClassification1SiteReading()
	{
		// CLASSIFICATION (8 LANE SITE WITH ONLY LANES COMBINED) SITE STATION FILE
		var formatter = new StationDescriptionFormatter();
		var actual = formatter.FromLine("S1701811B1020121R4Y4323F130  2LLP00000000000000000000000000000000000000000000000880IR5T4RE3480078560039178751088352540          1945    049N            Y0200000708.5 miles past Steven City near County Line Road   ");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal("1811B", actual.StationID);
		Assert.Equal(DirectionOfTravel.North, actual.DirectionOfTravelCode);
		Assert.Equal(0, actual.LaneOfTravelCode);
		Assert.Equal(2012, actual.YearOfData);
		Assert.Equal(FunctionalPurpose.Interstate, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal(4, actual.Lanes);
		Assert.True(actual.StationUsedForTmas);
		Assert.Equal(4, actual.LanesMonitoredForVolume);
		Assert.Equal(MethodOfVolumeCounting.PermanentDevice, actual.MethodOfVolumeCounting);
		Assert.Equal(2, actual.LanesMonitoredForClassificationAndOrSpeed);
		Assert.Equal(MechanismOfClassificationOrSpeed.PermanentDevice, actual.MechanismOfClassificationOrSpeed);
		Assert.Equal(MethodOfClassification.AxleSpacingFHWA13, actual.MethodOfClassification);
		Assert.Equal(ClassificationGroupings.Thirteen, actual.ClassificationGroupings);
		Assert.Equal(0, actual.LanesMonitoredForTruckWeight);
		Assert.Null(actual.MethodOfTruckWeighing);
		Assert.Null(actual.CalibrationOfWeighing);
		Assert.Equal(MethodOfDataRetrieval.Automated, actual.MethodOfDataRetrieval);
		Assert.Equal(TypeOfSensor.InductiveLoop, actual.TypeOfSensor1);
		Assert.Equal(TypeOfSensor.InductiveLoop, actual.TypeOfSensor2);
		Assert.Equal(StationPurpose.Planning, actual.PrimaryPurpose);
		Assert.Equal("880IR5T4RE348", actual.LrsRouteID);
		Assert.Equal(785.600d, actual.LrsLocation);
		Assert.Equal(39.178751d, actual.Latitude);
		Assert.Equal(-88.352540d, actual.Longitude);
		Assert.Null(actual.LttpSiteID);
		Assert.Null(actual.PreviousStationID);
		Assert.Equal(1945, actual.YearEstablished);
		Assert.Null(actual.YearDiscontinued);
		Assert.Equal(49, actual.CountyCode);
		Assert.False(actual.HpmsSampleType);
		Assert.Null(actual.HpmsSampleID);
		Assert.True(actual.NationalHighwaySystem);
		Assert.Equal(PostedRouteSigning.Interstate, actual.PostedRouteSigning);
		Assert.Equal("708", actual.PostedRouteNumber);
		Assert.Equal(".5 miles past Steven City near County Line Road", actual.StationLocation);
	}

	[Fact]
	public void TestClassification1SiteWriting()
	{
		// CLASSIFICATION (8 LANE SITE WITH ONLY LANES COMBINED) SITE STATION FILE
		var formatter = new StationDescriptionFormatter();
		var stationDescription = new StationDescription()
		{
			StateCode = State.Illinois,
			StationID = "1811B",
			DirectionOfTravelCode = DirectionOfTravel.North,
			LaneOfTravelCode = 0,
			YearOfData = 2012,
			FunctionalPurpose = FunctionalPurpose.Interstate,
			FunctionalTypeCode = FunctionalType.Rural,
			Lanes = 4,
			StationUsedForTmas = true,
			LanesMonitoredForVolume = 4,
			MethodOfVolumeCounting = MethodOfVolumeCounting.PermanentDevice,
			LanesMonitoredForClassificationAndOrSpeed = 2,
			MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
			MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
			ClassificationGroupings = ClassificationGroupings.Thirteen,
			LanesMonitoredForTruckWeight = 0,
			MethodOfTruckWeighing = null,
			CalibrationOfWeighing = null,
			MethodOfDataRetrieval = MethodOfDataRetrieval.Automated,
			TypeOfSensor1 = TypeOfSensor.InductiveLoop,
			TypeOfSensor2 = TypeOfSensor.InductiveLoop,
			PrimaryPurpose = StationPurpose.Planning,
			LrsRouteID = "880IR5T4RE348",
			LrsLocation = 785.600d,
			Latitude = 39.178751d,
			Longitude = -88.352540d,
			LttpSiteID = null,
			PreviousStationID = null,
			YearEstablished = 1945,
			YearDiscontinued = null,
			CountyCode = 49,
			HpmsSampleType = false,
			HpmsSampleID = null,
			NationalHighwaySystem = true,
			PostedRouteSigning = PostedRouteSigning.Interstate,
			PostedRouteNumber = "708",
			StationLocation = ".5 miles past Steven City near County Line Road",
		};

		var expected = "S1701811B1020121R4Y4323F130  2LLP00000000000000000000000000000000000000000000000880IR5T4RE3480078560039178751088352540          1945    049N            Y0200000708.5 miles past Steven City near County Line Road   ";
		var actual = formatter.ToLine(stationDescription);

		Assert.Equal(expected, new string(actual));
	}

	[Fact]
	public void TestClassification2SiteReading()
	{
		// CLASSIFICATION (8 LANE SITE WITH ONLY LANES COMBINED) SITE STATION FILE
		var formatter = new StationDescriptionFormatter();
		var actual = formatter.FromLine("S1701811B5020121R4Y4323F130  2LLP00000000000000000000000000000000000000000000000880IR5T4RE3480078560039178751088352540          1945    049N            Y0200000708.5 miles past Steven City near County Line Road   ");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal("1811B", actual.StationID);
		Assert.Equal(DirectionOfTravel.South, actual.DirectionOfTravelCode);
		Assert.Equal(0, actual.LaneOfTravelCode);
		Assert.Equal(2012, actual.YearOfData);
		Assert.Equal(FunctionalPurpose.Interstate, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal(4, actual.Lanes);
		Assert.True(actual.StationUsedForTmas);
		Assert.Equal(4, actual.LanesMonitoredForVolume);
		Assert.Equal(MethodOfVolumeCounting.PermanentDevice, actual.MethodOfVolumeCounting);
		Assert.Equal(2, actual.LanesMonitoredForClassificationAndOrSpeed);
		Assert.Equal(MechanismOfClassificationOrSpeed.PermanentDevice, actual.MechanismOfClassificationOrSpeed);
		Assert.Equal(MethodOfClassification.AxleSpacingFHWA13, actual.MethodOfClassification);
		Assert.Equal(ClassificationGroupings.Thirteen, actual.ClassificationGroupings);
		Assert.Equal(0, actual.LanesMonitoredForTruckWeight);
		Assert.Null(actual.MethodOfTruckWeighing);
		Assert.Null(actual.CalibrationOfWeighing);
		Assert.Equal(MethodOfDataRetrieval.Automated, actual.MethodOfDataRetrieval);
		Assert.Equal(TypeOfSensor.InductiveLoop, actual.TypeOfSensor1);
		Assert.Equal(TypeOfSensor.InductiveLoop, actual.TypeOfSensor2);
		Assert.Equal(StationPurpose.Planning, actual.PrimaryPurpose);
		Assert.Equal("880IR5T4RE348", actual.LrsRouteID);
		Assert.Equal(785.600d, actual.LrsLocation);
		Assert.Equal(39.178751d, actual.Latitude);
		Assert.Equal(-88.352540d, actual.Longitude);
		Assert.Null(actual.LttpSiteID);
		Assert.Null(actual.PreviousStationID);
		Assert.Equal(1945, actual.YearEstablished);
		Assert.Null(actual.YearDiscontinued);
		Assert.Equal(49, actual.CountyCode);
		Assert.False(actual.HpmsSampleType);
		Assert.Null(actual.HpmsSampleID);
		Assert.True(actual.NationalHighwaySystem);
		Assert.Equal(PostedRouteSigning.Interstate, actual.PostedRouteSigning);
		Assert.Equal("708", actual.PostedRouteNumber);
		Assert.Equal(".5 miles past Steven City near County Line Road", actual.StationLocation);
	}

	[Fact]
	public void TestClassification2SiteWriting()
	{
		// CLASSIFICATION (8 LANE SITE WITH ONLY LANES COMBINED) SITE STATION FILE
		var formatter = new StationDescriptionFormatter();
		var stationDescription = new StationDescription()
		{
			StateCode = State.Illinois,
			StationID = "1811B",
			DirectionOfTravelCode = DirectionOfTravel.South,
			LaneOfTravelCode = 0,
			YearOfData = 2012,
			FunctionalPurpose = FunctionalPurpose.Interstate,
			FunctionalTypeCode = FunctionalType.Rural,
			Lanes = 4,
			StationUsedForTmas = true,
			LanesMonitoredForVolume = 4,
			MethodOfVolumeCounting = MethodOfVolumeCounting.PermanentDevice,
			LanesMonitoredForClassificationAndOrSpeed = 2,
			MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
			MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
			ClassificationGroupings = ClassificationGroupings.Thirteen,
			LanesMonitoredForTruckWeight = 0,
			MethodOfTruckWeighing = null,
			CalibrationOfWeighing = null,
			MethodOfDataRetrieval = MethodOfDataRetrieval.Automated,
			TypeOfSensor1 = TypeOfSensor.InductiveLoop,
			TypeOfSensor2 = TypeOfSensor.InductiveLoop,
			PrimaryPurpose = StationPurpose.Planning,
			LrsRouteID = "880IR5T4RE348",
			LrsLocation = 785.600d,
			Latitude = 39.178751d,
			Longitude = -88.352540d,
			LttpSiteID = null,
			PreviousStationID = null,
			YearEstablished = 1945,
			YearDiscontinued = null,
			CountyCode = 49,
			HpmsSampleType = false,
			HpmsSampleID = null,
			NationalHighwaySystem = true,
			PostedRouteSigning = PostedRouteSigning.Interstate,
			PostedRouteNumber = "708",
			StationLocation = ".5 miles past Steven City near County Line Road",
		};

		var expected = "S1701811B5020121R4Y4323F130  2LLP00000000000000000000000000000000000000000000000880IR5T4RE3480078560039178751088352540          1945    049N            Y0200000708.5 miles past Steven City near County Line Road   ";
		var actual = formatter.ToLine(stationDescription);

		Assert.Equal(expected, new string(actual));
	}

	[Fact]
	public void TestWeightSite1Reading()
	{
		// WEIGHT SITE (2 LANE SITE) STATION FILE
		var formatter = new StationDescriptionFormatter();
		var actual = formatter.FromLine("S1701812C1120121R1Y4313F1315A2QLL00000000000000000000000000000000000000000000000890IR5T4RE3480078560039178751088352540          1965    049Y008905673459Y0200000708.5 miles past Steven City near County Line Road   ");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal("1812C", actual.StationID);
		Assert.Equal(DirectionOfTravel.North, actual.DirectionOfTravelCode);
		Assert.Equal(1, actual.LaneOfTravelCode);
		Assert.Equal(2012, actual.YearOfData);
		Assert.Equal(FunctionalPurpose.Interstate, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal(1, actual.Lanes);
		Assert.True(actual.StationUsedForTmas);
		Assert.Equal(4, actual.LanesMonitoredForVolume);
		Assert.Equal(MethodOfVolumeCounting.PermanentDevice, actual.MethodOfVolumeCounting);
		Assert.Equal(1, actual.LanesMonitoredForClassificationAndOrSpeed);
		Assert.Equal(MechanismOfClassificationOrSpeed.PermanentDevice, actual.MechanismOfClassificationOrSpeed);
		Assert.Equal(MethodOfClassification.AxleSpacingFHWA13, actual.MethodOfClassification);
		Assert.Equal(ClassificationGroupings.Thirteen, actual.ClassificationGroupings);
		Assert.Equal(1, actual.LanesMonitoredForTruckWeight);
		Assert.Equal(MethodOfTruckWeighing.PermanentWIM, actual.MethodOfTruckWeighing);
		Assert.Equal(CalibrationOfWeighing.E1318, actual.CalibrationOfWeighing);
		Assert.Equal(MethodOfDataRetrieval.Automated, actual.MethodOfDataRetrieval);
		Assert.Equal(TypeOfSensor.QuartzPiezoelectric, actual.TypeOfSensor1);
		Assert.Equal(TypeOfSensor.InductiveLoop, actual.TypeOfSensor2);
		Assert.Equal(StationPurpose.LoadData, actual.PrimaryPurpose);
		Assert.Equal("890IR5T4RE348", actual.LrsRouteID);
		Assert.Equal(785.600d, actual.LrsLocation);
		Assert.Equal(39.178751d, actual.Latitude);
		Assert.Equal(-88.352540d, actual.Longitude);
		Assert.Null(actual.LttpSiteID);
		Assert.Null(actual.PreviousStationID);
		Assert.Equal(1965, actual.YearEstablished);
		Assert.Null(actual.YearDiscontinued);
		Assert.Equal(49, actual.CountyCode);
		Assert.True(actual.HpmsSampleType);
		Assert.Equal("8905673459", actual.HpmsSampleID);
		Assert.True(actual.NationalHighwaySystem);
		Assert.Equal(PostedRouteSigning.Interstate, actual.PostedRouteSigning);
		Assert.Equal("708", actual.PostedRouteNumber);
		Assert.Equal(".5 miles past Steven City near County Line Road", actual.StationLocation);
	}

	[Fact]
	public void TestWeightSite1Writing()
	{
		// WEIGHT SITE (2 LANE SITE) STATION FILE
		var formatter = new StationDescriptionFormatter();
		var stationDescription = new StationDescription()
		{
			StateCode = State.Illinois,
			StationID = "1812C",
			DirectionOfTravelCode = DirectionOfTravel.North,
			LaneOfTravelCode = 1,
			YearOfData = 2012,
			FunctionalPurpose = FunctionalPurpose.Interstate,
			FunctionalTypeCode = FunctionalType.Rural,
			Lanes = 1,
			StationUsedForTmas = true,
			LanesMonitoredForVolume = 4,
			MethodOfVolumeCounting = MethodOfVolumeCounting.PermanentDevice,
			LanesMonitoredForClassificationAndOrSpeed = 1,
			MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
			MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
			ClassificationGroupings = ClassificationGroupings.Thirteen,
			LanesMonitoredForTruckWeight = 1,
			MethodOfTruckWeighing = MethodOfTruckWeighing.PermanentWIM,
			CalibrationOfWeighing = CalibrationOfWeighing.E1318,
			MethodOfDataRetrieval = MethodOfDataRetrieval.Automated,
			TypeOfSensor1 = TypeOfSensor.QuartzPiezoelectric,
			TypeOfSensor2 = TypeOfSensor.InductiveLoop,
			PrimaryPurpose = StationPurpose.LoadData,
			LrsRouteID = "890IR5T4RE348",
			LrsLocation = 785.600d,
			Latitude = 39.178751d,
			Longitude = -88.352540d,
			LttpSiteID = null,
			PreviousStationID = null,
			YearEstablished = 1965,
			YearDiscontinued = null,
			CountyCode = 49,
			HpmsSampleType = true,
			HpmsSampleID = "8905673459",
			NationalHighwaySystem = true,
			PostedRouteSigning = PostedRouteSigning.Interstate,
			PostedRouteNumber = "708",
			StationLocation = ".5 miles past Steven City near County Line Road",
		};

		var expected = "S1701812C1120121R1Y4313F1315A2QLL00000000000000000000000000000000000000000000000890IR5T4RE3480078560039178751088352540          1965    049Y008905673459Y0200000708.5 miles past Steven City near County Line Road   ";
		var actual = formatter.ToLine(stationDescription);

		Assert.Equal(expected, new string(actual));
	}

	[Fact]
	public void TestWeightSite2Reading()
	{
		// WEIGHT SITE (2 LANE SITE) STATION FILE
		var formatter = new StationDescriptionFormatter();
		var actual = formatter.FromLine("S1701812C5120121R1Y4313F1315A2QLL00000000000000000000000000000000000000000000000890IR5T4RE3480078560039178751088352540          1965    049Y008905673459Y0200000708.5 miles past Steven City near County Line Road   ");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal("1812C", actual.StationID);
		Assert.Equal(DirectionOfTravel.South, actual.DirectionOfTravelCode);
		Assert.Equal(1, actual.LaneOfTravelCode);
		Assert.Equal(2012, actual.YearOfData);
		Assert.Equal(FunctionalPurpose.Interstate, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal(1, actual.Lanes);
		Assert.True(actual.StationUsedForTmas);
		Assert.Equal(4, actual.LanesMonitoredForVolume);
		Assert.Equal(MethodOfVolumeCounting.PermanentDevice, actual.MethodOfVolumeCounting);
		Assert.Equal(1, actual.LanesMonitoredForClassificationAndOrSpeed);
		Assert.Equal(MechanismOfClassificationOrSpeed.PermanentDevice, actual.MechanismOfClassificationOrSpeed);
		Assert.Equal(MethodOfClassification.AxleSpacingFHWA13, actual.MethodOfClassification);
		Assert.Equal(ClassificationGroupings.Thirteen, actual.ClassificationGroupings);
		Assert.Equal(1, actual.LanesMonitoredForTruckWeight);
		Assert.Equal(MethodOfTruckWeighing.PermanentWIM, actual.MethodOfTruckWeighing);
		Assert.Equal(CalibrationOfWeighing.E1318, actual.CalibrationOfWeighing);
		Assert.Equal(MethodOfDataRetrieval.Automated, actual.MethodOfDataRetrieval);
		Assert.Equal(TypeOfSensor.QuartzPiezoelectric, actual.TypeOfSensor1);
		Assert.Equal(TypeOfSensor.InductiveLoop, actual.TypeOfSensor2);
		Assert.Equal(StationPurpose.LoadData, actual.PrimaryPurpose);
		Assert.Equal("890IR5T4RE348", actual.LrsRouteID);
		Assert.Equal(785.600d, actual.LrsLocation);
		Assert.Equal(39.178751d, actual.Latitude);
		Assert.Equal(-88.352540d, actual.Longitude);
		Assert.Null(actual.LttpSiteID);
		Assert.Null(actual.PreviousStationID);
		Assert.Equal(1965, actual.YearEstablished);
		Assert.Null(actual.YearDiscontinued);
		Assert.Equal(49, actual.CountyCode);
		Assert.True(actual.HpmsSampleType);
		Assert.Equal("8905673459", actual.HpmsSampleID);
		Assert.True(actual.NationalHighwaySystem);
		Assert.Equal(PostedRouteSigning.Interstate, actual.PostedRouteSigning);
		Assert.Equal("708", actual.PostedRouteNumber);
		Assert.Equal(".5 miles past Steven City near County Line Road", actual.StationLocation);
	}

	[Fact]
	public void TestWeightSite2Writing()
	{
		// WEIGHT SITE (2 LANE SITE) STATION FILE
		var formatter = new StationDescriptionFormatter();
		var stationDescription = new StationDescription()
		{
			StateCode = State.Illinois,
			StationID = "1812C",
			DirectionOfTravelCode = DirectionOfTravel.South,
			LaneOfTravelCode = 1,
			YearOfData = 2012,
			FunctionalPurpose = FunctionalPurpose.Interstate,
			FunctionalTypeCode = FunctionalType.Rural,
			Lanes = 1,
			StationUsedForTmas = true,
			LanesMonitoredForVolume = 4,
			MethodOfVolumeCounting = MethodOfVolumeCounting.PermanentDevice,
			LanesMonitoredForClassificationAndOrSpeed = 1,
			MechanismOfClassificationOrSpeed = MechanismOfClassificationOrSpeed.PermanentDevice,
			MethodOfClassification = MethodOfClassification.AxleSpacingFHWA13,
			ClassificationGroupings = ClassificationGroupings.Thirteen,
			LanesMonitoredForTruckWeight = 1,
			MethodOfTruckWeighing = MethodOfTruckWeighing.PermanentWIM,
			CalibrationOfWeighing = CalibrationOfWeighing.E1318,
			MethodOfDataRetrieval = MethodOfDataRetrieval.Automated,
			TypeOfSensor1 = TypeOfSensor.QuartzPiezoelectric,
			TypeOfSensor2 = TypeOfSensor.InductiveLoop,
			PrimaryPurpose = StationPurpose.LoadData,
			LrsRouteID = "890IR5T4RE348",
			LrsLocation = 785.600d,
			Latitude = 39.178751d,
			Longitude = -88.352540d,
			LttpSiteID = null,
			PreviousStationID = null,
			YearEstablished = 1965,
			YearDiscontinued = null,
			CountyCode = 49,
			HpmsSampleType = true,
			HpmsSampleID = "8905673459",
			NationalHighwaySystem = true,
			PostedRouteSigning = PostedRouteSigning.Interstate,
			PostedRouteNumber = "708",
			StationLocation = ".5 miles past Steven City near County Line Road",
		};

		var expected = "S1701812C5120121R1Y4313F1315A2QLL00000000000000000000000000000000000000000000000890IR5T4RE3480078560039178751088352540          1965    049Y008905673459Y0200000708.5 miles past Steven City near County Line Road   ";
		var actual = formatter.ToLine(stationDescription);

		Assert.Equal(expected, new string(actual));
	}
}
