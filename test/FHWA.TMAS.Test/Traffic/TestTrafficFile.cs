using Fhwa.Tmas.Traffic.IO;

namespace Fhwa.Tmas.Test.Traffic;

public sealed class TestTrafficFile
{
	[Theory]
	[MemberData(nameof(DataFileNames))]
	public void TestFileNames(string fileName, Fips.State stateCode, string stationID, int dataMonth, int dataYear, string extension)
	{
		var trafficFile = new TrafficFile(stateCode, stationID, dataMonth, dataYear);
		var actual = trafficFile.GenerateFileName(extension);

		Assert.Equal(fileName, actual);
	}

	public static IEnumerable<object[]> DataFileNames()
	{
		yield return new object[] { "17000042032021.tst", Fips.State.Illinois, "42", 3, 2021, "tst" };
		yield return new object[] { "01000031041950.rst", Fips.State.Alabama, "31", 4, 1950, "rst" };
		yield return new object[] { "02123456111812.out", Fips.State.Alaska, "123456", 11, 1812, "out" };
		yield return new object[] { "31100000090990.ely", Fips.State.Nebraska, "100000", 09, 990, "ely" };
	}
}
