using DishIndex.Models;

namespace DishIndex.Tests;

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
	public void IngredientsGroupConstructor_CustomName_Test()
	{
		IngredientsGroup group = new(RecipeData.IngredientsGroupFillingName,
			[
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly
			]);

		Assert.Equal(RecipeData.IngredientsGroupFillingName, group.GroupName);
		Assert.Equal(2, group.Ingredients.Count);
		Assert.Equal(RecipeData.IngredientNamePeanutButter, group.Ingredients[0].Name);
		Assert.Equal(RecipeData.IngredientNameJelly, group.Ingredients[1].Name);
	}

	[Fact]
	public void IngredientsGroupConstructor_DefaultName_Test()
	{
		IngredientsGroup group = new(ingredients:
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
	public void IngredientsGroupConstructor_EmptyConstructor_Test()
	{
		IngredientsGroup group = new();

		Assert.Null(group.GroupName);
		Assert.NotNull(group.Ingredients);
		Assert.Empty(group.Ingredients);
	}

	[Fact]
	public void RecipeConstructor_Test()
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

		Assert.Equal(RecipeData.RecipeName, recipe.Name);
		Assert.Equal(2, recipe.InstructionsGroups.Count);
		Assert.Equal(RecipeData.IngredientsGroupOutsideName, recipe.IngredientsGroups[0].GroupName);
		Assert.Equal(RecipeData.IngredientsGroupFillingName, recipe.IngredientsGroups[1].GroupName);
		Assert.Equal(2, recipe.IngredientsGroups.Count);
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
		Assert.Null(recipe.IngredientsGroups[0].GroupName);
		Assert.Equal(1, recipe.IngredientsGroups.Count);
		Assert.Null(recipe.InstructionsGroups[0].GroupName);
		Assert.Empty(recipe.Tips);
	}
}
