using DishIndex.Models;
using DishIndex.Parsers;
using System.Runtime.CompilerServices;

namespace DishIndex.Tests;

public class RecipeParserTests
{
	private static readonly IList<string> RecipeNoTipsLines = [
		"0: Recipe Name",
		"1: Garbage 1",
		"2: Garbage 2",
		"3: An Ingredients Title",
		"4: Ingredient 1",
		"5: Ingredient 2",
		"6: Ingredient 3",
		"7: Ingredient 4",
		"8: Instructions Start Here",
		"9: Instruction 1",
		"10: Instruction 2",
	];

	private static readonly IList<string> TipsLines = [
		"11: Tips",
		"12: Tip #1",
		"13: Tip #2",
		"14: Tip #3",
	];

	private static readonly IList<string> RecipeWithTipsLines = [.. RecipeNoTipsLines, .. TipsLines];

	private void TestGetSection(IEnumerable<string> result, int expectedCount, int expectedFirst, int expectedLast)
	{
		Assert.Equal(expectedCount, result.Count());
		Assert.Equal(expectedFirst.ToString(), result.First());
		Assert.Equal(expectedLast.ToString(), result.Last());
	}

	[Fact]
	public void GetSection_Test()
	{
		IList<string> input = Enumerable.Range(0, 10).Select(x => x.ToString()).ToList();
		TestGetSection(RecipeParser.GetSection(input, 0, 10), 10, 0, 9);
		TestGetSection(RecipeParser.GetSection(input, 1, 5), 4, 1, 4);
	}

	private void TestRecipeSection(IEnumerable<string> actual, int expectedCount, int expectedFirstIndex, int expectedLastIndex, IList<string> recipe)
	{
		Assert.Equal(expectedCount, actual.Count());
		Assert.Equal(recipe[expectedFirstIndex], actual.First());
		Assert.Equal(recipe[expectedLastIndex], actual.Last());
	}

	[Fact]
	public void GetRecipeSections_Test()
	{
		RecipeParser.GetRecipeSections(
			RecipeWithTipsLines,
			out string recipeName,
			out IEnumerable<string> ingredientsSection,
			out IEnumerable<string> instructionsSection,
			out IEnumerable<string>? tipsSection);

		Assert.Equal(RecipeWithTipsLines[0], recipeName);
		TestRecipeSection(ingredientsSection, 5, 3, 7, RecipeWithTipsLines);
		TestRecipeSection(instructionsSection, 3, 8, 10, RecipeWithTipsLines);
		TestRecipeSection(tipsSection!, 4, 11, 14, RecipeWithTipsLines);
	}

	[Fact]
	public void GetRecipeSections_NoTips_Test()
	{
		RecipeParser.GetRecipeSections(
			RecipeNoTipsLines,
			out string recipeName,
			out IEnumerable<string> ingredientsSection,
			out IEnumerable<string> instructionsSection,
			out IEnumerable<string>? tipsSection);

		Assert.Equal(RecipeWithTipsLines[0], recipeName);
		TestRecipeSection(ingredientsSection, 5, 3, 7, RecipeNoTipsLines);
		TestRecipeSection(instructionsSection, 3, 8, 10, RecipeNoTipsLines);
		Assert.Null(tipsSection);
	}

	[Fact]
	public void GetRecipeSections_AlternateNames_Test()
	{
		IList<string> alternateRecipeLines = RecipeWithTipsLines.ToList();
		alternateRecipeLines[8] = "8: Preparations";
		alternateRecipeLines[11] = "11: Memories";

		RecipeParser.GetRecipeSections(
			alternateRecipeLines,
			out string recipeName,
			out IEnumerable<string> ingredientsSection,
			out IEnumerable<string> instructionsSection,
			out IEnumerable<string>? tipsSection);

		Assert.Equal(alternateRecipeLines[0], recipeName);
		TestRecipeSection(ingredientsSection, 5, 3, 7, alternateRecipeLines);
		TestRecipeSection(instructionsSection, 3, 8, 10, alternateRecipeLines);
		TestRecipeSection(tipsSection!, 4, 11, 14, alternateRecipeLines);
	}

	[Fact]
	public void ParseInstructionsSection_Test()
	{
		InstructionsGroup group = RecipeParser.ParseInstructionsGroup(RecipeNoTipsLines.Skip(8));
		Assert.Equal(RecipeNoTipsLines[8], group.GroupName);
		Assert.Equal(2, group.Steps.Count);
		Assert.Equal(RecipeNoTipsLines[9], group.Steps[0].Instructions);
		Assert.Equal(RecipeNoTipsLines[10], group.Steps[1].Instructions);
	}


