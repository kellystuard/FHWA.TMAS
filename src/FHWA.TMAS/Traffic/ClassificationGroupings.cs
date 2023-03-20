namespace Fhwa.Tmas.Traffic;

/// <summary>
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
/// </summary>
public enum ClassificationGroupings
{
	/// <summary>
	/// Two groups:
	/// <list type="bullet">
	/// <item><description>(classes 1-3) vehicles</description></item>
	/// <item><description>(classes 4-7; 8-10; 11-13) vehicles</description></item>
	/// </list>
	/// </summary>
	Two = 2,
	/// <summary>
	/// Three groups:
	/// <list type="bullet">
	/// <item><description>(classes 1-3) vehicles</description></item>
	/// <item><description>single-unit (classes 4-7)</description></item>
	/// <item><description>combination (classes 8-13) vehicles</description></item>
	/// </list>
	/// </summary>
	Three = 3,
	/// <summary>
	/// Four groups:
	/// <list type="bullet">
	/// <item><description>(classes 1-3) vehicles</description></item>
	/// <item><description>single-unit vehicles (classes 4-7)</description></item>
	/// <item><description>single-trailer vehicles (classes 8-10)</description></item>
	/// <item><description>multiple -trailer vehicles (classes 11-13)</description></item>
	/// </list>
	/// </summary>
	Four = 4,
	/// <summary>
	/// Five groups:
	/// <list type="bullet">
	/// <item><description>motorcycles (class 1)</description></item>
	/// <item><description>two-axle, four-tire vehicles(classes 2-3)</description></item>
	/// <item><description>buses and single-unit vehicles(classes 4-7)</description></item>
	/// <item><description>single-trailer combination vehicles(classes 8-10)</description></item>
	/// <item><description>multiple-trailer combination vehicles(classes 11-13)</description></item>
	/// </list>
	/// </summary>
	Five = 5,
	/// <summary>
	/// Six groups:
	/// <list type="bullet">
	/// <item><description>motorcycles (class 1)</description></item>
	/// <item><description>two-axle, four-tire vehicles(classes 2-3)</description></item>
	/// <item><description>buses(class 4)</description></item>
	/// <item><description>single-unit vehicles(classes 5-7)</description></item>
	/// <item><description>single-trailer combination vehicles(classes 8-10)</description></item>
	/// <item><description>multiple-trailer combination vehicles(classes 11-13)</description></item>
	/// </list>
	/// </summary>
	Six = 6,
	/// <summary>
	/// Seven groups:
	/// <list type="bullet">
	/// <item><description>motorcycles (class 1)</description></item>
	/// <item><description>passenger cars(class 2)</description></item>
	/// <item><description>light duty trucks(class 3)</description></item>
	/// <item><description>buses(class 4)</description></item>
	/// <item><description>single-unit vehicles(classes 5-7)</description></item>
	/// <item><description>combination vehicles(classes 8-10)</description></item>
	/// <item><description>multiple-trailer combination vehicles(classes 11-13)</description></item>
	/// </list>
	/// </summary>
	Seven = 7,
	/// <summary>FHWA's standard 13 class system.</summary>
	Thirteen = 13,
}