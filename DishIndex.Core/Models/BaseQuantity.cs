using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DishIndex.Models;

public enum SystemOfMeasurement
{
	None,
	Metric,
	US
}

public abstract class BaseQuantity<Q, U>
	where Q : BaseQuantity<Q, U>, new()
	where U : struct, Enum
{
	public double Scalar { get; set; }
	[JsonConverter(typeof(JsonStringEnumConverter))]
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
