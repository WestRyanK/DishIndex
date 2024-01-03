﻿using DishIndex.Utilities;
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

    public static VolumeQuantity ConvertTo(VolumeQuantity quantity, VolumeUnit unit)
    {
        double milliliters = quantity.Scalar * quantity.Unit.Milliliters();
        double inNewUnit = milliliters / unit.Milliliters();
        return new VolumeQuantity { Scalar = inNewUnit, Unit = unit };
    }
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
    USTeaspoon,
    [Milliliters(14.786764781)]
    USTablespoon,
    [Milliliters(236.5882365)]
    USCup,
    [Milliliters(473.176473)]
    USPint,
    [Milliliters(946.352946)]
    USQuart,
    [Milliliters(3785.411784)]
    USGallon,
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