using System.Collections.Immutable;

namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Holds the fields that represent TMAS traffic weight data. Represents the hydrated form of the record 
/// written to disk.
/// </summary>
/// <remarks>
/// <para>
/// The Weight Data Format is the mechanism currently used to submit weight data to FHWA. FHWA
/// prefers that States submit weight data using the Per Vehicle Formats (PVF) described in Section 7.7.
/// In particular, weight data should be submitted using the “W” or “Z” variants of the PVF record format
/// (Sections 7.7.4 and 7.7.5, respectively).
/// </para>
/// <para>
/// If States still wish to use the current Weight Data Format, a description of that file format is as
/// follows. Each file submitted in this format contains one record for each vehicle. Each record describes
/// that vehicle’s axle weights and axle spacings.
/// </para>
/// <para>
/// As a reminder, all weight data are to use English units.
/// </para>
/// </remarks>
public readonly record struct WeightData : ITrafficData
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
	//TODO: Find a better way to handle this field
	/// <summary>
	/// Vehicle Class (Column 22-23) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// Enter the class of the vehicle from FHWA Vehicle Classes 1 to 13. (Note: vehicles from classes 1 -
	/// 3 are ordinarily omitted from weight data submittal.)
	/// </para>
	/// <para>
	/// A dummy vehicle class of ‘m’ indicates that weight data for this hour are missing. A dummy
	/// vehicle class of ‘d’ indicates that weight data for this hour are not missing, and thus if there are
	/// no weight records for the hour, then there were no trucks during that hour. Without these
	/// indications, no weight records for an hour might be interpreted to mean that the WIM system
	/// was not working.
	/// </para>
	/// </remarks>
	public string VehicleClass { get; init; }
	/// <summary>
	/// Open (Column 24-26) – Optional
	/// </summary>
	/// <remarks>
	/// This field is for special studies or State use such as for vehicle speed (miles per hour) or
	/// pavement temperature (degrees Fahrenheit).
	/// </remarks>
	public string? Open { get; init; }
	/// <summary>
	/// Total Weight of Vehicle (Columns 27-32) - Critical
	/// </summary>
	/// <remarks>
	/// Enter the gross vehicle weight to the nearest pound. For example, 110,200 lbs. would be
	/// reported in the field as 110200. There are no decimals or commas used in the field. This should
	/// equal the sum of all the axle weights except for rounding.
	/// </remarks>
	public int TotalWeightOfVehicle { get; init; }
	/// <summary>
	/// Number of Axles (Columns 33-34) – Critical
	/// </summary>
	/// <remarks>
	/// <para>
	/// Enter the total number of axles in use by the vehicle (including any trailers).
	/// </para>
	/// <para>
	/// The number of axles determines how many axle weight and spacing fields will be expected on
	/// each record. As a reminder, the axle weight and spacing fields should be reported in English
	/// units. The rest of the record alternates between axle weights and axle spacings, starting from the
	/// front of the vehicle. Axle weights are to the nearest pound. Axle spacings are to the nearest tenth
	/// of a foot. All values should be right-justified with leading blanks as needed.
	/// </para>
	/// <para>
	/// Quality control (QC) checks should be performed on the axle weights and spacings.
	/// </para>
	/// </remarks>
	public int NumberOfAxles { get; init; }
	/// <summary>
	/// Axle Weight 1 (Columns 35-39) - Critical
	/// </summary>
	/// <remarks>
	/// Due to programming language conventions, the first axle weight is `AxleWeights[0]` and the last one is `AxleWeights[12]`.
	/// </remarks>
	public IImmutableList<int?> AxleWeights { get; init; }
	/// <summary>
	/// Axles 1-2 Spacing (Columns 40-43) - Critical
	/// </summary>
	/// <remarks>
	/// Due to programming language conventions, the first axle spacing (1-2) is `AxleWeight[0]` and the last one (12-13) is `AxleWeight[11]`.
	/// </remarks>
	public IImmutableList<int?> AxleSpacing { get; init; }
}
