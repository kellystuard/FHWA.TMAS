namespace Fhwa.Tmas.Traffic;

/// <summary>
/// This field can be used to
/// designate either 5-minute or 15-minute binned speed data.
/// </summary>
/// <remarks>
/// For 15-minute binned intervals of speed data use 1-4.
/// For 5-minute binned intervals of speed data use A-L.
/// </remarks>
public enum SpeedDataTimeInterval
{
	/// <summary>For the 15-minute interval 0.0-14.999</summary>
	Code1 = '1',
	/// <summary>For the 15-minute interval 15.0-29.999</summary>
	Code2 = '2',
	/// <summary>For the 15-minute interval 30.0-44.999</summary>
	Code3 = '3',
	/// <summary>For the 15-minute interval 45.0-59.999</summary>
	Code4 = '4',
	/// <summary>For the 5-minute interval 0.0-4.999</summary>
	CodeA = 'A',
	/// <summary>For the 5-minute interval 5.0-9.999</summary>
	CodeB = 'B',
	/// <summary>For the 5-minute interval 10.0-14.999</summary>
	CodeC = 'C',
	/// <summary>For the 5-minute interval 15.0-19.999</summary>
	CodeD = 'D',
	/// <summary>For the 5-minute interval 20.0-24.999</summary>
	CodeE = 'E',
	/// <summary>For the 5-minute interval 25.0-29.999</summary>
	CodeF = 'F',
	/// <summary>For the 5-minute interval 30.0-34.999</summary>
	CodeG = 'G',
	/// <summary>For the 5-minute interval 35.0-39.999</summary>
	CodeH = 'H',
	/// <summary>For the 5-minute interval 40.0-44.999</summary>
	CodeI = 'I',
	/// <summary>For the 5-minute interval 45.0-49.999</summary>
	CodeJ = 'J',
	/// <summary>For the 5-minute interval 50.0-54.999</summary>
	CodeK = 'K',
	/// <summary>For the 5-minute interval 55.0-59.999</summary>
	CodeL = 'L', 
}
