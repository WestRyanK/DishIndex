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

	private void TestRecipeSection(IEnumerable<string> actual, int expectedCount, int expectedFirstIndex, int expectedLastIndex)
	{
		Assert.Equal(expectedCount, actual.Count());
		Assert.Equal(RecipeWithTipsLines[expectedFirstIndex], actual.First());
		Assert.Equal(RecipeWithTipsLines[expectedLastIndex], actual.Last());
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
		TestRecipeSection(ingredientsSection, 5, 3, 7);
		TestRecipeSection(instructionsSection, 3, 8, 10);
		TestRecipeSection(tipsSection!, 4, 11, 14);
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
		TestRecipeSection(ingredientsSection, 5, 3, 7);
		TestRecipeSection(instructionsSection, 3, 8, 10);
		Assert.Null(tipsSection);
	}
}
