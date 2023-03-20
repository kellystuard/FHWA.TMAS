namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Method of Traffic Volume Counting.
/// </summary>
public enum MethodOfVolumeCounting
{
	///<summary>Human observation (manual)</summary>
	Human = '1',
	///<summary>Portable traffic recording device</summary>
	PortableDevice = '2',
	///<summary>Permanent continuous count station (CCS) formerly ATR</summary>
	PermanentDevice = '3',
}