	[Fact]
	public void ParseIngredientsSection_Test()
	{
		IngredientsGroup group = RecipeParser.ParseIngredientsGroup(RecipeNoTipsLines.Skip(3).Take(5));
		Assert.Equal(RecipeNoTipsLines[3], group.GroupName);
		Assert.Equal(4, group.Ingredients.Count);
		Assert.Equal(RecipeNoTipsLines[4], group.Ingredients[0].Name);
		Assert.Equal(RecipeNoTipsLines[5], group.Ingredients[1].Name);
		Assert.Equal(RecipeNoTipsLines[6], group.Ingredients[2].Name);
		Assert.Equal(RecipeNoTipsLines[7], group.Ingredients[3].Name);
	}

	[Fact]
	public void ParseTips_Test()
	{
		List<string> tips = RecipeParser.ParseTips(TipsLines);
		Assert.Equal(3, tips.Count);
		Assert.Equal(RecipeWithTipsLines[12], tips[0]);
		Assert.Equal(RecipeWithTipsLines[13], tips[1]);
		Assert.Equal(RecipeWithTipsLines[14], tips[2]);
	}

	[Fact]
	public void ParseTips_Null_Test()
	{
		List<string> tips = RecipeParser.ParseTips(null);
		Assert.Empty(tips);
	}

	[Fact]
	public void ParseRecipe_Test()
	{
		string recipeString = string.Join("\n", RecipeWithTipsLines);
		Recipe recipe = RecipeParser.Parse(recipeString);

		Assert.Equal(RecipeWithTipsLines[0], recipe.Name);

		Assert.Equal(1, recipe.IngredientsGroups.Count);
		Assert.Equal(RecipeWithTipsLines[3], recipe.IngredientsGroups[0].GroupName);
		Assert.Equal(4, recipe.IngredientsGroups[0].Ingredients.Count);
		Assert.Equal(RecipeWithTipsLines[4], recipe.IngredientsGroups[0].Ingredients[0].Name);
		Assert.Equal(RecipeWithTipsLines[5], recipe.IngredientsGroups[0].Ingredients[1].Name);
		Assert.Equal(RecipeWithTipsLines[6], recipe.IngredientsGroups[0].Ingredients[2].Name);
		Assert.Equal(RecipeWithTipsLines[7], recipe.IngredientsGroups[0].Ingredients[3].Name);

		Assert.Equal(1, recipe.InstructionsGroups.Count);
		Assert.Equal(RecipeWithTipsLines[8], recipe.InstructionsGroups[0].GroupName);
		Assert.Equal(2, recipe.InstructionsGroups[0].Steps.Count);
		Assert.Equal(RecipeWithTipsLines[9], recipe.InstructionsGroups[0].Steps[0].Instructions);
		Assert.Equal(RecipeWithTipsLines[10], recipe.InstructionsGroups[0].Steps[1].Instructions);

		Assert.Equal(3, recipe.Tips.Count);
		Assert.Equal(RecipeWithTipsLines[12], recipe.Tips[0]);
		Assert.Equal(RecipeWithTipsLines[13], recipe.Tips[1]);
		Assert.Equal(RecipeWithTipsLines[14], recipe.Tips[2]);
	}

	[Fact]
	public void ParseRecipe_NoTips_Test()
	{
		string recipeString = string.Join("\n", RecipeNoTipsLines);
		Recipe recipe = RecipeParser.Parse(recipeString);

		Assert.Equal(RecipeWithTipsLines[0], recipe.Name);

		Assert.Equal(1, recipe.IngredientsGroups.Count);
		Assert.Equal(RecipeWithTipsLines[3], recipe.IngredientsGroups[0].GroupName);
		Assert.Equal(4, recipe.IngredientsGroups[0].Ingredients.Count);
		Assert.Equal(RecipeWithTipsLines[4], recipe.IngredientsGroups[0].Ingredients[0].Name);
		Assert.Equal(RecipeWithTipsLines[5], recipe.IngredientsGroups[0].Ingredients[1].Name);
		Assert.Equal(RecipeWithTipsLines[6], recipe.IngredientsGroups[0].Ingredients[2].Name);
		Assert.Equal(RecipeWithTipsLines[7], recipe.IngredientsGroups[0].Ingredients[3].Name);

		Assert.Equal(1, recipe.InstructionsGroups.Count);
		Assert.Equal(RecipeWithTipsLines[8], recipe.InstructionsGroups[0].GroupName);
		Assert.Equal(2, recipe.InstructionsGroups[0].Steps.Count);
		Assert.Equal(RecipeWithTipsLines[9], recipe.InstructionsGroups[0].Steps[0].Instructions);
		Assert.Equal(RecipeWithTipsLines[10], recipe.InstructionsGroups[0].Steps[1].Instructions);

		Assert.Equal(0, recipe.Tips.Count);
	}
}
