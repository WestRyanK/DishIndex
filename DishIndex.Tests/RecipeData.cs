using DishIndex.Models;

namespace DishIndex.Tests;

internal class RecipeData
{
	public static readonly string StepStringPlate = "Place two slices of bread beside each other on a plate.";
	public static readonly string StepStringPeanutButter = "Spread peanut butter on one side of both slices of bread.";
	public static readonly string StepStringJelly = "Spread jelly on one of the slices of bread.";
	public static readonly string StepStringTogether = "Place the two slices of bread together with fillings on the inside.";
	public static readonly string StepStringEasy = "You know what to do. Why do you need instructions?";

	public static readonly string InstructionsGroupDefaultName = "Instructions";
	public static readonly string InstructionsGroupComplicatedName = "Complicated Instructions";
	public static readonly string InstructionsGroupEasyName = "Easy Instructions";

	public static readonly string IngredientGroupDefaultName = "Ingredients";
	public static readonly string IngredientGroupFillingName = "Filling Ingredients";
	public static readonly string IngredientGroupOutsideName = "Outside Ingredients";

	public static readonly string IngredientNamePeanutButter = "Peanut Butter";
	public static readonly string IngredientNameJelly = "Grape Jelly";
	public static readonly string IngredientNameBread = "White Bread";
	public static readonly string IngredientInstructionPeanutButter = "Extra Crunchy";
	public static readonly string IngredientInstructionBread = "Sliced Thick";

	public static readonly VolumeQuantity QuantityPeanutButter = new(3, VolumeUnit.TablespoonUS);
	public static readonly VolumeQuantity QuantityJelly = new(4, VolumeUnit.TeaspoonUS);
	public static readonly VolumeQuantity QuantityBread = new(2, VolumeUnit.None);


	public static readonly Ingredient IngredientBread = new Ingredient(
		IngredientNameBread,
		QuantityBread,
		IngredientInstructionBread);
	public static readonly Ingredient IngredientPeanutButter = new Ingredient(
		IngredientNamePeanutButter,
		QuantityPeanutButter,
		IngredientInstructionPeanutButter);
	public static readonly Ingredient IngredientJelly = new Ingredient(
		IngredientNameJelly,
		QuantityJelly);

	public static readonly InstructionStep InstructionPlate = new InstructionStep(StepStringPlate);
	public static readonly InstructionStep InstructionPeanutButter = new InstructionStep(StepStringPeanutButter);
	public static readonly InstructionStep InstructionJelly = new InstructionStep(StepStringJelly);
	public static readonly InstructionStep InstructionTogether = new InstructionStep(StepStringTogether);
	public static readonly InstructionStep InstructionEasy = new InstructionStep(StepStringEasy);

	public static readonly string RecipeName = "Peanut Butter and Jelly Sandwich";

	public static readonly string TipBanana = "Try adding banana to the sandwich!";
}
