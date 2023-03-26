using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;
using System.Collections.Immutable;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestClassificationDataFormatter
{
	[Theory]
	[MemberData(nameof(DataClassificationData))]
	public void TestClassificationDataReading(string line, in ClassificationData expected, ClassificationGroupings classificationGroupings)
	{
		var formatter = new ClassificationDataFormatter(classificationGroupings);
		var actual = formatter.FromLine(line);

		Assert.Equal(expected, actual);
	}

	[Theory]
	[MemberData(nameof(DataClassificationData))]
	public void TestClassificationDataWriting(string expected, in ClassificationData ClassificationData, ClassificationGroupings classificationGroupings)
	{
		var formatter = new ClassificationDataFormatter(classificationGroupings);
		var actual = formatter.ToLine(ClassificationData);

		Assert.Equal(expected, new string(actual));
	}

	public static IEnumerable<object[]> DataClassificationData()
	{
		// LENGTH CLASSIFICATION(3 LENGTH CLASS BINS, 4 LANES WITH LANES AND DIRECTIONS NOT COMBINED AT 60 - MINUTE INTERVAL FOR 2 HOURS) SITE CLASSIFICATION FILE
		var common = new ClassificationData()
		{
			StateCode = State.Illinois,
			StationID = "1811B",
			DateOfData = new DateTime(2012, 04, 25, 0, 0, 0),
			TimeInterval = null,
			Restrictions = Restrictions.None,
		};

		yield return new object[]
		{
			"C1701811B112012042500 000990000510004800010                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				LaneOfTravelCode = 1,
				TotalIntervalVolume = 99,
				ClassBins = new int?[] { 51, 48, 10, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		yield return new object[]
		{
			"C1701811B122012042500 000200000050001400001                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				LaneOfTravelCode = 2,
				TotalIntervalVolume = 20,
				ClassBins = new int?[] { 5, 14, 1, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		yield return new object[]
		{
			"C1701811B522012042500 000110000020000800001                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				LaneOfTravelCode = 2,
				TotalIntervalVolume = 11,
				ClassBins = new int?[] { 2, 8, 1, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		yield return new object[]
		{
			"C1701811B512012042500 000660000230003700006                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				LaneOfTravelCode = 1,
				TotalIntervalVolume = 66,
				ClassBins = new int?[] { 23, 37, 6, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		yield return new object[]
		{
			"C1701811B112012042501 000720000420002100009                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				LaneOfTravelCode = 1,
				DateOfData = common.DateOfData.AddHours(1),
				TotalIntervalVolume = 72,
				ClassBins = new int?[] { 42, 21, 9, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		common = common with
		{
			DateOfData = common.DateOfData.AddHours(1),
		};

		yield return new object[]
		{
			"C1701811B122012042501 000170000050001100001                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.North,
				LaneOfTravelCode = 2,
				TotalIntervalVolume = 17,
				ClassBins = new int?[] { 5, 11, 1, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		yield return new object[]
		{
			"C1701811B522012042501 000110000020000700002                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				LaneOfTravelCode = 2,
				TotalIntervalVolume = 11,
				ClassBins = new int?[] { 2, 7, 2, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		yield return new object[]
		{
			"C1701811B512012042501 000570000210002900007                                                  ",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.South,
				LaneOfTravelCode = 1,
				TotalIntervalVolume = 57,
				ClassBins = new int?[] { 21, 29, 7, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Three,
		};

		// AXLE CLASSIFICATION (13 AXLE CLASS BINS, 2 LANES WITH LANES OR DIRECTIONS NOT COMBINED AT 15-MINUTE INTERVAL FOR 1 HOUR) SITE CLASSIFICATION FILE
		common = common with
		{
			StationID = "18140",
			LaneOfTravelCode = 1,
			DateOfData = new DateTime(2012, 12, 1, 0, 0, 0),
		};

		yield return new object[]
		{
			"C17018140312012120100100054000000000370000600000000010000000000000020000700000000000000000001",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.East,
				TimeInterval = TimeInterval.Code1,
				TotalIntervalVolume = 54,
				ClassBins = new int?[] { 0, 37, 6, 0, 1, 0, 0, 2, 7, 0, 0, 0, 1 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};

		yield return new object[]
		{
			"C17018140712012120100100055000001000380000900000000000000000000000030000500000000000000000000",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.West,
				TimeInterval = TimeInterval.Code1,
				TotalIntervalVolume = 55,
				ClassBins = new int?[] { 1, 38, 9, 0, 0, 0, 0, 3, 5, 0, 0, 0, 0 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};

		yield return new object[]
		{
			"C17018140312012120100200051000000000390000800000000010000100000000010000800000000000000000001",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.East,
				TimeInterval = TimeInterval.Code2,
				TotalIntervalVolume = 51,
				ClassBins = new int?[] { 0, 39, 8, 0, 1, 1, 0, 1, 8, 0, 0, 0, 1 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};

		yield return new object[]
		{
			"C17018140712012120100200058000000000370001000000000000000000000000020000900000000000000000000",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.West,
				TimeInterval = TimeInterval.Code2,
				TotalIntervalVolume = 58,
				ClassBins = new int?[] { 0, 37, 10, 0, 0, 0, 0, 2, 9, 0, 0, 0, 0 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};

		yield return new object[]
		{
			"C17018140312012120100300060000001000370000600000000000000100000000040001200001000000000000000",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.East,
				TimeInterval = TimeInterval.Code3,
				TotalIntervalVolume = 60,
				ClassBins = new int?[] { 1, 37, 6, 0, 0, 1, 0, 4, 12, 1, 0, 0, 0 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};

		yield return new object[]
		{
			"C17018140712012120100300067000000000360000500000000010000000000000000001500002000000000000000",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.West,
				TimeInterval = TimeInterval.Code3,
				TotalIntervalVolume = 67,
				ClassBins = new int?[] { 0, 36, 5, 0, 1, 0, 0, 0, 15, 2, 0, 0, 0 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};

		yield return new object[]
		{
			"C17018140312012120100400064000001000340000900001000010000000000000020001600000000010000000000",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.East,
				TimeInterval = TimeInterval.Code4,
				TotalIntervalVolume = 64,
				ClassBins = new int?[] { 1, 34, 9, 1, 1, 0, 0, 2, 16, 0, 1, 0, 0 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};

		yield return new object[]
		{
			"C17018140712012120100400063000000000380000800000000000000000000000030001300000000000000000001",
			common with
			{
				DirectionOfTravelCode = DirectionOfTravel.West,
				TimeInterval = TimeInterval.Code4,
				TotalIntervalVolume = 63,
				ClassBins = new int?[] { 0, 38, 8, 0, 0, 0, 0, 3, 13, 0, 0, 0, 1 }
					.ToImmutableArray().WithValueSemantics(),
			},
			ClassificationGroupings.Thirteen,
		};
	}
}
