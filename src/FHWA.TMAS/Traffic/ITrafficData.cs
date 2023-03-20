namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Defines the minimal functionality necessary to describe traffic data that can be serialized to TMAS specifications.
/// </summary>
public interface ITrafficData
{
	/// <summary>
	/// For station description records, the value is 'S'.
	/// </summary>
	static abstract char RecordType { get; }
}