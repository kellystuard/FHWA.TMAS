namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Method of Truck Weighing.
/// </summary>
public enum MethodOfTruckWeighing
{
	/// <summary>Portable static scale</summary>
	PortableScale = '1',
	/// <summary>Chassis-mounted, towed static scale</summary>
	TowedScale = '2',
	/// <summary>Platform or pit static scale</summary>
	PlatformScale = '3',
	/// <summary>Portable weigh-in-motion system</summary>
	PortableWIM = '4',
	/// <summary>Permanent weigh-in-motion system</summary>
	PermanentWIM = '5',
}