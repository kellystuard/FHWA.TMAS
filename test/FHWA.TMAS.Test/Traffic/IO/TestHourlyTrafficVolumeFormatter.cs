using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;
using System.Collections;
using System.Collections.Immutable;
using System.Runtime.Serialization;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestHourlyTrafficVolumeFormatter
{
	[Theory]
	[MemberData(nameof(DataHourlyTrafficVolume))]
	public void TestHourlyTrafficVolumeReading(string line, in HourlyTrafficVolume expected)
	{
		var formatter = new HourlyTrafficVolumeFormatter();
		var actual = formatter.FromLine(line);

		Assert.Equal(expected, actual);
	}

	[Theory]
	[MemberData(nameof(DataHourlyTrafficVolume))]
	public void TestHourlyTrafficVolumeWriting(string expected, in HourlyTrafficVolume hourlyTrafficVolume)
	{
		var formatter = new HourlyTrafficVolumeFormatter();
		var actual = formatter.ToLine(hourlyTrafficVolume);

		Assert.Equal(expected, new string(actual));
	}

	public static IEnumerable<object[]> DataHourlyTrafficVolume()
	{
		// VOLUME SITE (4 LANES WITH ALL LANES AND DIRECTIONS COMBINED) VOLUME FILE
		yield return new object[]
		{
			"3172R01710A90201204254000460002200014000130002900030000750013600179002180026400293003220040100439          003660026100202001430009800054000220",
			new HourlyTrafficVolume()
			{
				StateCode = State.Illinois,
				FunctionalPurpose = FunctionalPurpose.PrincipalArterial,
				FunctionalTypeCode = FunctionalType.Rural,
				StationID = "1710A",
				DirectionOfTravelCode = DirectionOfTravel.NSorNESWCombined,
				LaneOfTravelCode = 0,
				DateOfData = new DateOnly(2012, 4, 25),
				VolumeCounted = new int?[] { 46, 22, 14, 13, 29, 30, 75, 136, 179, 218, 264, 293, 322, 401, 439, null, null, 366, 261, 202, 143, 98, 54, 22 }
					.ToImmutableArray().WithValueSemantics(),
				Restrictions = Restrictions.None,
			},
		};

		// VOLUME SITE (4 LANES WITH ONLY LANES COMBINED) VOLUME FILE (line 1)
		yield return new object[]
		{
			"3172R018130302012042540000500004000040000200001000010000500003000120001600019000190001900026000190002000015000190001400011000090001300004000020",
			new HourlyTrafficVolume()
			{
				StateCode = State.Illinois,
				FunctionalPurpose = FunctionalPurpose.PrincipalArterial,
				FunctionalTypeCode = FunctionalType.Rural,
				StationID = "18130",
				DirectionOfTravelCode = DirectionOfTravel.East,
				LaneOfTravelCode = 0,
				DateOfData = new DateOnly(2012, 4, 25),
				VolumeCounted = new int?[] { 5, 4, 4, 2, 1, 1, 5, 3, 12, 16, 19, 19, 19, 26, 19, 20, 15, 19, 14, 11, 9, 13, 4, 2 }
					.ToImmutableArray().WithValueSemantics(),
				Restrictions = Restrictions.None,
			},
		};

		// VOLUME SITE (4 LANES WITH ONLY LANES COMBINED) VOLUME FILE (line 2)
		yield return new object[]
		{
			"3172R018130702012042540000800001000030000300004000000000000001000040000900019000150001400022000260001500019000110000500016000030001000006000040",
			new HourlyTrafficVolume()
			{
				StateCode = State.Illinois,
				FunctionalPurpose = FunctionalPurpose.PrincipalArterial,
				FunctionalTypeCode = FunctionalType.Rural,
				StationID = "18130",
				DirectionOfTravelCode = DirectionOfTravel.West,
				LaneOfTravelCode = 0,
				DateOfData = new DateOnly(2012, 4, 25),
				VolumeCounted = new int?[] { 8, 1, 3, 3, 4, 0, 0, 1, 4, 9, 19, 15, 14, 22, 26, 15, 19, 11, 5, 16, 3, 10, 6, 4 }
					.ToImmutableArray().WithValueSemantics(),
				Restrictions = Restrictions.None,
			},
		};
	}
}
