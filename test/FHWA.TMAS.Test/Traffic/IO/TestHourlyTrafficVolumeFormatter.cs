using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;
using System.Runtime.Serialization;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestHourlyTrafficVolumeFormatter
{
	[Fact]
	public void TestVolumeSiteCombinedLanesAndDirectionsReading()
	{
		// VOLUME SITE (4 LANES WITH ALL LANES AND DIRECTIONS COMBINED) VOLUME FILE
		var formatter = new HourlyTrafficVolumeFormatter();
		var actual = formatter.FromLine("3172R01710A90201204254000460002200014000130002900030000750013600179002180026400293003220040100439          003660026100202001430009800054000220");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal(FunctionalPurpose.PrincipalArterial, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal("1710A", actual.StationID);
		Assert.Equal(DirectionOfTravel.NSorNESWCombined, actual.DirectionOfTravelCode);
		Assert.Equal(0, actual.LaneOfTravelCode);
		Assert.Equal(new DateOnly(2012, 4, 25), actual.DateOfData);
		Assert.Equal(new DateOnly(2012, 4, 25).DayOfWeek, actual.DayOfWeek);
		Assert.Equal(new int?[] { 46, 22, 14, 13, 29, 30, 75, 136, 179, 218, 264, 293, 322, 401, 439, null, null, 366, 261, 202, 143, 98, 54, 22 }, actual.VolumeCounted);
		Assert.Equal(Restrictions.None, actual.Restrictions);
	}

	[Fact]
	public void TestVolumeSiteCombinedLanesAndDirectionsWriting()
	{
		// VOLUME SITE (4 LANE SITE WITH ALL LANES AND DIRECTIONS COMBINED) STATION FILE
		var formatter = new HourlyTrafficVolumeFormatter();
		var hourlyTrafficVolume = new HourlyTrafficVolume()
		{
			StateCode = State.Illinois,
			FunctionalPurpose = FunctionalPurpose.PrincipalArterial,
			FunctionalTypeCode = FunctionalType.Rural,
			StationID = "1710A",
			DirectionOfTravelCode = DirectionOfTravel.NSorNESWCombined,
			LaneOfTravelCode = 0,
			DateOfData = new DateOnly(2012, 4, 25),
			VolumeCounted = new int?[] { 46, 22, 14, 13, 29, 30, 75, 136, 179, 218, 264, 293, 322, 401, 439, null, null, 366, 261, 202, 143, 98, 54, 22 },
			Restrictions = Restrictions.None,
		};

		var expected = "3172R01710A90201204254000460002200014000130002900030000750013600179002180026400293003220040100439          003660026100202001430009800054000220";
		var actual = formatter.ToLine(hourlyTrafficVolume);

		Assert.Equal(expected, new string(actual));
	}

	[Fact]
	public void TestVolumeSiteCombinedLanesReading1()
	{
		// VOLUME SITE (4 LANES WITH ALL LANES AND DIRECTIONS COMBINED) VOLUME FILE
		var formatter = new HourlyTrafficVolumeFormatter();
		var actual = formatter.FromLine("3172R018130302012042540000500004000040000200001000010000500003000120001600019000190001900026000190002000015000190001400011000090001300004000020");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal(FunctionalPurpose.PrincipalArterial, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal("18130", actual.StationID);
		Assert.Equal(DirectionOfTravel.East, actual.DirectionOfTravelCode);
		Assert.Equal(0, actual.LaneOfTravelCode);
		Assert.Equal(new DateOnly(2012, 4, 25), actual.DateOfData);
		Assert.Equal(new DateOnly(2012, 4, 25).DayOfWeek, actual.DayOfWeek);
		Assert.Equal(new int?[] { 5, 4, 4, 2, 1, 1, 5, 3, 12, 16, 19, 19, 19, 26, 19, 20, 15, 19, 14, 11, 9, 13, 4, 2 }, actual.VolumeCounted);
		Assert.Equal(Restrictions.None, actual.Restrictions);
	}

	[Fact]
	public void TestVolumeSiteCombinedLanesWriting1()
	{
		// VOLUME SITE (4 LANE SITE WITH ALL LANES AND DIRECTIONS COMBINED) STATION FILE
		var formatter = new HourlyTrafficVolumeFormatter();
		var hourlyTrafficVolume = new HourlyTrafficVolume()
		{
			StateCode = State.Illinois,
			FunctionalPurpose = FunctionalPurpose.PrincipalArterial,
			FunctionalTypeCode = FunctionalType.Rural,
			StationID = "18130",
			DirectionOfTravelCode = DirectionOfTravel.East,
			LaneOfTravelCode = 0,
			DateOfData = new DateOnly(2012, 4, 25),
			VolumeCounted = new int?[] { 5, 4, 4, 2, 1, 1, 5, 3, 12, 16, 19, 19, 19, 26, 19, 20, 15, 19, 14, 11, 9, 13, 4, 2 },
			Restrictions = Restrictions.None,
		};

		var expected = "3172R018130302012042540000500004000040000200001000010000500003000120001600019000190001900026000190002000015000190001400011000090001300004000020";
		var actual = formatter.ToLine(hourlyTrafficVolume);

		Assert.Equal(expected, new string(actual));
	}

	[Fact]
	public void TestVolumeSiteCombinedLanesReading2()
	{
		// VOLUME SITE (4 LANES WITH ALL LANES AND DIRECTIONS COMBINED) VOLUME FILE
		var formatter = new HourlyTrafficVolumeFormatter();
		var actual = formatter.FromLine("3172R018130702012042540000800001000030000300004000000000000001000040000900019000150001400022000260001500019000110000500016000030001000006000040");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal(FunctionalPurpose.PrincipalArterial, actual.FunctionalPurpose);
		Assert.Equal(FunctionalType.Rural, actual.FunctionalTypeCode);
		Assert.Equal("18130", actual.StationID);
		Assert.Equal(DirectionOfTravel.West, actual.DirectionOfTravelCode);
		Assert.Equal(0, actual.LaneOfTravelCode);
		Assert.Equal(new DateOnly(2012, 4, 25), actual.DateOfData);
		Assert.Equal(new DateOnly(2012, 4, 25).DayOfWeek, actual.DayOfWeek);
		Assert.Equal(new int?[] { 8, 1, 3, 3, 4, 0, 0, 1, 4, 9, 19, 15, 14, 22, 26, 15, 19, 11, 5, 16, 3, 10, 6, 4 }, actual.VolumeCounted);
		Assert.Equal(Restrictions.None, actual.Restrictions);
	}

	[Fact]
	public void TestVolumeSiteCombinedLanesWriting2()
	{
		// VOLUME SITE (4 LANE SITE WITH ALL LANES AND DIRECTIONS COMBINED) STATION FILE
		var formatter = new HourlyTrafficVolumeFormatter();
		var hourlyTrafficVolume = new HourlyTrafficVolume()
		{
			StateCode = State.Illinois,
			FunctionalPurpose = FunctionalPurpose.PrincipalArterial,
			FunctionalTypeCode = FunctionalType.Rural,
			StationID = "18130",
			DirectionOfTravelCode = DirectionOfTravel.West,
			LaneOfTravelCode = 0,
			DateOfData = new DateOnly(2012, 4, 25),
			VolumeCounted = new int?[] { 8, 1, 3, 3, 4, 0, 0, 1, 4, 9, 19, 15, 14, 22, 26, 15, 19, 11, 5, 16, 3, 10, 6, 4 },
			Restrictions = Restrictions.None,
		};

		var expected = "3172R018130702012042540000800001000030000300004000000000000001000040000900019000150001400022000260001500019000110000500016000030001000006000040";
		var actual = formatter.ToLine(hourlyTrafficVolume);

		Assert.Equal(expected, new string(actual));
	}
}
