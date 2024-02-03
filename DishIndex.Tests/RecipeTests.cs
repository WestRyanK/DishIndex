using DishIndex.Models;

namespace DishIndex.Tests;

#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
public class RecipeTests
{
	[Fact]
	public void InstructionStepConstructor_Test()
	{
		InstructionStep step = new(RecipeData.StepStringPeanutButter);
		Assert.Equal(RecipeData.StepStringPeanutButter, step.Instructions);
	}

	[Fact]
	public void InstructionGroupConstructor_CustomName_Test()
	{
		InstructionsGroup group = new(RecipeData.InstructionsGroupComplicatedName,
			[
				new InstructionStep(RecipeData.StepStringPlate),
				new InstructionStep(RecipeData.StepStringPeanutButter),
			]);

		Assert.Equal(RecipeData.InstructionsGroupComplicatedName, group.GroupName);
		Assert.Equal(2, group.Steps.Count);
		Assert.Equal(RecipeData.StepStringPlate, group.Steps[0].Instructions);
		Assert.Equal(RecipeData.StepStringPeanutButter, group.Steps[1].Instructions);
	}

	[Fact]
	public void InstructionGroupConstructor_DefaultName_Test()
	{
		InstructionsGroup group = new(steps: [
				new InstructionStep(RecipeData.StepStringPlate),
			new InstructionStep(RecipeData.StepStringPeanutButter),
		]);

		Assert.Null(group.GroupName);
		Assert.Equal(2, group.Steps.Count);
		Assert.Equal(RecipeData.StepStringPlate, group.Steps[0].Instructions);
		Assert.Equal(RecipeData.StepStringPeanutButter, group.Steps[1].Instructions);
	}

	[Fact]
	public void InstructionGroupConstructor_EmptyConstructor_Test()
	{
		InstructionsGroup group = new();

		Assert.Null(group.GroupName);
		Assert.NotNull(group.Steps);
		Assert.Empty(group.Steps);
	}

	[Fact]
	public void IngredientConstructor_Test()
	{
		Ingredient ingredient = new(
			RecipeData.IngredientNamePeanutButter,
			RecipeData.QuantityPeanutButter,
			RecipeData.IngredientInstructionPeanutButter);

		Assert.Equal(RecipeData.IngredientNamePeanutButter, ingredient.Name);
		Assert.Equal(RecipeData.QuantityPeanutButter, ingredient.Quantity);
		Assert.Equal(RecipeData.IngredientInstructionPeanutButter, ingredient.Instruction);
	}

	[Fact]
	public void IngredientConstructor_NoInstruction_Test()
	{
		Ingredient ingredient = new(
			RecipeData.IngredientNameJelly,
			RecipeData.QuantityJelly);

		Assert.Equal(RecipeData.IngredientNameJelly, ingredient.Name);
		Assert.Equal(RecipeData.QuantityJelly, ingredient.Quantity);
		Assert.Null(ingredient.Instruction);
	}

	[Fact]
	public void IngredientGroupConstructor_CustomName_Test()
	{
		IngredientGroup group = new(RecipeData.IngredientGroupFillingName,
			[
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly
			]);

		Assert.Equal(RecipeData.IngredientGroupFillingName, group.GroupName);
		Assert.Equal(2, group.Ingredients.Count);
		Assert.Equal(RecipeData.IngredientNamePeanutButter, group.Ingredients[0].Name);
		Assert.Equal(RecipeData.IngredientNameJelly, group.Ingredients[1].Name);
	}

	[Fact]
	public void IngredientGroupConstructor_DefaultName_Test()
	{
		IngredientGroup group = new(ingredients:
			[
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly
			]);

		Assert.Null(group.GroupName);
		Assert.Equal(2, group.Ingredients.Count);
		Assert.Equal(RecipeData.IngredientNamePeanutButter, group.Ingredients[0].Name);
		Assert.Equal(RecipeData.IngredientNameJelly, group.Ingredients[1].Name);
	}

	[Fact]
	public void IngredientGroupConstructor_EmptyConstructor_Test()
	{
		IngredientGroup group = new();

		Assert.Null(group.GroupName);
		Assert.NotNull(group.Ingredients);
		Assert.Empty(group.Ingredients);
	}

	[Fact]
	public void RecipeConstructor_Test()
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

		Assert.Equal(RecipeData.RecipeName, recipe.Name);
		Assert.Equal(2, recipe.InstructionsGroups.Count);
		Assert.Equal(RecipeData.IngredientGroupOutsideName, recipe.IngredientGroups[0].GroupName);
		Assert.Equal(RecipeData.IngredientGroupFillingName, recipe.IngredientGroups[1].GroupName);
		Assert.Equal(2, recipe.IngredientGroups.Count);
		Assert.Equal(RecipeData.InstructionsGroupComplicatedName, recipe.InstructionsGroups[0].GroupName);
		Assert.Equal(RecipeData.InstructionsGroupEasyName, recipe.InstructionsGroups[1].GroupName);
		Assert.Equal(1, recipe.Tips.Count);
		Assert.Equal(RecipeData.TipBanana, recipe.Tips[0]);
	}

	[Fact]
	public void RecipeConstructor_SingleGroup_Test()
	{
		Recipe recipe = new(RecipeData.RecipeName,
			[
				RecipeData.IngredientBread,
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly
			],
			[
				RecipeData.InstructionPlate,
				RecipeData.InstructionPeanutButter,
				RecipeData.InstructionJelly,
				RecipeData.InstructionTogether
			]);

		Assert.Equal(RecipeData.RecipeName, recipe.Name);
		Assert.Equal(1, recipe.InstructionsGroups.Count);
		Assert.Null(recipe.IngredientGroups[0].GroupName);
		Assert.Equal(1, recipe.IngredientGroups.Count);
		Assert.Null(recipe.InstructionsGroups[0].GroupName);
		Assert.Empty(recipe.Tips);
	}
}
#pragma warning restore xUnit2013 // Do not use equality check to check for collection size.
