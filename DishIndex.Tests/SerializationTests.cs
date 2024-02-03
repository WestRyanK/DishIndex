using DishIndex.Models;
using System.Text.Json;

namespace DishIndex.Tests;

#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
public class SerializationTests
{
	private static readonly JsonSerializerOptions _SerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
	{
		WriteIndented = true,
		
	};

	[Fact]
	public void RecipeSerialization_Test()
	{
		Recipe recipe = new(RecipeData.RecipeName,
			[
				new IngredientGroup(RecipeData.IngredientGroupOutsideName,
				[
					RecipeData.IngredientBread,
				]),
				new IngredientGroup(RecipeData.IngredientGroupFillingName,
				[
					RecipeData.IngredientPeanutButter,
					RecipeData.IngredientJelly
				])
			],
			[
				new InstructionsGroup(RecipeData.InstructionsGroupComplicatedName,
				[
					RecipeData.InstructionPlate,
					RecipeData.InstructionPeanutButter,
					RecipeData.InstructionJelly,
					RecipeData.InstructionTogether
				]),
				new InstructionsGroup(RecipeData.InstructionsGroupEasyName,
				[
					RecipeData.InstructionEasy
				])
			],
			[
				RecipeData.TipBanana
			]);

		string serialized = JsonSerializer.Serialize(recipe, _SerializerOptions);
		Recipe back = JsonSerializer.Deserialize<Recipe>(serialized)!;

		Assert.Equal(RecipeData.RecipeName, back.Name);
		Assert.Equal(2, back.IngredientGroups.Count);
		Assert.Equal(RecipeData.IngredientGroupOutsideName, back.IngredientGroups[0].GroupName);
		Assert.Equal(RecipeData.IngredientGroupFillingName, back.IngredientGroups[1].GroupName);
		Assert.Equal(2, back.InstructionsGroups.Count);
		Assert.Equal(RecipeData.InstructionsGroupComplicatedName, back.InstructionsGroups[0].GroupName);
		Assert.Equal(RecipeData.InstructionsGroupEasyName, back.InstructionsGroups[1].GroupName);
		Assert.Equal(1, back.Tips.Count);
		Assert.Equal(RecipeData.TipBanana, back.Tips[0]);

		Assert.Equal(1, back.IngredientGroups[0].Ingredients.Count);
		Assert.Equal(2, back.IngredientGroups[1].Ingredients.Count);

		Assert.Equal(recipe.IngredientGroups[0].Ingredients[0].Instruction, back.IngredientGroups[0].Ingredients[0].Instruction);
	}

	[Fact]
	public void VolumeQuantityUnitSerialization_Test()
	{
		string serialized = JsonSerializer.Serialize(RecipeData.QuantityJelly, _SerializerOptions);
		Assert.Contains(RecipeData.QuantityJelly.Unit.ToString(), serialized);
	}

	[Fact]
	public void VolumeQuantitySerialization_Test()
	{
		string serialized = JsonSerializer.Serialize(RecipeData.QuantityJelly, _SerializerOptions);
		VolumeQuantity back = JsonSerializer.Deserialize<VolumeQuantity>(serialized)!;

		Assert.Equal(RecipeData.QuantityJelly.Scalar, back.Scalar);
		Assert.Equal(RecipeData.QuantityJelly.Unit, back.Unit);


		serialized = JsonSerializer.Serialize(RecipeData.QuantityPeanutButter);
		back = JsonSerializer.Deserialize<VolumeQuantity>(serialized)!;

		Assert.Equal(RecipeData.QuantityPeanutButter.Scalar, back.Scalar);
		Assert.Equal(RecipeData.QuantityPeanutButter.Unit, back.Unit);


		serialized = JsonSerializer.Serialize(RecipeData.QuantityBread);
		back = JsonSerializer.Deserialize<VolumeQuantity>(serialized)!;

		Assert.Equal(RecipeData.QuantityBread.Scalar, back.Scalar);
		Assert.Equal(RecipeData.QuantityBread.Unit, back.Unit);
	}

