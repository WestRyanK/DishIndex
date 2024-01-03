using DishIndex.Utilities;
using System.Collections.Concurrent;

namespace DishIndex.Models;

internal struct VolumeQuantity
{
    private double _scalar;

    public double Scalar
    {
        get => _scalar;
        set => _scalar = value;
    }

    private VolumeUnit _unit;

    public VolumeUnit Unit
    {
        get => _unit;
        set => _unit = value;
    }

    public VolumeQuantity(double scalar, VolumeUnit unit)
    {
        Scalar = scalar;
        Unit = unit;
    }

    public VolumeQuantity ConvertTo(VolumeUnit unit)
    {
        double milliliters = Scalar * Unit.Milliliters();
        double inNewUnit = milliliters / unit.Milliliters();
        return new VolumeQuantity { Scalar = inNewUnit, Unit = unit };
    }

    public static VolumeQuantity operator *(VolumeQuantity quantity, double scale) => new VolumeQuantity(quantity.Scalar * scale, quantity.Unit);
}

internal enum VolumeUnit
{
    [Milliliters(0)]
    None,
    [Milliliters(1)]
    Milliliter,
    [Milliliters(1000)]
    Liter,
    [Milliliters(4.9289215937)]
    TeaspoonUS,
    [Milliliters(14.786764781)]
    TablespoonUS,
    [Milliliters(236.5882365)]
    CupUS,
    [Milliliters(473.176473)]
    PintUS,
    [Milliliters(946.352946)]
    QuartUS,
    [Milliliters(3785.411784)]
    GallonUS,
}

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class MillilitersAttribute : Attribute
{
    readonly double _milliliters;

    public MillilitersAttribute(double milliliterQuantity)
    {
        this._milliliters = milliliterQuantity;
    }

    public double Milliliters => _milliliters;
}

internal static class VolumeUnitExtensions
{
    private static readonly ConcurrentDictionary<VolumeUnit, MillilitersAttribute> _MilliliterAttributesByVolumeUnit = new();

    private static MillilitersAttribute MilliliterAttribute(this VolumeUnit volumeUnit)
    {
        if (_MilliliterAttributesByVolumeUnit.TryGetValue(volumeUnit, out var result))
        {
            return result;
        }

        if (volumeUnit.TryGetEnumAttribute(out MillilitersAttribute? attribute))
        {
            _MilliliterAttributesByVolumeUnit.TryAdd(volumeUnit, attribute);
            return attribute;
        }

        throw new Exception($"Missing {nameof(MilliliterAttribute)} Attribute");
    }

    public static double Milliliters(this VolumeUnit volumeUnit) => volumeUnit.MilliliterAttribute().Milliliters;
}