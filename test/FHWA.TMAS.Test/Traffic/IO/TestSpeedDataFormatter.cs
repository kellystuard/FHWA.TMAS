using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;
using System.Collections.Immutable;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestSpeedDataFormatter
{
	[Theory]
	[MemberData(nameof(DataSpeedData))]
	public void TestSpeedDataReading(string line, in SpeedData expected)
	{
		var formatter = new SpeedDataFormatter();
		var actual = formatter.FromLine(line);

		Assert.Equal(expected, actual);
	}

	[Theory]
	[MemberData(nameof(DataSpeedData))]
	public void TestSpeedDataWriting(string expected, in SpeedData SpeedData)
	{
		var formatter = new SpeedDataFormatter();
		var actual = formatter.ToLine(SpeedData);

		Assert.Equal(expected, new string(actual));
	}

	public static IEnumerable<object[]> DataSpeedData()
	{
		yield return new object[]
		{
			"T17018114112012062000A 1500375000000000000000000000000000000000070001200048001650008600031000210000500000                                                  ",
			new SpeedData()
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
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 7, 12, 48, 165, 86, 31, 21, 5, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};
	}
}
