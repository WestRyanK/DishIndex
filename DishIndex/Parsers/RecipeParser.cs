using DishIndex.Models;
using DishIndex.Utilities;
using System.Linq;

namespace DishIndex.Parsers;

internal class RecipeParser
{
	internal const string IngredientHeader = "Ingredient";
	internal const string InstructionHeader = "Instruction";
	internal const string TipHeader = "Tip";
	public static Recipe Parse(string recipeText)
	{
		string[] recipeLines = recipeText.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		if (recipeLines.Length <= 3)
		{
			throw new FormatException("Empty Recipe");
		}

		GetRecipeSections(
			recipeLines,
			out string recipeName,
			out IEnumerable<string> ingredientsSection,
			out IEnumerable<string> instructionsSection,
			out IEnumerable<string>? tipsSection);


		return null;
	}

	internal static void GetRecipeSections(IList<string> recipeLines,
		out string recipeName,
		out IEnumerable<string> ingredientsSection,
		out IEnumerable<string> instructionsSection,
		out IEnumerable<string>? tipsSection)
	{
		recipeName = recipeLines[0];

		int ingredientsStartIndex = recipeLines.IndexOf((i, line) => i > 0 && line.Contains(IngredientHeader));
		if (ingredientsStartIndex < 0)
		{
			throw new FormatException($"Recipe missing '{IngredientHeader}' header.");
		}

		int instructionsStartIndex = recipeLines.IndexOf((i, line) => i > ingredientsStartIndex && line.Contains(InstructionHeader));
		if (instructionsStartIndex < 0)
		{
			throw new FormatException($"Recipe missing '{InstructionHeader}' header.");
		}

		int tipsStartIndex = recipeLines.IndexOf((i, line) => i > instructionsStartIndex && line.Contains(TipHeader));

		ingredientsSection = GetSection(recipeLines, ingredientsStartIndex, instructionsStartIndex);
		instructionsSection = GetSection(recipeLines, instructionsStartIndex, tipsStartIndex > 0 ? tipsStartIndex : recipeLines.Count);
		tipsSection = (tipsStartIndex > 0) ?  GetSection(recipeLines, tipsStartIndex, recipeLines.Count) : null;
	}

	internal static IEnumerable<string> GetSection(IList<string> recipeLines, int startIndex, int endIndex)
	{
		return recipeLines.Skip(startIndex).Take(endIndex - startIndex);
	}
}
