using DishIndex.Core.Models;
using S = System.Text.Json;
using N = Newtonsoft.Json;
using DishIndex.Core.Generators;
using DishIndex.Core.Utilities;

namespace DishIndex.Tests;

public class SerializationTests
{
	IJsonSerializer _serializer = new NewtonsoftJsonSerializer();

	[Fact]
	public void RecipeSerialization_Test()
	{
		Recipe recipe = new(RecipeData.RecipeName,
			[
				new IngredientsGroup(RecipeData.IngredientsGroupOutsideName,
				[
					RecipeData.IngredientBread,
				]),
				new IngredientsGroup(RecipeData.IngredientsGroupFillingName,
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

		string serialized = _serializer.Serialize(recipe);
		Recipe back = _serializer.Deserialize<Recipe>(serialized);

		Assert.Equal(RecipeData.RecipeName, back.Name);
		Assert.Equal(2, back.IngredientsGroups.Count);
		Assert.Equal(RecipeData.IngredientsGroupOutsideName, back.IngredientsGroups[0].GroupName);
		Assert.Equal(RecipeData.IngredientsGroupFillingName, back.IngredientsGroups[1].GroupName);
		Assert.Equal(2, back.InstructionsGroups.Count);
		Assert.Equal(RecipeData.InstructionsGroupComplicatedName, back.InstructionsGroups[0].GroupName);
		Assert.Equal(RecipeData.InstructionsGroupEasyName, back.InstructionsGroups[1].GroupName);
		Assert.Equal(1, back.Tips.Count);
		Assert.Equal(RecipeData.TipBanana, back.Tips[0]);

		Assert.Equal(1, back.IngredientsGroups[0].Ingredients.Count);
		Assert.Equal(2, back.IngredientsGroups[1].Ingredients.Count);

		Assert.Equal(recipe.IngredientsGroups[0].Ingredients[0].Instruction, back.IngredientsGroups[0].Ingredients[0].Instruction);
	}

	[Fact]
	public void VolumeQuantityUnitSerialization_Test()
	{
		string serialized = _serializer.Serialize(RecipeData.QuantityJelly);
		Assert.Contains(RecipeData.QuantityJelly.Unit.ToString(), serialized);
	}

	[Fact]
	public void VolumeQuantitySerialization_Test()
	{
		string serialized = _serializer.Serialize(RecipeData.QuantityJelly);
		VolumeQuantity back = _serializer.Deserialize<VolumeQuantity>(serialized)!;

		Assert.Equal(RecipeData.QuantityJelly.Scalar, back.Scalar);
		Assert.Equal(RecipeData.QuantityJelly.Unit, back.Unit);


		serialized = _serializer.Serialize(RecipeData.QuantityPeanutButter);
		back = _serializer.Deserialize<VolumeQuantity>(serialized)!;

		Assert.Equal(RecipeData.QuantityPeanutButter.Scalar, back.Scalar);
		Assert.Equal(RecipeData.QuantityPeanutButter.Unit, back.Unit);


		serialized = _serializer.Serialize(RecipeData.QuantityBread);
		back = _serializer.Deserialize<VolumeQuantity>(serialized)!;

		Assert.Equal(RecipeData.QuantityBread.Scalar, back.Scalar);
		Assert.Equal(RecipeData.QuantityBread.Unit, back.Unit);
	}

	[Fact]
	public void IngredientSerialization_Test()
	{
		string serialized = _serializer.Serialize(RecipeData.IngredientPeanutButter);
		Ingredient back = _serializer.Deserialize<Ingredient>(serialized)!;

		Assert.Equal(RecipeData.IngredientPeanutButter.Name, back.Name);
		Assert.Equal(RecipeData.IngredientPeanutButter.Instruction, back.Instruction);
		Assert.Equal(RecipeData.IngredientPeanutButter.Quantity!.Scalar, back.Quantity!.Scalar);
	}

	[Fact]
	public void IngredientSerialization_NoInstruction_Test()
	{
		string serialized = _serializer.Serialize(RecipeData.IngredientJelly);
		Ingredient back = _serializer.Deserialize<Ingredient>(serialized)!;

		Assert.Equal(RecipeData.IngredientJelly.Name, back.Name);
		Assert.DoesNotContain(nameof(Ingredient.Instruction), serialized);
		Assert.Null(back.Instruction);
		Assert.Equal(RecipeData.IngredientJelly.Quantity!.Scalar, back.Quantity!.Scalar); }

	[Fact]
	public void InstructionsSerialization_Test()
	{
		string serialized = _serializer.Serialize(RecipeData.InstructionEasy);
		InstructionStep back = _serializer.Deserialize<InstructionStep>(serialized)!;

		Assert.Equal(RecipeData.InstructionEasy.Instructions, back.Instructions);
	}

	[Fact]
	public void IngredientsGroupSerialization_Test()
	{
		IngredientsGroup group = new IngredientsGroup(RecipeData.IngredientsGroupFillingName,
			[
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly,
			]);

		string serialized = _serializer.Serialize(group);
		IngredientsGroup back = _serializer.Deserialize<IngredientsGroup>(serialized)!;

		Assert.Equal(group.GroupName, group.GroupName);
		Assert.Equal(2, group.Ingredients.Count);
		Assert.Equal(group.Ingredients[0].Name, group.Ingredients[0].Name);
	}

	[Fact]
	public void IngredientsGroupSerialization_NoName_Test()
	{
		IngredientsGroup group = new IngredientsGroup(ingredients: [
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly,
			]);

		string serialized = _serializer.Serialize(group);
		IngredientsGroup back = _serializer.Deserialize<IngredientsGroup>(serialized)!;

		Assert.DoesNotContain(nameof(IngredientsGroup.GroupName), serialized);
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

		string serialized = _serializer.Serialize(group);
		InstructionsGroup back = _serializer.Deserialize<InstructionsGroup>(serialized)!;

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

		string serialized = _serializer.Serialize(group);
		InstructionsGroup back = _serializer.Deserialize<InstructionsGroup>(serialized)!;

		Assert.DoesNotContain(nameof(InstructionsGroup.GroupName), serialized);
		Assert.Null(group.GroupName);
		Assert.Equal(3, group.Steps.Count);
		Assert.Equal(group.Steps[0].Instructions, group.Steps[0].Instructions);
	}

	[Fact]
	public void SchemaEnum_Test()
	{
		string schema = NewtonsoftJsonSerializer.GetSchema<VolumeUnit>();
		Assert.Contains("\"type\": \"string\"", schema); // Enum values must be serialized as a string, not integer
		Assert.DoesNotContain("\"type\": \"integer\"", schema);
	}
}
