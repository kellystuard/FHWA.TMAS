namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Direction of Travel Code
/// </summary>
/// <remarks>
/// Combined directions are only permitted for volume stations only. There should be a separate
/// record for each direction identified. Whether or not lanes are combined in each
/// direction depends on field Lane of Travel.
/// </remarks>
public enum DirectionOfTravel
{
	/// <summary>North</summary>
	North = '1',
	/// <summary>Northeast</summary>
	Northeast = '2',
	/// <summary>East</summary>
	East = '3',
	/// <summary>Southeast</summary>
	Southeast = '4',
	/// <summary>South</summary>
	South = '5',
	/// <summary>Southwest</summary>
	Southwest = '6',
	/// <summary>West</summary>
	West = '7',
	/// <summary>Northwest</summary>
	Northwest = '8',
	/// <summary>North-South or Northeast-Southwest combined (volume stations only)</summary>
	NSorNESWCombined = '9',
	/// <summary>East-West or Southeast-Northwest combined (volume stations only)</summary>
	EWorSENWCombined = '0',
}
