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
		var common = new SpeedData()
		{
			StateCode = State.Illinois,
			StationID = "18114",
			LaneOfTravelCode = 1,
			DateOfData = new DateTime(2012, 6, 20, 0, 0, 0),
			NumberOfSpeedBins = 15,
		};

		// SPEED SITE (15 BIN, 2 LANES WITH LANES OR DIRECTIONS NOT COMBINED AT 5-MINUTE INTERVAL FOR 1 HOUR) SPEED FILE
		yield return new object[]
		{
			"T17018114112012062000A 1500375000000000000000000000000000000000070001200048001650008600031000210000500000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeA,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 375,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 7, 12, 48, 165, 86, 31, 21, 5, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000B 1500305000000000000000000000000000004000050000800025001430007900025000130000300000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeB,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 305,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 4, 5, 8, 25, 143, 79, 25, 13, 3, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000C 1500266000000000000000000000000000000000030000300032001310005800026000110000100001                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeC,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 266,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 3, 3, 32, 131, 58, 26, 11, 1, 1, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000D 1500268000000000000000000000000000000000020000400037001280004300039000120000300000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeD,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 268,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 2, 4, 37, 128, 43, 39, 12, 3, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000E 1500248000000000000000000000000000001000010000300039001190003900028000150000400000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeE,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 248,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 1, 1, 3, 39, 119, 39, 28, 15, 4, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000F 1500231000000000000000000000000000000000020000200025001080004200032000150000500000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeF,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 231,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 2, 2, 25, 108, 42, 32, 15, 5, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000G 1500197000000000000000000000000000000000000000100018000990003500028000110000500000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeG,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 197,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 1, 18, 99, 35, 28, 11, 5, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000H 1500183000000000000000000000000000000000000000000013000980003200027000090000400000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeH,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 183,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 13, 98, 32, 27, 9, 4, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000I 1500173000000000000000000000000000000000000000000008000950003100028000080000300000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeI,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 173,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 8, 95, 31, 28, 8, 3, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000J 1500159000000000000000000000000000000000000000000007000920002800025000050000200000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeJ,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 159,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 7, 92, 28, 25, 5, 2, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000K 1500154000000000000000000000000000000000000000000011000880002500026000030000100000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeK,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 154,
				SpeedBins = new int?[] {0, 0, 0, 0, 0, 0, 0, 0, 11, 88, 25, 26, 3, 1, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114112012062000L 1500145000000000000000000000000000000000000000000012000840002100024000040000000000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = SpeedDataTimeInterval.CodeL,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 145,
				SpeedBins = new int?[] {0, 0, 0, 0, 0, 0, 0, 0, 12, 84, 21, 24, 4, 0, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000A 1500427000000000000000000000000000000000080001300049000156000870003200022000060000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeA,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 427,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 8, 13, 49, 15, 60008, 70003, 20002, 20000, 60000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000B 1500307000000000000000000000000000000000060000900026000141000820002700011000050000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeB,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 307,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 6, 9, 26, 14, 10008, 20002, 70001, 10000, 50000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000C 1500268000000000000000000000000000000000020000300031000128000610002500013000040001                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeC,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 268,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 2, 3, 31, 12, 80006, 10002, 50001, 30000, 40001, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000D 1500294000000000000000000000000000000000000000100035000134000650003600015000070001                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeD,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 294,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 1, 35, 13, 40006, 50003, 60001, 50000, 70001, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000E 1500277000000000000000000000000000000000000000700031000121000580003100021000080000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeE,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 277,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 7, 31, 12, 10005, 80003, 10002, 10000, 80000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000F 1500288000000000000000000000000000000000000000200026000115000550003200022000070001                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeF,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 288,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 2, 26, 11, 50005, 50003, 20002, 20000, 70001, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000G 1500272000000000000000000000000000000000000000400027000122000560003500023000050000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeG,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 272,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 4, 27, 12, 20005, 60003, 50002, 30000, 50000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000H 1500242000000000000000000000000000000000000000300021000114000490003300018000040000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeH,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 242,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 3, 21, 11, 40004, 90003, 30001, 80000, 40000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000I 1500212000000000000000000000000000000000000000000015000104000480002400019000020000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeI,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 212,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 15, 10, 40004, 80002, 40001, 90000, 20000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000J 1500198000000000000000000000000000000000000000000012000098000470002500013000030000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeJ,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 198,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 12, 9, 80004, 70002, 50001, 30000, 30000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000K 1500185000000000000000000000000000000000000000000008000087000510002400012000030000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeK,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 185,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 8, 8, 70005, 10002, 40001, 20000, 30000, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000L 1500155000000000000000000000000000000000000000000005000076000440002100009000000000                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = SpeedDataTimeInterval.CodeL,
				DefinitionOfFirstSpeedBin = null,
				TotalIntervalVolume = 155,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 5, 7, 60004, 40002, 10000, 90000, 0, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		// SPEED SITE(21 BIN, 4 LANES WITH LANES OR DIRECTIONS NOT COMBINED AT 60 - MINUTE INTERVAL FOR 1 HOUR, TOTAL VOLUME NOT RECORDED) SPEED FILE
		yield return new object[]
		{
			"T17018114112012062000 121     000000000000000000000000000000000000000000005000340005400021000150000200001000000000000000000000000000000                    ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				TimeInterval = null,
				DefinitionOfFirstSpeedBin = 1,
				NumberOfSpeedBins = 21,
				TotalIntervalVolume = null,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 5, 34, 54, 21, 15, 2, 1, 0, 0, 0, 0, 0, 0, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114122012062000 121     000000000000000000000000000000000000000000007000300004600024000130000300002000000000000000000000000000000                    ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				LaneOfTravelCode = 2,
				TimeInterval = null,
				DefinitionOfFirstSpeedBin = 1,
				NumberOfSpeedBins = 21,
				TotalIntervalVolume = null,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 7, 30, 46, 24, 13, 3, 2, 0, 0, 0, 0, 0, 0, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114522012062000 121     000000000000000000000000000000000000000000010000280005600031000210000500004000030000000000000000000000000                    ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				LaneOfTravelCode = 2,
				TimeInterval = null,
				DefinitionOfFirstSpeedBin = 1,
				NumberOfSpeedBins = 21,
				TotalIntervalVolume = null,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 10, 28, 56, 31, 21, 5, 4, 3, 0, 0, 0, 0, 0, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114512012062000 121     000000000000000000000000000000000000000000008000260003600029000180000300003000020000100000000000000000000                    ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				TimeInterval = null,
				DefinitionOfFirstSpeedBin = 1,
				NumberOfSpeedBins = 21,
				TotalIntervalVolume = null,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 8, 26, 36, 29, 18, 3, 3, 2, 1, 0, 0, 0, 0, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		// SPEED SITE (25 BIN, 2 LANES WITH LANES OR DIRECTIONS NOT COMBINED AT 60-MINUTE INTERVAL FOR 1 HOUR) SPEED FILE
		yield return new object[]
		{
			"T17018114312012062000 2250068300000000000000000000000000000000000000000000000005000140002600056000860010900121000950008200042000210001500011000000000000000",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.East,
				TimeInterval = null,
				DefinitionOfFirstSpeedBin = 2,
				NumberOfSpeedBins = 25,
				TotalIntervalVolume = 683,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 14, 26, 56, 86, 109, 121, 95, 82, 42, 21, 15, 11, 0, 0, 0 }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"T17018114712012062000 2250068100000000000000000000000000000000000000000000000004000120002800064000850009800127000940008100041000180001300013000030000000000",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.West,
				TimeInterval = null,
				DefinitionOfFirstSpeedBin = 2,
				NumberOfSpeedBins = 25,
				TotalIntervalVolume = 681,
				SpeedBins = new int?[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 12, 28, 64, 85, 98, 127, 94, 81, 41, 18, 13, 13, 3, 0, 0 }
					.ToImmutableArray().WithValueSemantics(),
			},
		};
	}
}
