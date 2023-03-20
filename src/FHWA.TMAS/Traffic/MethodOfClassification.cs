namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Code the type of input and processing used to classify vehicles.
/// </summary>
public enum MethodOfClassification
{
	/// <summary>Human observation on site (manual)</summary>
	HumanOnSite = 'A',
	/// <summary>Human observation of vehicle image (e.g., video)</summary>
	HumanImage = 'B',
	/// <summary>Automated interpretation of vehicle image or signature (e.g., video, microwave, sonic, radar)</summary>
	AutomatedImageOrSignature = 'C',
	/// <summary>Vehicle length classification</summary>
	VehicleLength = 'D',
	/// <summary>Axle spacing with ASTM Standard E1572</summary>
	AxleSpacingE1572 = 'E',
	/// <summary>Axle spacing with FHWA 13 Vehicle Types</summary>
	AxleSpacingFHWA13 = 'F',
	/// <summary>Axle spacing with FHWA 13 Vehicle Types (modified)</summary>
	AxleSpacingFHWA13Modified = 'G',
	/// <summary>Six categories matching the HPMS vehicle summary table requirements (See TMG Chapter 6, Section 6.2)</summary>
	SixCategories = 'H',
	/// <summary> = Inductive or Magnetic Signature (PVF data only)</summary>
	InductiveOrMagnetic = 'I',
	/// <summary>Axle spacing and weight method</summary>
	AxleSpacingWeight = 'K',
	/// <summary>Axle spacing and vehicle length method</summary>
	AxleSpacingLength = 'L',
	/// <summary>Axle spacing, weight, and vehicle length method</summary>
	AxleSpacingWeightLength = 'M',
	/// <summary>Axle spacing and other input(s) not specified above</summary>
	AxleSpacingOther = 'N',
	/// <summary>Other axle spacing method</summary>
	OtherAxleMethod = 'O',
	/// <summary>LTPP classification algorithm (The details of this scheme can be obtained in the report Verification, Refinement, and Applicability of LTPP Classification Scheme.)</summary>
	LTPP = 'R',
	/// <summary>State specific algorithm</summary>
	StateSpecific = 'S',
	/// <summary>Vendor default algorithm</summary>
	Vendor = 'V',
	/// <summary>Other means not specified above</summary>
	Other = 'Z',
}