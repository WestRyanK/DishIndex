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
    [VolumeUnit(0, SystemOfMeasurement.None)]
    None,
    [VolumeUnit(1, SystemOfMeasurement.Metric)]
    Milliliter,
    [VolumeUnit(1000, SystemOfMeasurement.Metric)]
    Liter,
    [VolumeUnit(4.9289215937, SystemOfMeasurement.US)]
    TeaspoonUS,
    [VolumeUnit(14.786764781, SystemOfMeasurement.US)]
    TablespoonUS,
    [VolumeUnit(236.5882365, SystemOfMeasurement.US)]
    CupUS,
    [VolumeUnit(473.176473, SystemOfMeasurement.US)]
    PintUS,
    [VolumeUnit(946.352946, SystemOfMeasurement.US)]
    QuartUS,
    [VolumeUnit(3785.411784, SystemOfMeasurement.US)]
    GallonUS,
}

internal enum SystemOfMeasurement
{
    None,
    Metric,
    US
}

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class VolumeUnitAttribute : Attribute
{
    readonly double _milliliters;
    public double Milliliters => _milliliters;

    readonly SystemOfMeasurement _systemOfMeasurement;
    public SystemOfMeasurement SystemOfMeasurement => _systemOfMeasurement;

    public VolumeUnitAttribute(double milliliterQuantity, SystemOfMeasurement systemOfMeasurement)
    {
        _milliliters = milliliterQuantity;
        _systemOfMeasurement = systemOfMeasurement;
    }
}

internal static class VolumeUnitExtensions
{
    private static readonly ConcurrentDictionary<VolumeUnit, VolumeUnitAttribute> _VolumeUnitAttributesByVolumeUnit = new();

    private static VolumeUnitAttribute VolumeUnitAttribute(this VolumeUnit volumeUnit)
    {
        if (_VolumeUnitAttributesByVolumeUnit.TryGetValue(volumeUnit, out var result))
        {
            return result;
        }

        if (volumeUnit.TryGetEnumAttribute(out VolumeUnitAttribute? attribute))
        {
            _VolumeUnitAttributesByVolumeUnit.TryAdd(volumeUnit, attribute);
            return attribute;
        }

        throw new Exception($"Missing {nameof(VolumeUnitAttribute)} Attribute");
    }

    public static double Milliliters(this VolumeUnit volumeUnit) => volumeUnit.VolumeUnitAttribute().Milliliters;

    public static SystemOfMeasurement SystemOfMeasurement(this VolumeUnit volumeUnit) => volumeUnit.VolumeUnitAttribute().SystemOfMeasurement;
}