	[Fact]
	public void IngredientSerialization_Test()
	{
		string serialized = JsonSerializer.Serialize(RecipeData.IngredientPeanutButter, _SerializerOptions);
		Ingredient back = JsonSerializer.Deserialize<Ingredient>(serialized)!;

		Assert.Equal(RecipeData.IngredientPeanutButter.Name, back.Name);
		Assert.Equal(RecipeData.IngredientPeanutButter.Instruction, back.Instruction);
		Assert.Equal(RecipeData.IngredientPeanutButter.Quantity.Scalar, back.Quantity.Scalar);
	}

	[Fact]
	public void IngredientSerialization_NoInstruction_Test()
	{
		string serialized = JsonSerializer.Serialize(RecipeData.IngredientJelly, _SerializerOptions);
		Ingredient back = JsonSerializer.Deserialize<Ingredient>(serialized)!;

		Assert.Equal(RecipeData.IngredientJelly.Name, back.Name);
		Assert.DoesNotContain(nameof(Ingredient.Instruction), serialized);
		Assert.Null(back.Instruction);
		Assert.Equal(RecipeData.IngredientJelly.Quantity.Scalar, back.Quantity.Scalar); }

	[Fact]
	public void InstructionsSerialization_Test()
	{
		string serialized = JsonSerializer.Serialize(RecipeData.InstructionEasy, _SerializerOptions);
		InstructionStep back = JsonSerializer.Deserialize<InstructionStep>(serialized)!;

		Assert.Equal(RecipeData.InstructionEasy.Instructions, back.Instructions);
	}

	[Fact]
	public void IngredientsGroupSerialization_Test()
	{
		IngredientGroup group = new IngredientGroup(RecipeData.IngredientGroupFillingName,
			[
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly,
			]);

		string serialized = JsonSerializer.Serialize(group, _SerializerOptions);
		IngredientGroup back = JsonSerializer.Deserialize<IngredientGroup>(serialized)!;

		Assert.Equal(group.GroupName, group.GroupName);
		Assert.Equal(2, group.Ingredients.Count);
		Assert.Equal(group.Ingredients[0].Name, group.Ingredients[0].Name);
	}

	[Fact]
	public void IngredientsGroupSerialization_NoName_Test()
	{
		IngredientGroup group = new IngredientGroup(ingredients: [
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly,
			]);

		string serialized = JsonSerializer.Serialize(group, _SerializerOptions);
		IngredientGroup back = JsonSerializer.Deserialize<IngredientGroup>(serialized)!;

		Assert.DoesNotContain(nameof(IngredientGroup.GroupName), serialized);
		Assert.Null(group.GroupName);
		Assert.Equal(2, group.Ingredients.Count);
		Assert.Equal(group.Ingredients[0].Name, group.Ingredients[0].Name);
	}

	[Fact]
	public void InstructionsGroupSerialization_Test()
	{
		InstructionsGroup group = new InstructionsGroup(RecipeData.InstructionsGroupComplicatedName,
			[
				RecipeData.InstructionPlate,
				RecipeData.InstructionPeanutButter,
				RecipeData.InstructionJelly,
			]);

		string serialized = JsonSerializer.Serialize(group, _SerializerOptions);
		InstructionsGroup back = JsonSerializer.Deserialize<InstructionsGroup>(serialized)!;

		Assert.Equal(group.GroupName, group.GroupName);
		Assert.Equal(3, group.Steps.Count);
		Assert.Equal(group.Steps[0].Instructions, group.Steps[0].Instructions);
	}

	[Fact]
	public void InstructionsGroupSerialization_NoName_Test()
	{
		InstructionsGroup group = new InstructionsGroup(steps: [
				RecipeData.InstructionPlate,
				RecipeData.InstructionPeanutButter,
				RecipeData.InstructionJelly,
			]);

		string serialized = JsonSerializer.Serialize(group, _SerializerOptions);
		InstructionsGroup back = JsonSerializer.Deserialize<InstructionsGroup>(serialized)!;

		Assert.DoesNotContain(nameof(InstructionsGroup.GroupName), serialized);
		Assert.Null(group.GroupName);
		Assert.Equal(3, group.Steps.Count);
		Assert.Equal(group.Steps[0].Instructions, group.Steps[0].Instructions);
	}
}
#pragma warning restore xUnit2013 // Do not use equality check to check for collection size.
