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

[Description("The amount of an ingredient specified as a scalar number and a unit of measurement.")]
public abstract class BaseQuantity<Q, U>
	where Q : BaseQuantity<Q, U>, new()
	where U : struct, Enum
{
	[Description("The number amount of an ingredient without the unit of measurement.")]
	public double Scalar { get; set; }
	[Description("The unit of measurement used to measure the quantity of an ingredient.")]
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
