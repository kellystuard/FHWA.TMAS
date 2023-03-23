namespace Fhwa.Tmas.Traffic;

/// <summary>
/// Holds the fields that represent TMAS station description data. Represents the hydrated form of the record 
/// written to disk.
/// </summary>
/// <remarks>
/// <para>
/// The Station Description record format is needed for reporting all data. If a Station Description record
/// is omitted, any succeeding records for other record formats will not be processed by the TMAS
/// software. The Station Description file contains one record per traffic monitoring station, per
/// direction, per lane(unless lanes are combined by the data collection device), per year. In addition,
/// updated station records can be submitted at any time during the year if an equipment change occurs
/// at a site, which would result in a different type of data being submitted at that location. All fields on
/// each record are character fields.
/// </para>
/// <para>
/// The TMAS software retains all approved station records as of December 31 of each year. FHWA
/// recommends that a yearly review of all station record fields be conducted to insure the records are
/// current and reflect what is in the field.
/// </para>
/// </remarks>
public readonly record struct StationDescription : ITrafficData
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
	/// Year of Data (Columns 12-15) – Critical
	/// </summary>
	/// <remarks>
	/// Code the four digits of the year in which the data were collected.
	/// </remarks>
	public int YearOfData { get; init; }
	/// <summary>
	/// Functional Classification Code (Columns 16-17) – Critical
	/// </summary>
	/// <remarks>
	/// Column 16 contains one of the Functional Classification Codes listed in Table 7-5.
	/// </remarks>
	public FunctionalPurpose FunctionalPurpose { get; init; }
	/// <summary>
	/// Functional Classification Code (Columns 16-17) – Critical
	/// </summary>
	/// <remarks>
	/// Column 17 contains either an “R” for rural or “U” for urban.For example, a code of 2R indicates 
	/// a Rural Principal Arterial – Other Freeways and Expressways.
	/// </remarks>
	public FunctionalType FunctionalTypeCode { get; init; }
	/// <summary>
	/// Number of Lanes in Direction Indicated (Column 18) – Critical
	/// </summary>
	/// <remarks>
	/// Code the number of lanes in one direction at the site regardless of the number of lanes being
	/// monitored.Use 9 if there are more than eight lanes.
	/// </remarks>
	public int Lanes { get; init; }
	/// <summary>
	/// Sample Type for TMAS (Column 19) – Critical
	/// </summary>
	/// <remarks>
	/// Data submitted to TMAS may be from research or other needs that do not follow the trends in a
	/// given State and may not be for long-term purposes.
	/// </remarks>
	public bool StationUsedForTmas { get; init; }
	/// <summary>
	/// Number of Lanes Monitored for Traffic Volume (Column 20) – Critical
	/// </summary>
	/// <remarks>
	/// Code the number of lanes in one direction that are monitored at this site.Use 9 if there are more
	/// than eight lanes.
	/// </remarks>
	public int LanesMonitoredForVolume { get; init; }
	/// <summary>
	/// Method of Traffic Volume Counting (Column 21) – Critical
	/// </summary>
	public MethodOfVolumeCounting MethodOfVolumeCounting { get; init; }
	/// <summary>
	/// Number of Lanes Monitored for Vehicle Classification and/or speed (Column 22) – Critical
	/// </summary>
	/// <remarks>
	/// Code the number of lanes in one direction that are monitored for vehicle classification and/or
	/// speed at this site.Use 9 if there are more than eight lanes in a given direction.
	/// </remarks>
	public int LanesMonitoredForClassificationAndOrSpeed { get; init; }
	/// <summary>
	/// Mechanism of Vehicle Classification and/or speed (Column 23) – Critical/Optional
	/// </summary>
	/// <remarks>
	/// <see langword="null"/> this field when unused.
	/// </remarks>
	public MechanismOfClassificationOrSpeed? MechanismOfClassificationOrSpeed { get; init; }
	/// <summary>
	/// Method for Vehicle Classification (Column 24) – Critical/Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// Code the type of input and processing used to classify vehicles.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public MethodOfClassification? MethodOfClassification { get; init; }
	/// <summary>
	/// Vehicle Classification Groupings (Columns 25-26) – Critical/Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// The value in this field indicates the total number of classes in the vehicle classification system
	/// being used as well as how vehicles are grouped together in those classes in relation to the 13
	/// FHWA categories. The recommended default value is 13, which indicates that the standard
	/// FHWA 13 vehicle category classification system (see Appendix C) is being used. Other vehicle
	/// classification systems may be based on the HPMS or specific States’ classification schema
	/// documented in the State’s Traffic Monitoring System (TMS) documentation. The value that is
	/// placed in columns 25 and 26 will determine the number of count fields needed on the Vehicle
	/// Classification Record (see Section 7.5). The following list indicates the acceptable values that can
	/// be entered into Columns 25 and 26 and their meaning. In the following table, the numbers in
	/// parentheses refer to the 13 FHWA classes, and describe how the FHWA classes relate to the
	/// classes being reported.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public ClassificationGroupings? ClassificationGroupings { get; init; }
	/// <summary>
	/// Number of Lanes Monitored for Truck Weight (Column 27) – Critical
	/// </summary>
	/// <remarks>
	/// Code the number of lanes in one direction that are monitored for truck weight at this site.Use 9
	/// if there are more than eight lanes.
	/// </remarks>
	public int LanesMonitoredForTruckWeight { get; init; }
	/// <summary>
	/// Method of Truck Weighing (Column 28) – Critical/Optional
	/// </summary>
	/// <remarks>
	/// <see langword="null"/> this field when unused.
	/// </remarks>
	public MethodOfTruckWeighing? MethodOfTruckWeighing { get; init; }
	/// <summary>
	/// Calibration of Weighing System (Column 29) – Critical/Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// Code the method used to calibrate the weighing system, e.g., comparing weigh-in-motion and
	/// weights from static scales.At a minimum, yearly calibration is recommended, and maybe more
	/// often depending upon the site, sensors, equipment, and array used.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public CalibrationOfWeighing? CalibrationOfWeighing { get; init; }
	/// <summary>
	/// Method of Data Retrieval (Column 30) – Critical
	/// </summary>
	/// <remarks>
	/// <see langword="null"/> this field when unused.
	/// </remarks>
	public MethodOfDataRetrieval? MethodOfDataRetrieval { get; init; }
	/// <summary>
	/// Type of Sensor (Column 31) – Critical
	/// </summary>
	/// <remarks>
	/// Code the type of sensor used for traffic detection.
	/// </remarks>
	public TypeOfSensor TypeOfSensor1 { get; init; }
	/// <summary>
	/// Second Type of Sensor (Column 32) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// If there are two types of sensors at the station, code the second using the same codes as Type of
	/// Sensor. Otherwise, code N for none.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public TypeOfSensor? TypeOfSensor2 { get; init; }
	/// <summary>
	/// Primary Purpose (Column 33) – Critical
	/// </summary>
	/// <remarks>
	/// This field indicates the primary purpose for installing the station and hence which organization is
	/// responsible for it and supplies the data.
	/// </remarks>
	public StationPurpose PrimaryPurpose { get; init; }
	/// <summary>
	/// Linear Referencing System Route ID (Columns 34-93) (60 characters) – Critical
	/// </summary>
	/// <remarks>
	/// <para>
	/// The LRS Route ID is the route identification reported in the HPMS database for the section of
	/// roadway where the station is located.The LRS Route_ID field allows for 60 characters, is right
	/// justified, and must contain leading zeros. No blanks are allowed unless the entire field is left
	/// blank. The LRS Route_ID entry can be alphanumeric but must not contain blanks. More
	/// information concerning the LRS Route_ID Field may be found in the HPMS Field Manual.
	/// </para>
	/// <para>
	/// Note that in the 2015 HPMS Addendum, states are allowed 120 characters instead of the 60
	/// allowed in the 2010 coding instructions. If your state uses more than 60 characters in this field,
	/// enter the 60 rightmost characters in Field 23 of this station description record.
	/// </para>
	/// </remarks>
	public string LrsRouteID { get; init; }
	/// <summary>
	/// Linear Referencing System Location Point (Columns 94-101) (8 digits) – Critical
	/// </summary>
	/// <remarks>
	/// This is the LRS location point for the station. It is the milepost location for the route named by
	/// the Route_ID field. It is similar information to the LRS Beginning Point and LRS Ending Point in the
	/// HPMS. The milepost for the station must be within the range of the LRS beginning point and LRS
	/// ending point for the HPMS roadway section upon which the station is located. It is coded in
	/// miles, to the nearest thousandth of a mile, with an implied decimal in the middle: XXXXX.XXX.
	/// </remarks>
	public double LrsLocation { get; init; }
	/// <summary>
	/// Latitude (Columns 102-109) – Critical
	/// </summary>
	/// <remarks>
	/// This is the latitude of the station location with the north hemisphere assumed and decimal place
	/// understood as XX.XXX XXX.If the value is 39.178 400 (Illinois), then the field is coded as
	/// ‘39178751’, with an implied decimal point after the second digit.
	/// </remarks>
	public double Latitude { get; init; }
	/// <summary>
	/// Longitude (Columns 110-118) – Critical
	/// </summary>
	/// <remarks>
	/// This is the longitude of the station location with the west hemisphere assumed and decimal place
	/// understood as XXX.XXX XXX. If the value is 088.352 540 (Illinois), then the field is coded as
	/// ‘088352540’, with an implied decimal point after the third digit.
	/// </remarks>
	public double Longitude { get; init; }
	/// <summary>
	/// LTPP Site Identification (Columns 119-122) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// If the site is used in the LTPP sample, give the LTPP site ID.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public string? LttpSiteID { get; init; }
	/// <summary>
	/// Previous Station ID (Columns 123-128) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// If the station replaces another station, give the station ID that was used previously.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public string? PreviousStationID { get; init; }
	/// <summary>
	/// Year Station Established (Columns 129-132) – Critical
	/// </summary>
	/// <remarks>
	/// Code the four digits of the appropriate year if known.
	/// </remarks>
	public int? YearEstablished { get; init; }
	/// <summary>
	/// Year Station Discontinued (Columns 133-136) – Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// Code the four digits of the appropriate year if known.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public int? YearDiscontinued { get; init; }
	/// <summary>
	/// FIPS County Code (Columns 137-139) – Critical
	/// </summary>
	/// <remarks>
	/// Use the three-digit FIPS county code(see Federal Information Processing Standards Publication
	/// 6, Counties of the States of the United States).
	/// </remarks>
	/// <example>Should be changed to enum from this source: https://raw.githubusercontent.com/kjhealy/us-county/master/data/census/fips-by-state.csv</example>
	public int CountyCode { get; init; }
	/// <summary>
	/// HPMS Sample Type (Column 140) – Critical
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description><see langword="false"/> = No, not on an HPMS standard sample section</description></item>
	/// <item><description><see langword="true"/> = Yes, on an HPMS standard sample section</description></item>
	/// </list>
	/// </remarks>
	public bool HpmsSampleType { get; init; }
	/// <summary>
	/// HPMS Sample Identifier (Columns 141-152) – Critical/Optional
	/// </summary>
	/// <remarks>
	/// <para>
	/// If the station is on an HPMS standard sample section, code the HPMS Sample Identifier per the
	/// HPMS Field Manual(Field 7 in the HPMS Sample Panel Identification dataset).
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public string? HpmsSampleID { get; init; }
	/// <summary>
	/// National Highway System (Column 153) – Critical
	/// </summary>
	/// <remarks>
	/// <list type="bullet">
	/// <item><description><see langword="false"/> = No, not on National Highway System</description></item>
	/// <item><description><see langword="true"/> = Yes, on National Highway System</description></item>
	/// </list>
	/// </remarks>
	public bool NationalHighwaySystem { get; init; }
	/// <summary>
	/// Posted Route Signing (Column 154-155) – Critical
	/// </summary>
	/// <remarks>
	/// This is the same as Route Signing in HPMS Field Manual.
	/// </remarks>
	public PostedRouteSigning PostedRouteSigning { get; init; }
	/// <summary>
	/// Posted Signed Route Number (Columns 156-163) – Critical
	/// </summary>
	/// <remarks>
	/// <para>
	/// Code the route number of the principal route on which the station is located.This is the same as
	/// Signed Route Number in HPMS Field Manual.
	/// </para>
	/// <para>
	/// If the station is located on a city street, <see langword="null"/> this field.
	/// </para>
	/// </remarks>
	public string? PostedRouteNumber { get; init; }
	/// <summary>
	/// Station Location (Columns 164-213) – Critical
	/// </summary>
	/// <remarks>
	/// <para>
	/// This is an English text entry field. For stations located on a numbered route, enter the name of
	/// the nearest major intersecting route, State border, or landmark on State road maps and the
	/// distance and direction of the station from that landmark to the station (e.g., “12 miles south of
	/// the Kentucky border”). If the station is located on a city street, enter the city and street name.
	/// Abbreviate if necessary. Left justify this field.
	/// </para>
	/// <para>
	/// <see langword="null"/> this field when unused.
	/// </para>
	/// </remarks>
	public string? StationLocation { get; init; }
}
