using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;
using System.Runtime.Serialization;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestSpeedDataFormatter
{
	[Fact]
	public void TestVolumeSiteCombinedLanesAndDirectionsReading()
	{
		// SPEED SITE (15 BIN, 2 LANES WITH LANES OR DIRECTIONS NOT COMBINED AT 5-MINUTE INTERVAL FOR 1 HOUR) SPEED FILE
		var formatter = new SpeedDataFormatter();
		var actual = formatter.FromLine("T17018114112012062000A 1500375000000000000000000000000000000000070001200048001650008600031000210000500000                                                  ");

		Assert.Equal(State.Illinois, actual.StateCode);
		Assert.Equal("18114", actual.StationID);
		Assert.Equal(DirectionOfTravel.North, actual.DirectionOfTravelCode);
		Assert.Equal(1, actual.LaneOfTravelCode);
		Assert.Equal(new DateTime(2012, 6, 20, 0, 0, 0), actual.DateOfData);
		Assert.Equal(SpeedDataTimeInterval.CodeA, actual.TimeInterval);
		Assert.Null(actual.DefinitionOfFirstSpeedBin);
		Assert.Equal(15, actual.NumberOfSpeedBins);
		Assert.Equal(375, actual.TotalIntervalVolume);
		Assert.Equal(new int?[] { 0, 0, 0, 0, 0, 0, 7, 12, 48, 165, 86, 31, 21, 5, 0, null, null, null, null, null, null, null, null, null, null }, actual.SpeedBins);
	}

	[Fact]
	public void TestVolumeSiteCombinedLanesAndDirectionsWriting()
	{
		// SPEED SITE (15 BIN, 2 LANES WITH LANES OR DIRECTIONS NOT COMBINED AT 5-MINUTE INTERVAL FOR 1 HOUR) SPEED FILE
		var formatter = new SpeedDataFormatter();
		var speedData = new SpeedData()
		{
			StateCode = State.Illinois,
			StationID = "18114",
			DirectionOfTravelCode = DirectionOfTravel.North,
			LaneOfTravelCode = 1,
			DateOfData = new DateTime(2012, 6, 20, 0, 0, 0),
			TimeInterval = SpeedDataTimeInterval.CodeA,
			DefinitionOfFirstSpeedBin = null,
			NumberOfSpeedBins = 15,
			TotalIntervalVolume = 375,
			SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 7, 12, 48, 165, 86, 31, 21, 5, 0, null, null, null, null, null, null, null, null, null, null },
		};

		var expected = "T17018114112012062000A 1500375000000000000000000000000000000000070001200048001650008600031000210000500000                                                  ";
		var actual = formatter.ToLine(speedData);

		Assert.Equal(expected, new string(actual));
	}
}
