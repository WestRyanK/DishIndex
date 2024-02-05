using DishIndex.Core.Parsers;

namespace DishIndex.Tests;

public class OpenAiParserTests
{
	[Fact]
	public void FormatRecipeAsJson_Test()
	{
		string json = OpenAiParser.FormatRecipeAsJson(RecipeData.RecipeTextBroccoliWreath);
	}
}
