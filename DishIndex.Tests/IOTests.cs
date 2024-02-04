using DishIndex.Core.Generators;
using DishIndex.Models;

namespace DishIndex.Tests;
public class IOTests
{
	[Fact]
	public void JsonExportImport_Test()
	{
		const string path = "recipe.json";
		File.Delete(path);

		Recipe recipe = new Recipe(RecipeData.RecipeName,
			ingredients: [
				RecipeData.IngredientPeanutButter,
				RecipeData.IngredientJelly,
				RecipeData.IngredientBread,
			],
			instructions: [
				RecipeData.InstructionPlate,
				RecipeData.InstructionPeanutButter,
				RecipeData.InstructionJelly,
				RecipeData.InstructionTogether,
			]);

		JsonRecipeIO.Export(recipe, path);

		Assert.True(File.Exists(path));

		Recipe back = JsonRecipeIO.Import(path);

		Assert.Equal(recipe.Name, back.Name);
		File.Delete(path);
	}
}
