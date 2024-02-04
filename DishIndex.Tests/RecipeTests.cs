using DishIndex.Models;

namespace DishIndex.Tests;

public class RecipeTests_Constructor
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

public class RecipeTests_Equality
{
	[Fact]
	public void VolumeQuantityEquality_Test()
	{
		Assert.False(new VolumeQuantity(0, VolumeUnit.None).Equals(null));
		Assert.False(new VolumeQuantity(0, VolumeUnit.None).Equals(new object()));
		Assert.False(new VolumeQuantity(3, VolumeUnit.None).Equals(new VolumeQuantity(3, VolumeUnit.Milliliter)));
		Assert.False(new VolumeQuantity(3, VolumeUnit.None).Equals(new VolumeQuantity(7, VolumeUnit.None)));
		Assert.True(new VolumeQuantity(5, VolumeUnit.PintUS).Equals(new VolumeQuantity(5, VolumeUnit.PintUS)));
		Assert.True(new VolumeQuantity(0, VolumeUnit.None).Equals(new VolumeQuantity(0, VolumeUnit.None)));
		Assert.False(new VolumeQuantity(1, VolumeUnit.TablespoonUS).Equals(new VolumeQuantity(3, VolumeUnit.TeaspoonUS)), "Equivalent but differently expressed quantities should still be considered different.");
	}

	[Fact]
	public void IngredientEquality_Test()
	{
		Ingredient ingredient = new("A", new(0, VolumeUnit.None), "B");
		Ingredient ingredientNullQuantity = new("A", null, "B");

		Assert.False(ingredient.Equals(null));
		Assert.False(ingredient.Equals(new object()));
		Assert.False(ingredient.Equals(new Ingredient("A2", new(0, VolumeUnit.None), "B")));
		Assert.False(ingredient.Equals(new Ingredient("A", new(0, VolumeUnit.None), "B2")));
		Assert.False(ingredient.Equals(new Ingredient("A", new(1, VolumeUnit.None), "B")));
		Assert.False(ingredientNullQuantity.Equals(new Ingredient("A", new(0, VolumeUnit.None), "B")));
		Assert.False(ingredient.Equals(new Ingredient("A", null, "B")));
		Assert.True(ingredientNullQuantity.Equals(new Ingredient("A", null, "B")));
		Assert.True(ingredient.Equals(new Ingredient("A", new(0, VolumeUnit.None), "B")));
	}

	[Fact]
	public void IngredientsGroupEquality_Test()
	{
		IngredientsGroup group = new("A", [
			new("B", null),
			new("C", null),
		]);
		IngredientsGroup groupEmpty = new("A", []);
		IngredientsGroup groupNullIngredients = new("A", null);
		IngredientsGroup groupNullName = new(null, null);


		Assert.False(group.Equals(null));
		Assert.False(group.Equals(new object()));
		Assert.False(group.Equals(new IngredientsGroup("D", group.Ingredients)));
		Assert.False(group.Equals(new IngredientsGroup("D", [group.Ingredients[0], group.Ingredients[1]])));
		Assert.False(group.Equals(new IngredientsGroup("A", null)));
		Assert.False(group.Equals(new IngredientsGroup("A", [new("F", null)])));
		Assert.False(groupEmpty.Equals(group));
		Assert.False(groupNullIngredients.Equals(group));
		Assert.False(groupNullName.Equals(group));
		Assert.True(group.Equals(new IngredientsGroup("A", [new("B", null), new("C", null)])));
		Assert.True(groupEmpty.Equals(new IngredientsGroup("A", [])));
		Assert.True(groupNullIngredients.Equals(new IngredientsGroup("A", null)));
		Assert.True(groupNullName.Equals(new IngredientsGroup(null, null)));
	}

	[Fact]
	public void InstructionStepEquality_Test()
	{
		InstructionStep step = new InstructionStep("A");

		Assert.False(step.Equals(null));
		Assert.False(step.Equals(new object()));
		Assert.False(step.Equals(new InstructionStep("B")));
		Assert.True(step.Equals(new InstructionStep("A")));
	}

	[Fact]
	public void InstructionsGroupEquality_Test()
	{
		InstructionsGroup group = new("A", [
			new InstructionStep("B"),
			new InstructionStep("C"),
		]);
		InstructionsGroup groupEmpty = new("A", []);
		InstructionsGroup groupNullIngredients = new("A", null);
		InstructionsGroup groupNullName = new(null, null);


		Assert.False(group.Equals(null));
		Assert.False(group.Equals(new object()));
		Assert.False(group.Equals(new InstructionsGroup("D", group.Steps)));
		Assert.False(group.Equals(new InstructionsGroup("D", [group.Steps[0], group.Steps[1]])));
		Assert.False(group.Equals(new InstructionsGroup("A", null)));
		Assert.False(group.Equals(new InstructionsGroup("A", [new("F")])));
		Assert.False(groupEmpty.Equals(group));
		Assert.False(groupNullIngredients.Equals(group));
		Assert.False(groupNullName.Equals(group));
		Assert.True(group.Equals(new InstructionsGroup("A", [new("B"), new("C")])));
		Assert.True(groupEmpty.Equals(new InstructionsGroup("A", [])));
		Assert.True(groupNullIngredients.Equals(new InstructionsGroup("A", null)));
		Assert.True(groupNullName.Equals(new InstructionsGroup(null, null)));
	}

	[Fact]
	public void RecipeEquality_Test()
	{
		Recipe recipe = new Recipe("A",
			ingredientsGroups: [
				new IngredientsGroup("B", [
					new Ingredient("C", null)
				]),
				new IngredientsGroup("D", [
					new Ingredient("E", null)
				])
			],
			instructionsGroups: [
				new InstructionsGroup("F", [
					new InstructionStep("G"),
					new InstructionStep("H"),
				]),
				new InstructionsGroup("I", [
					new InstructionStep("J"),
					new InstructionStep("K"),
				])
			],
			tips: [
				"L"
			]);

		Assert.False(recipe.Equals(null));
		Assert.False(recipe.Equals(new object()));
		Assert.False(recipe.Equals(new Recipe("A2", recipe.IngredientsGroups, recipe.InstructionsGroups, recipe.Tips)));
		Assert.False(recipe.Equals(new Recipe("A", [], recipe.InstructionsGroups, recipe.Tips)));
		Assert.False(recipe.Equals(new Recipe("A", recipe.IngredientsGroups, [], recipe.Tips)));
		Assert.False(recipe.Equals(new Recipe("A", recipe.IngredientsGroups, recipe.InstructionsGroups, null)));
		Assert.False(new Recipe("A", [], recipe.InstructionsGroups, recipe.Tips).Equals(recipe));
		Assert.False(new Recipe("A", recipe.IngredientsGroups, [], recipe.Tips).Equals(recipe));
		Assert.False(new Recipe("A", recipe.IngredientsGroups, recipe.InstructionsGroups, null).Equals(recipe));
		Assert.True(recipe.Equals(new Recipe("A",
			ingredientsGroups: [
				new IngredientsGroup("B", [
					new Ingredient("C", null)
				]),
				new IngredientsGroup("D", [
					new Ingredient("E", null)
				])
			],
			instructionsGroups: [
				new InstructionsGroup("F", [
					new InstructionStep("G"),
					new InstructionStep("H"),
				]),
				new InstructionsGroup("I", [
					new InstructionStep("J"),
					new InstructionStep("K"),
				])
			],
			tips: [
				"L"
			])));
	}
}
