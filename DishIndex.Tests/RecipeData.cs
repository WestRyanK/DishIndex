using DishIndex.Core.Models;

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

	public static readonly string IngredientsGroupDefaultName = "Ingredients";
	public static readonly string IngredientsGroupFillingName = "Filling Ingredients";
	public static readonly string IngredientsGroupOutsideName = "Outside Ingredients";

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


	public static readonly string RecipeTextBroccoliWreath = """
		RECIPE
		Broccoli Chicken Wreath

		Ready in 50 minutes
		Serves 8 people

		Ingredients
		½ cup broccoli
		¼ cup water chestnuts
		2 tablespoons onion
		(Optional red peppers)
		10 ounces chicken
		1 cup cheddar cheese
		1 can cream of chicken soup
		2 cans refrigerated crescent rolls
		Preparation
		Chop broccoli, water chestnuts, onion, and chicken.
		   Mix with cheese and soup. 
		On a circular pan, arrange crescent dough triangles in a circle making a wreath.   
		Scoop filling evenly into the ring, fold dough over.
		Brush dough tops with melted butter and sprinkle with parmesan cheese.
		Bake at 350 degrees for 20-30 minutes.
		Tips
		This is a Lee recipe.
		""";

	public static readonly Recipe RecipeBroccoliWreath = new("Broccoli Chicken Wreath",
		ingredientsGroups: [
			new IngredientsGroup("Ingredients", [
				new Ingredient("½ cup broccoli", null),
				new Ingredient("¼ cup water chestnuts", null),
				new Ingredient("2 tablespoons onion", null),
				new Ingredient("(Optional red peppers)", null),
				new Ingredient("10 ounces chicken", null),
				new Ingredient("1 cup cheddar cheese", null),
				new Ingredient("1 can cream of chicken soup", null),
				new Ingredient("2 cans refrigerated crescent rolls", null),
			]),
		],
		instructionsGroups: [
			new InstructionsGroup("Preparation", [
				new InstructionStep("Chop broccoli, water chestnuts, onion, and chicken."),
				new InstructionStep("Mix with cheese and soup."),
				new InstructionStep("On a circular pan, arrange crescent dough triangles in a circle making a wreath."),
				new InstructionStep("Scoop filling evenly into the ring, fold dough over."),
				new InstructionStep("Brush dough tops with melted butter and sprinkle with parmesan cheese."),
				new InstructionStep("Bake at 350 degrees for 20-30 minutes."),
			]),
		],
		tips: [
			"This is a Lee recipe."
		]);
}
