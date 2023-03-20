namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Mechanism of Vehicle Classification and/or speed
/// </summary>
public enum MechanismOfClassificationOrSpeed
{
	///<summary>Human observation (manual) vehicle classification</summary>
	HumanObservation = '1',
	///<summary>Portable vehicle classification device</summary>
	PortableDevice = '2',
	///<summary>Permanent vehicle classification device</summary>
	PermanentDevice = '3',
	///<summary>Speed only site</summary>
	SpeedOnly = '4',
}