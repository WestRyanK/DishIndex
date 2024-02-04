using DishIndex.Core.Models;

namespace DishIndex.Tests;

public class VolumeQuantityTests
{
	private void TestConversion(double inputScalar, VolumeUnit inputUnit, double outputScalar, VolumeUnit outputUnit, double precision = 0.000001)
	{
		VolumeQuantity input = new(inputScalar, inputUnit);
		VolumeQuantity output = input.ConvertTo(outputUnit);
		VolumeQuantity back = output.ConvertTo(inputUnit);

		Assert.Equal(outputUnit, output.Unit);
		Assert.Equal(outputScalar, output.Scalar, precision);

		Assert.Equal(input.Unit, back.Unit);
		Assert.Equal(input.Scalar, back.Scalar, precision);
	}

	[Fact]
	public void USConversion_Tests()
	{
		TestConversion(1, VolumeUnit.TeaspoonUS, 1.0 / 3, VolumeUnit.TablespoonUS);
		TestConversion(1, VolumeUnit.TablespoonUS, 1.0 / 16, VolumeUnit.CupUS);
		TestConversion(1, VolumeUnit.CupUS, .5, VolumeUnit.PintUS);
		TestConversion(1, VolumeUnit.PintUS, .5, VolumeUnit.QuartUS);
		TestConversion(1, VolumeUnit.QuartUS, .25, VolumeUnit.GallonUS);

		TestConversion(1, VolumeUnit.GallonUS, 768, VolumeUnit.TeaspoonUS);
	}

	[Fact]
	public void ConversionWithScalar_Tests()
	{
		TestConversion(2, VolumeUnit.GallonUS, 2 * 768, VolumeUnit.TeaspoonUS);
	}

	[Fact]
	public void MetricConversion_Tests()
	{
		TestConversion(1, VolumeUnit.Milliliter, .001, VolumeUnit.Liter);
	}

	[Fact]
	public void USToMetricConversion_Tests()
	{
		TestConversion(1, VolumeUnit.QuartUS, 0.946352946, VolumeUnit.Liter);
	}

	[Fact]
	public void ScaleVolumeQuantity_Test()
	{
		VolumeQuantity input = new(2, VolumeUnit.CupUS);
		VolumeQuantity output = input * 5;

		Assert.Equal(input.Unit, output.Unit);
		Assert.Equal(10, output.Scalar);
	}
}