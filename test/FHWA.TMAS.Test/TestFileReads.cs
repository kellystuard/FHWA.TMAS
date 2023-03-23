using Fhwa.Tmas.Traffic;
using Fhwa.Tmas.Traffic.IO;

namespace Fhwa.Tmas.Test;

public sealed class TestFileReads
{
	[Fact]
	public void TestReadVolumeSite4LaneCombinedStationFile()
	{
		Directory.CreateDirectory("TrafficOutput");
		try
		{
			using (var reader = new TrafficReader<StationDescription, StationDescriptionFormatter>(path: "TrafficFiles",
				trafficFile: VolumeSite4LaneCombinedStationFile))
			using (var writer = new TrafficWriter<StationDescription, StationDescriptionFormatter>(path: "TrafficOutput",
				trafficFile: VolumeSite4LaneCombinedStationFile))
			{
				foreach (var station in reader.ReadAll())
					writer.WriteAll(new[] { station });
			}
			var fileName = VolumeSite4LaneCombinedStationFile.GenerateFileName(StationDescriptionFormatter.FileExtension);
			Assert.Equal(
				File.ReadAllText(Path.Combine("TrafficFiles", fileName)),
				File.ReadAllText(Path.Combine("TrafficOutput", fileName))
			);
		}
		finally
		{
			Directory.Delete("TrafficOutput", recursive: true);
		}
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