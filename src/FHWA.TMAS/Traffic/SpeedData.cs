using System.Collections.Immutable;

namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Holds the fields that represent TMAS traffic speed data. Represents the hydrated form of the record 
/// written to disk.
/// </summary>
/// <remarks>
/// <para>
/// The speed data file format is a variable length record used to report the number of vehicles traveling
/// in specified 5 mph speed bins during specified time periods. Each record can contain 1 hour of data,
/// 15 minutes of data, or 5 minutes of data. The submitting State chooses the time interval for which
/// data are being reported and indicates that time interval as a field in the record.
/// </para>
/// <para>
/// To submit data to FHWA, the speed data format must have a minimum of 15 bins up to a maximum
/// of 25 bins and should supply data in 5 mph speed bins defined as being required by FHWA. Any speed
/// data records that do not meet these specifications are purged by the TMAS software. All records
/// should follow the record formats defined in the TMG. In addition to the minimum and maximum
/// number of bins, a State may choose to report data in additional speed bins (see next paragraph).
/// When submitting data only using the minimum number of speed bins(15), the first speed bin
/// includes all vehicles traveling 20 mph or slower. The second speed bin is then defined as all vehicles
/// traveling faster than 20 mph but less than or equal to 25 mph. The last of the fifteen speed bins is
/// defined as all vehicles traveling faster than 85 mph.
/// </para>
/// <para>
/// If they desire, States may submit one or two additional speed bins for slow traveling vehicles. They
/// may create one additional slow speed bin (for vehicles traveling 15 mph or slower), or two slow
/// speed bins (one for vehicles traveling 10 mph or slower, and the other for vehicles traveling greater
/// than 10 mph up to 15 mph.) Similarly, a State may create additional high speed bins. Up to eight
/// additional bins may be added to provide more detail on high speed travel. The number of additional
/// high speed bins being reported should be indicated on the speed record. Finally, when additional high
/// speed bins are reported, the length of the speed record changes.
/// </para>
/// </remarks>
public readonly record struct SpeedData : ITrafficData
{
	/// <summary>
	/// FIPS State Codes (Columns 2-3) – Critical
	/// </summary>
	public Fips.State StateCode { get; init; }
	/// <summary>
	/// Station Identification (Columns 4-9) – Critical
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
	/// Direction of Travel Code (Column 10) – Critical
	/// </summary>
	/// <remarks>
	/// Combined directions are only permitted for volume stations only.There should be a separate
	/// record for each direction identified in Table 7-3. Whether or not lanes are combined in each
	/// direction depends on field #5, Lane of Travel.
	/// </remarks>
	public DirectionOfTravel DirectionOfTravelCode { get; init; }
	/// <summary>
	/// Lane of Travel (Column 11) – Critical
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
	/// Date of Data (Columns 12-15, 16-17, 18-19, and 20-21) - Critical
	/// </summary>
	/// <remarks>
	/// Code the beginning of the hour in which the count was taken.
	/// Covers 3 fields: Year of Data, Month of Data, Day, and Hour of Data.
	/// </remarks>
	public DateTime DateOfData { get; init; }
	/// <summary>
	/// Speed Data Time Interval (Column 22) – Optional
	/// </summary>
	/// <remarks>
	/// A 60-minute time interval is assumed if this column is left blank.This field can be used to
	/// designate either 5-minute or 15-minute binned speed data.
	/// </remarks>
	public SpeedDataTimeInterval? TimeInterval { get; init; }
	/// <summary>
	/// Definition of First Speed Bin (Column 23) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// If this field is left blank, the first speed bin is assumed to be defined as being all vehicles traveling
	/// equal to or slower than 20 mph.
	/// </para>
	/// <para>
	/// If the State wishes to submit data that provide more detail of vehicles traveling at slower speeds,
	/// it may supply data in one or two additional slow speed bins. If the value “1” is coded in Column
	/// 21 the first speed bin is defined as “all vehicles traveling at equal to or slower than 15 mph.” If
	/// the value “2” is coded in Column 21, the first speed bin submitted will contain “all vehicles
	/// traveling at equal to or slower than 10 mph” while the second bin will be defined as “all vehicles
	/// traveling faster than 10 mph and at equal to or slower than 15 mph.”
	/// </para>
	/// </remarks>
	public int? DefinitionOfFirstSpeedBin { get; init; }
	/// <summary>
	/// Total Number of Speed Bins Reported (Columns 24-25) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// If this field is left blank the record should contain data in 15 speed bins; the first bin is defined as
	/// all vehicles traveling 20 mph or slower, and the last bin is defined as all vehicles traveling faster
	/// than 85 mph. The length of the record for 15 speed bins is 105 columns wide.
	/// </para>
	/// <para>
	/// If the State is supplying data in additional speed bins, this value is used to determine the correct
	/// record length.
	/// </para>
	/// <para>
	/// When used in conjunction with Field 11 (Column 23) it is possible to determine the definition of
	/// all speed bins being submitted.
	/// </para>
	/// </remarks>
	public int? NumberOfSpeedBins { get; init; }
	/// <summary>
	/// Total Interval Volume (Columns 26-30) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// This numeric field is the total traffic volume for the interval being reported on this record. The
	/// total volume is needed because vehicle speed may not have been detected for all vehicles, in
	/// which case the sum of the speed counts would not equal the total volume. If the total volume is
	/// not collected, leave this field blank.
	/// </para>
	/// <para>
	/// The following speed count fields are numeric fields with the traffic volume by vehicle speed for
	/// each interval being reported. Traffic volumes in each speed bin for each reporting interval should
	/// be entered as zero-filled or blank-filled right justified integers in the appropriate columns.
	/// </para>
	/// </remarks>
	public int? TotalIntervalVolume { get; init; }
	/// <summary>
	/// Bin 1-25 Count (Columns 35-155) – 1-15: Critical / 16-25: Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// Due to programming language conventions, the first "Bin 1" is `SpeedBins[0]` and the last one is `SpeedBins[24]`.
	/// </para>
	/// <para>
	/// Bin 1 includes the number of vehicles in the slowest speed range being submitted. Right justify
	/// the integer volume number in the data entry field. The default condition for this speed bin is “all
	/// vehicles traveling equal to or slower than 20 mph.” If a different definition is used Field 11
	/// (Column 23) should be used to define this speed bin. If there are no vehicles observed in this
	/// speed range during the time period being reported, enter “0”, not “ “ (blank), to indicate that
	/// there are no vehicles in this speed range, and enter “0” similarly for each speed bin below.
	/// </para>
	/// <para>
	/// Bin 2-15 include the number of vehicles in the 2nd-15th slowest speed range being submitted. Right
	/// justify the integer volume number in the data entry field. If there are no vehicles observed in this
	/// speed range during the time period being reported, enter “0.”
	/// </para>
	/// <para>
	/// Bin 16-25 includes the number of vehicles in the 16th-20th speed range being submitted. It is used
	/// only when a State submits data in additional speed bins beyond the 15 required by FHWA. If this
	/// bin is used, Field #12 (Columns 24-25) should contain a value equal to or greater than 16. Right
	/// justify the integer volume number in the data entry field. If there are no vehicles observed in this
	/// speed range during the time period being reported, enter “0”.
	/// </para>
	/// </remarks>
	public IImmutableList<int?> SpeedBins { get; init; }
}
