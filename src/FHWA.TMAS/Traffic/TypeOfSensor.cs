namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Code the type of sensor used for traffic detection.
/// </summary>
public enum TypeOfSensor
{
	/// <summary>Automatic vehicle identification (AVI)</summary>
	AVI = 'A',
	/// <summary>Bending plate</summary>
	BendingPlate = 'B',
	/// <summary>Capacitance strip</summary>
	CapacitanceStrip = 'C',
	/// <summary>Capacitance mat/pad</summary>
	CapacitanceMat = 'D',
	/// <summary>Load cells (hydraulic or mechanical)</summary>
	LoadCells = 'E',
	/// <summary>Fiber optic</summary>
	FiberOptic = 'F',
	/// <summary>Strain gauge on bridge beam</summary>
	StrainGaugeBridge = 'G',
	/// <summary>Human observation (manual)</summary>
	Manual = 'H',
	/// <summary>Infrared</summary>
	Infrared = 'I',
	/// <summary>In - line strain gage load cell</summary>
	StrainGaugeInline = 'J',
	/// <summary>Laser / Lidar</summary>
	Laser = 'K',
	/// <summary>Inductive loop</summary>
	InductiveLoop = 'L',
	/// <summary>Magnetometer</summary>
	Magnetometer = 'M',
	/// <summary>Piezoelectric</summary>
	Piezoelectric = 'P',
	/// <summary>Quartz piezoelectric</summary>
	QuartzPiezoelectric = 'Q',
	/// <summary>Road tube</summary>
	RoadTube = 'R',
	/// <summary>Sonic / acoustic</summary>
	Sonic = 'S',
	/// <summary>Tape switch</summary>
	TapeSwitch = 'T',
	/// <summary>Ultrasonic</summary>
	Ultrasonic = 'U',
	/// <summary>Video image</summary>
	Video = 'V',
	/// <summary>Microwave(radar)</summary>
	Microwave = 'W',
	/// <summary>Radio wave (radar)</summary>
	Radar = 'X',
	/// <summary>Other</summary>
	Other = 'Z',
}