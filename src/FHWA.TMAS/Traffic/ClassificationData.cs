using System.Collections.Immutable;

namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Holds the fields that represent TMAS vehicle classification data. Represents the hydrated form of the record 
/// written to disk.
/// </summary>
/// <remarks>
/// <para>
/// The Vehicle Classification file is a variable length record. It contains one record for each time period
/// (e.g. by 5 min., 15 min., hour of the day) which data are being submitted. That record includes the
/// traffic volume by vehicle class data for that hour. The length of the record (number of columns in
/// each record) is determined by the value in Field 15 (columns 25-26) of the Station Description Record.
/// This means that if two different kinds of data collection equipment are used at a site and those
/// different pieces of equipment collect classification data in different formats (e.g., one uses length
/// classes and the other uses the 13-FHWA categories), then an updated State Description Record
/// should be submitted prior to submitting data using the second classification system or the records
/// being submitted will not be read correctly. All lanes in one direction should have the same data being
/// collected. FHWA uses the latest version of the State Description Record that is submitted. If this
/// record type already exists in TMAS, and no change in the equipment functionality (e.g., the type of
/// vehicle class data being collected) has occurred since that earlier record was submitted, it is not
/// necessary to submit an additional Station Description Record for the data to be processed in TMAS.
/// </para>
/// <para>
/// A single file can contain data from multiple stations and/or locations.
/// </para>
/// </remarks>
public readonly record struct ClassificationData : ITrafficData
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
	public TimeInterval? TimeInterval { get; init; }
	/// <summary>
	/// Total Interval Volume (Columns 23-27) – Critical
	/// </summary>
	/// <remarks>
	/// <para>
	/// This numeric field is the total traffic volume for the hour. The total volume is needed because the
	/// data collection process might not be able to classify some vehicles, in which case the sum of the
	/// vehicle class counts will not equal the total hourly volume.
	/// </para>
	/// <para>
	/// Fields 12 to 24:
	/// </para>
	/// <para>
	/// The following class count fields are numeric fields with the traffic volume by vehicle class for
	/// each hour of data. Field #15 in the Station Description Record, “Vehicle Classification Groupings,”
	/// determines the number of classes expected from this station. This value also determines how
	/// many columns are expected in the remainder of each record submitted in a given file. Truncate
	/// the vehicle classification record after the last classification field has been used. (That is, if only
	/// five vehicle classes are being reported, the record should only be 52 columns wide.)
	/// </para>
	/// <para>
	/// The default classification system is the FHWA 13 class system (see Appendix C). Where a
	/// classification(grouping) system other than FHWA’s 13-category system is used, the total number
	/// of columns for which data are entered will change from that described below. When no vehicles
	/// of a class being monitored are counted during a given hour, zero fill and right-justify the data in
	/// the columns associated with that class of vehicles.
	/// </para>
	/// <para>
	/// Before submittal to FHWA, these counts should be checked for reasonableness and quality
	/// controlled. When 13 vehicle types are reported, the Vehicle Classification record would not be
	/// larger than 92 columns, with Classes 1-13 (fields 12-24) all considered to be Critical. A
	/// relationship exists between Table 7-6 and the Vehicle Classes used. These need to be
	/// synchronized for proper processing in the TMAS software. TMAS allows users to set a limit for
	/// each class count as part of its automated quality assurance checks.
	/// </para>
	/// </remarks>
	public int? TotalIntervalVolume { get; init; }
	/// <summary>
	/// Restrictions (Column 143) - Critical
	/// </summary>
	public Restrictions Restrictions { get; init; }
	/// <summary>
	/// Bin 1-25 Count (Columns 29-93) – 1-2: Critical / 3-13: Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// Due to programming language conventions, the first "Bin 1" is `SpeedBins[0]` and the last one is `SpeedBins[13]`.
	/// </para>
	/// <para>
	/// Class 1 is for Motorcycles when using the 13 FHWA classification groups. If the FHWA 13-
	/// category system is not being used, this field will contain the traffic volume for the first class of
	/// vehicles being reported.
	/// </para>
	/// <para>
	/// Class 2 is for Passenger Cars when using the 13 FHWA classification groups. If the FHWA 13-
	/// category system is not being used, this field will contain the traffic volume for the second class of
	/// vehicles being reported.
	/// </para>
	/// <para>
	/// Class 3 is for Light Duty (2-axle, four-tire) Pick-up Trucks when using the 13 FHWA classification
	/// groups. If the FHWA 13-category system is not being used, this field will contain the traffic
	/// volume for the third class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 4 is for Buses when using the 13 FHWA classification groups. If the FHWA 13-category
	/// system is not being used, this field will contain the traffic volume for the fourth class of vehicles
	/// being reported.
	/// </para>
	/// <para>
	/// Class 5 is for Two-Axle, Six-Tire, Single-Unit Trucks when using the 13 FHWA classification groups.
	/// If the FHWA 13-category system is not being used, this field will contain the traffic volume for the
	/// fifth class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 6 is for Three-Axle, Single-Unit Trucks when using the 13 FHWA classification groups. If the
	/// FHWA 13-category system is not being used, this field will contain the traffic volume for the sixth
	/// class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 7 is for Four-or-More Axle, Single-Unit Trucks when using the 13 FHWA classification
	/// groups. If the FHWA 13-category system is not being used, this field will contain the traffic
	/// volume for the seventh class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 8 is for Four-or-Less Axle, Single-Trailer Trucks when using the 13 FHWA classification
	/// groups. If the FHWA 13-category system is not being used, this field will contain the traffic
	/// volume for the eighth class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 9 is for Five-Axle, Single-Trailer Trucks when using the 13 FHWA classification groups. If the
	/// FHWA 13-category system is not being used, this field will contain the traffic volume for the ninth
	/// class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 10 is for Six-or-More Axle, Single-Trailer Trucks when using the 13 FHWA classification
	/// groups. If the FHWA 13-category system is not being used, this field will contain the traffic
	/// volume for the tenth class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 11 is for Five-or-Less Axle, Multi-Trailer Trucks when using the 13 FHWA classification
	/// groups. If the FHWA 13-category system is not being used, this field will contain the traffic
	/// volume for the eleventh class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 12 is for Six-Axle, Multi-Trailer Trucks when using the 13 FHWA classification groups. If the
	/// FHWA 13-category system is not being used, this field will contain the traffic volume for the 12th
	/// class of vehicles being reported.
	/// </para>
	/// <para>
	/// Class 13 is for Seven-or-More Axle, Multi-Trailer Trucks when using the 13 FHWA classification
	/// groups. If the FHWA 13-category system is not being used, this field will contain the traffic
	/// volume for the 13th class of vehicles being reported.
	/// The vehicle classification record should be ended here if 13 classes are being reported. If volumes
	/// for additional classes of vehicles are being reported, add five additional columns for each
	/// additional vehicle class being reported. (These additional vehicle classes can include the vehicle
	/// categories of “Unclassified”, “Unclassifiable” vehicles that are reported by some types of
	/// equipment, or some state specific type of vehicle.)
	/// </para>
	/// </remarks>
	public IImmutableList<int?> ClassBins { get; init; }
}
