namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Holds the fields that represent TMAS traffic volume data. Represents the hydrated form of the record 
/// written to disk.
/// </summary>
/// <remarks>
/// <para>
/// The Hourly Traffic Volume Record format is a fixed length, fixed field record. One record is used for
/// each calendar day for which traffic monitoring data are being submitted. Each record contains a field
/// for traffic volume occurring during each of the 24 hours of that day. Blank fill the columns used for
/// any hours during which no data are being reported. Table 7-9 summarizes the Hourly Traffic Volume
/// record.
/// </para>
/// All numeric fields should be right-justified and blank fill the columns for which no data are being
/// reported.
/// <para>
/// </para>
/// </remarks>
public readonly record struct HourlyTrafficVolume : ITrafficData
{
	/// <summary>
	/// FIPS State Codes (Columns 2-3) – Critical
	/// </summary>
	public Fips.State StateCode { get; init; }
	/// <summary>
	/// Functional Classification Code (Columns 4-5) – Critical
	/// </summary>
	/// <remarks>
	/// Column 4 contains one of the Functional Classification Codes listed in Table 7-5.
	/// </remarks>
	public FunctionalPurpose FunctionalPurpose { get; init; }
	/// <summary>
	/// Functional Classification Code (Columns 4-5) – Critical
	/// </summary>
	/// <remarks>
	/// Column 5 contains either an “R” for rural or “U” for urban.For example, a code of 2R indicates 
	/// a Rural Principal Arterial – Other Freeways and Expressways.
	/// </remarks>
	public FunctionalType FunctionalTypeCode { get; init; }
	/// <summary>
	/// Station Identification (Columns 6-11) – Critical
	/// </summary>
	/// <remarks>
	/// <para>
	/// This field should contain an alphanumeric designation for the station where the survey data are
	/// collected.Station identification field entries must be identical in all records for a given station.
	/// Differences in characters, including spaces, blanks, hyphens, etc., prevent proper match.
	/// </para>
	/// <para>
	/// Right justify the Station ID if it is less than 6 characters.This field should be right-justified with
	/// unused columns zero-filled. This field can only be longer than 6 characters if the ASCII pipe
	/// format is used for all fields on the record.
	/// </para>
	/// </remarks>
	public string StationID { get; init; }
	/// <summary>
	/// Direction of Travel Code (Column 12) – Critical
	/// </summary>
	/// <remarks>
	/// Combined directions are only permitted for volume stations only.There should be a separate
	/// record for each direction identified in Table 7-3. Whether or not lanes are combined in each
	/// direction depends on field #5, Lane of Travel.
	/// </remarks>
	public DirectionOfTravel DirectionOfTravelCode { get; init; }
	/// <summary>
	/// Lane of Travel (Column 13) – Critical
	/// </summary>
	/// <remarks>
	/// <para>
	/// Either each lane is considered a separate station or all lanes in each direction are combined.
	/// </para>
	/// <para>
	/// All data for volume, class, and speed should be reported with the same resolution of
	/// lane/direction or lanes combined/direction to be consistent in the station record for all data
	/// types submitted.All data in either weight or PVF must be submitted by individual lane and by
	/// individual direction.
	/// </para>
	/// </remarks>
	public int LaneOfTravelCode { get; init; }
	/// <summary>
	/// Date of Data (Columns 14-17, 18-19, and 20-21)
	/// </summary>
	/// <remarks>
	/// Covers 3 fields: Year of Data, Month of Data, and Day of Data.
	/// </remarks>
	public DateOnly DateOfData { get; init; }
	/// <summary>
	/// Day of Week (Column 22) – Critical
	/// </summary>
	public DayOfWeek DayOfWeek { get => DateOfData.DayOfWeek; }

	/// <summary>
	/// Traffic Volume Counted Fields (Columns 23-27, ..., 138-142) - Optional
	/// </summary>
	/// <remarks>
	/// Enter the traffic volume counted during the hour covered in the columns indicated in Table 7-10.
	/// </remarks>
	public int?[] VolumeCounted { get; init; }
	/// <summary>
	/// Restrictions (Column 143) - Critical
	/// </summary>
	public Restrictions Restrictions { get; init; }
}
