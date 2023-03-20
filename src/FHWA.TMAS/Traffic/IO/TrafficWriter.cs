using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Fhwa.Tmas.Traffic.IO;

/// <summary>
/// Responsible for writing traffic data of any type that implements <see cref="ITrafficData"/>.
/// </summary>
/// <typeparam name="T">Type of traffic data to write.</typeparam>
/// <typeparam name="U">Type to use for formatting traffic data.</typeparam>
public class TrafficWriter<T, U> : IDisposable
	where T : struct, ITrafficData
	where U : ITrafficFormatter<T>, new()
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TrafficWriter{T, U}"/> class.
	/// </summary>
	/// <param name="path">Folder path to write traffic files.</param>
	/// <param name="stateCode">FIPS State Code</param>
	/// <param name="stationID">
	/// This field should contain an alphanumeric designation for the station where the survey data are
	/// collected.Station identification field entries must be identical in all records for a given station.
	/// Differences in characters, including spaces, blanks, hyphens, etc., prevent proper match.
	/// Right justify the Station ID if it is less than 6 characters. This field should be right-justified with
	/// unused columns zero-filled.
	/// </param>
	/// <param name="dataMonth">Month for the records in the traffic file.</param>
	/// <param name="dataYear">Year for the records in the traffic file.</param>
	public TrafficWriter(string path, Fips.State stateCode, string stationID, int dataMonth, int dataYear)
		: this(path, new TrafficFile(stateCode, stationID, dataMonth, dataYear))
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TrafficWriter{T, U}"/> class.
	/// </summary>
	/// <param name="path">Folder path to write traffic files.</param>
	/// <param name="trafficFile">File-related information.</param>
	public TrafficWriter(string path, TrafficFile trafficFile)
	{
		FileName = Path.Combine(path, trafficFile.GenerateFileName(U.FileExtension));
		_streamWriter = new StreamWriter(
			File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.None),
			Encoding.ASCII
		);
	}

	/// <summary>
	/// Writes all traffic records to the file.
	/// </summary>
	/// <param name="items">Traffic records to write.</param>
	public void WriteAll(IEnumerable<T> items)
	{
		foreach (var item in items)
		{
			var line = _trafficFormatter.ToLine(item);
			_streamWriter.WriteLine(line);
		}
	}

	///// <summary>
	///// Writes all traffic records to the file, asynchronously.
	///// </summary>
	///// <param name="items">Traffic records to write.</param>
	///// <param name="cancellationToken">Token used to cancel the asynchronous task.</param>
	//public async Task WriteAllAsync(IAsyncEnumerable<T> items, CancellationToken cancellationToken = default)
	//{
	//	await foreach (var item in items)
	//	{
	//		var line = _trafficFormatter.ToLine(item);
	//		await _streamWriter.WriteLineAsync(line);
	//	}
	//}

	/// <inheritdoc />
	public string FileName { get; init; }

	private static readonly U _trafficFormatter = new();
	private readonly StreamWriter _streamWriter;
	private bool disposedValue;

	#region IDisposable

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting 
	/// unmanaged resources.
	/// </summary>
	/// <param name="disposing">Notates if being called from <see cref="Dispose()"/>.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// dispose managed state (managed objects)
			}

			// free unmanaged resources (unmanaged objects) and override finalize
			_streamWriter.Dispose();
			// set large fields to null
			disposedValue = true;
		}
	}

	// override finalize only if dispose frees unmanaged resources
	// ~TrafficReader()
	// {
	//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
	//     Dispose(disposing: false);
	// }

	/// <inheritdoc />
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	#endregion
}
