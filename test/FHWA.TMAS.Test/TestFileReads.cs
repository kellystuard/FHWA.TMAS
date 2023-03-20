using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;

namespace Fhwa.Tmas.Test;

public sealed class TestFileReads
{
	[Fact]
	public void TestReadVolumeSite4LaneCombinedStationFile()
	{
		//var reader = new TrafficReader<StationDescription, StationDescriptionFormatter>(path: "TrafficFiles",
		//	trafficFile: VolumeSite4LaneCombinedStationFile);
		//var writer = new TrafficWriter<StationDescription, StationDescriptionFormatter>(path: "TrafficFiles",
		//	trafficFile: VolumeSite4LaneCombinedStationFile) with
		//{
		//	DataMonth = VolumeSite4LaneCombinedStationFile.DataMonth + 1,
		//});

		//foreach (var station in reader.ReadAll())
		//	writer.Write(station);
	}

	//public async Task TestReadVolumeSite4LaneCombinedStationFile()
	//{ 
	//	var writer = new TrafficWriter<StationDescription>(path: "",
	//		trafficFile: VolumeSite4LaneCombinedStationFile with
	//		{
	//			DataMonth = VolumeSite4LaneCombinedStationFile.DataMonth + 1,
	//		}
	//	);

	//	while (true)
	//	{
	//		var station = reader.Read();
	//		if (station.HasValue() == false)
	//			break;

	//		writer.Write(station);
	//	}

	//	while (true)
	//	{
	//		var station = await reader.ReadAsync();
	//		if (station.HasValue == false)
	//			break;

	//		await writer.WriteAsync(station);
	//	}
	//}

	private static readonly TrafficFile VolumeSite4LaneCombinedStationFile = new(
		Fips.State.Illinois, "abcxyz", 1, 1950);
}