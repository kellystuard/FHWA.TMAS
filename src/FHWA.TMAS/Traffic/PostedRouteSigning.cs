namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Posted Route Signing
/// </summary>
/// <remarks>
/// This is the same as Route Signing in HPMS Field Manual.
/// </remarks>
public enum PostedRouteSigning
{
	/// <summary>Not signed</summary>
	NotSigned = 1,
	/// <summary>Interstate</summary>
	Interstate = 2,
	/// <summary>U.S.</summary>
	US = 3,
	/// <summary>State</summary>
	State = 4,
	/// <summary>Off-Interstate Business Marker</summary>
	BusinessMarker = 5,
	/// <summary>County</summary>
	County = 6,
	/// <summary>Township</summary>
	Township = 7,
	/// <summary>Municipal</summary>
	Municipal = 8,
	/// <summary>Parkway Marker or Forest Route Marker</summary>
	Parkway = 9,
	/// <summary>None of the above</summary>
	None = 10,
}