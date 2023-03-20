namespace Fhwa.Tmas.Traffic;

/// <summary>
/// This field indicates the primary purpose for installing the station and hence which organization is
/// responsible for it and supplies the data.
/// </summary>
public enum StationPurpose
{
	/// <summary>Enforcement purposes (e.g., speed or weight enforcement)</summary>
	Enforcement = 'E',
	/// <summary>Operations purposes in support of ITS initiatives</summary>
	OperationsITS = 'I',
	/// <summary>Load data for pavement design or pavement management purposes</summary>
	LoadData = 'L',
	/// <summary>Operations purposes but not ITS</summary>
	OperationsOther = 'O',
	/// <summary>Planning or traffic statistics including HPMS reporting purposes</summary>
	Planning = 'P',
	/// <summary>Research purposes (e.g., LTPP)</summary>
	Research = 'R',
}