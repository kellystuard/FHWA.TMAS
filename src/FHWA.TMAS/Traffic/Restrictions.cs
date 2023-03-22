namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Restrictions (Column 143) - Critical
/// </summary>
public enum Restrictions
{
	/// <summary>No restrictions.</summary>
	None = '0',
	/// <summary>Construction or other activity affected traffic flow, traffic pattern not impacted.</summary>
	ConstructionNoImpact = '1',
	/// <summary>Traffic counting device problem (e.g., malfunction or overflow).</summary>
	DeviceProblem = '2',
	/// <summary>Weather affected traffic flow, traffic pattern not impacted.</summary>
	WeatherNoImpact = '3',
	/// <summary>Construction or other activity affected traffic flow, traffic pattern impacted.</summary>
	ConstructionImpacted = '4',
	/// <summary>Weather affected traffic flow, traffic pattern impacted.</summary>
	WeatherImpacted = '5',
}
