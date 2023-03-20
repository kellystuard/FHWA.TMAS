namespace Fhwa.Tmas.Traffic;

/// <summary>
/// When applied to properties of classes that implement <see cref="ITrafficData"/>, controlls the order that 
/// they are written.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class TrafficFormatAttribute : Attribute
{
	/// <summary>
	/// When applied to properties of classes that implement <see cref="ITrafficData"/>, controlls the order that 
	/// they are written.
	/// </summary>
	/// <param name="order">The order that the property should be written to the file.</param>
	/// <param name="width">The width of the property, in characters, as it is written to the file.</param>
	/// <param name="required">Determines if the field is required to have a value.</param>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when order is less than zero (0).</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when width is less than one (1).</exception>
	public TrafficFormatAttribute(int order, int width, bool required)
	{
		if (order < 0) throw new ArgumentOutOfRangeException(nameof(order), order, "Must be zero (0) or greater");
		if (width < 1) throw new ArgumentOutOfRangeException(nameof(width), width, "Must be one (1) or greater");

		Order = order;
		Width = width;
		Required = required;
	}

	/// <summary>
	/// The order that the property should be written to the file.
	/// </summary>
	/// <remarks>
	/// Though the properties are written in order, there can be gaps in the numbering.
	/// </remarks>
	public int Order { get; init; }
	/// <summary>
	/// The width of the property, in characters, as it is written to the file.
	/// </summary>
	/// <remarks>
	/// A width of at least 1 is required.
	/// </remarks>
	public int Width { get; init; }
	/// <summary>
	/// Determines if the field is required to have a value.
	/// </summary>
	/// <remarks>
	/// In strict writing, if a field is required but the value is <see langword="null"/> an exception will be thrown.
	/// </remarks>
	public bool Required { get; init; }
}