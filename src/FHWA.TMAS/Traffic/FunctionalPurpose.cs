namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Column 16 contains one of the Functional Classification Codes listed in Table 7-5.
/// </summary>
public enum FunctionalPurpose
{
	/// <summary>Interstate</summary>
	Interstate = '1',
	/// <summary>Principal Arterial – Other Freeways and Expressways</summary>
	PrincipalArterial = '2',
	/// <summary>Principal Arterial – Other</summary>
	PrincipalArterialOther = '3',
	/// <summary>Minor Arterial</summary>
	MinorArterial = '4',
	/// <summary>Major Collector</summary>
	MajorCollector = '5',
	/// <summary>Minor Collector</summary>
	MinorCollector = '6',
	/// <summary>Local</summary>
	Local = '7',
}
