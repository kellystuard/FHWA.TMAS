using Fhwa.Tmas.Fips;
using Fhwa.Tmas.Traffic.IO;
using Fhwa.Tmas.Traffic;
using System.Collections.Immutable;

namespace Fhwa.Tmas.Test.Traffic.IO;

public sealed class TestWeightDataFormatter
{
	[Theory]
	[MemberData(nameof(DataWeightData))]
	public void TestWeightDataReading(string line, in WeightData expected)
	{
		var formatter = new WeightDataFormatter();
		var actual = formatter.FromLine(line);

		Assert.Equal(expected, actual);
	}

	[Theory]
	[MemberData(nameof(DataWeightData))]
	public void TestWeightDataWriting(string expected, in WeightData WeightData)
	{
		var formatter = new WeightDataFormatter();
		var actual = formatter.ToLine(WeightData);

		Assert.Equal(expected, new string(actual));
	}

	public static IEnumerable<object[]> DataWeightData()
	{
		var common = new WeightData()
		{
			StateCode = State.Illinois,
			StationID = "18115",
			DirectionOfTravelCode = DirectionOfTravel.East,
			LaneOfTravelCode = 1,
			DateOfData = new DateTime(2012, 11, 7, 16, 0, 0),
			Open = null,
		};

		yield return new object[]
		{
			"W1701811531201211071609   0578860511210015112300004513730021409815004810831                                                                        ",
			common with
			{
				VehicleClass = "09",
				TotalWeightOfVehicle = 57886,
				NumberOfAxles = 5,
				AxleWeights = new int?[] { 11210, 12300, 13730, 9815, 10831, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
				AxleSpacing = new int?[] { 151, 45, 214, 48, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"W1701811531201211071604   0183510208522025209829                                                                                                   ",
			common with
			{
				VehicleClass = "04",
				TotalWeightOfVehicle = 18351,
				NumberOfAxles = 2,
				AxleWeights = new int?[] { 8522, 9829, null, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
				AxleSpacing = new int?[] { 252, null, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};

		yield return new object[]
		{
			"W1701811531201211071606   0472890309818013191025004618346                                                                                          ",
			common with
			{
				VehicleClass = "06",
				TotalWeightOfVehicle = 47289,
				NumberOfAxles = 3,
				AxleWeights = new int?[] { 9818, 91025, 18346, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
				AxleSpacing = new int?[] { 131, 46, null, null, null, null, null, null, null, null, null, null }
					.ToImmutableArray().WithValueSemantics(),
			},
		};
	}
}
