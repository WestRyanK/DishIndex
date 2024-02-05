using S = System.Text.Json.Serialization;
using N = Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using System.ComponentModel;

namespace DishIndex.Core.Models;

public enum SystemOfMeasurement
{
	None,
	Metric,
	US
}

[Description("Specifies ingredient quantity in a chosen unit.")]
public abstract class BaseQuantity<Q, U>
	where Q : BaseQuantity<Q, U>, new()
	where U : struct, Enum
{
	[Description("Numerical value of the quantity.")]
	public double Scalar { get; set; }
	[Description("Measurement unit for the quantity.")]
	[S.JsonConverter(typeof(S.JsonStringEnumConverter))]
	[N.JsonConverter(typeof(N.Converters.StringEnumConverter))]
	public U Unit { get; set; }

	public Q ConvertTo(U unit)
	{
		double milliliters = Scalar * UnitQuantity(Unit);
		double inNewUnit = milliliters / UnitQuantity(unit);
		return new Q() { Scalar = inNewUnit, Unit = unit };
	}

	public abstract double UnitQuantity(U unit);

	public static Q operator *(BaseQuantity<Q, U> quantity, double scale) => new Q() { Scalar = quantity.Scalar * scale, Unit = quantity.Unit };
}
