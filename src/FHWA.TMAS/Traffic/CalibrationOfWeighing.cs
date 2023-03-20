namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Code the method used to calibrate the weighing system, e.g., comparing weigh-in-motion and
/// weights from static scales.At a minimum, yearly calibration is recommended, and maybe more
/// often depending upon the site, sensors, equipment, and array used.
/// </summary>
public enum CalibrationOfWeighing
{
	/// <summary>ASTM Standard E1318</summary>
	E1318 = 'A',
	/// <summary>Subset of ASTM Standard E1318</summary>
	E1318Subset = 'B',
	/// <summary>Combination of test trucks and trucks from the traffic stream (but not ASTM E1318)</summary>
	TestTrucksAndTrafficTrucks = 'C',
	/// <summary>Other sample of trucks from the traffic stream</summary>
	OtherFromTraffic = 'D',
	/// <summary>Statistical average of the steering axle of class nines</summary>
	AxleAverage = 'M',
	/// <summary>LTPP Calibration Method</summary>
	LTPP = 'R',
	/// <summary>Static calibration</summary>
	StaticCalibration = 'S',
	/// <summary>Test trucks only</summary>
	TestOnly = 'T',
	/// <summary>Uncalibrated</summary>
	Uncalibrated = 'U',
	/// <summary>Other method</summary>
	Other = 'Z',
}