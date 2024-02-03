﻿using DishIndex.Models;
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

		IngredientsGroup ingredientsGroup = ParseIngredientsGroup(ingredientsSection);
		InstructionsGroup instructionsGroup = ParseInstructionsGroup(instructionsSection);
		List<string> tips = ParseTips(tipsSection);

		Recipe recipe = new Recipe(recipeName, [ingredientsGroup], [instructionsGroup], tips);
		return recipe;
	}

	internal static InstructionsGroup ParseInstructionsGroup(IEnumerable<string> section)
	{
		var instructions = section.Skip(1).Select(line => new InstructionStep(line)).ToList();
		InstructionsGroup group = new InstructionsGroup(section.First(), instructions);
		return group;
	}

	internal static IngredientsGroup ParseIngredientsGroup(IEnumerable<string> ingredientsSection)
	{
		List<Ingredient> ingredients = ingredientsSection.Skip(1).Select(ParseIngredient).ToList();
		IngredientsGroup group = new IngredientsGroup(ingredientsSection.First(), ingredients);
		return group;
	}

	internal static Ingredient ParseIngredient(string line)
	{
		Ingredient ingredient = new Ingredient(line, null, null);
		return ingredient;
	}

	internal static List<string> ParseTips(IEnumerable<string>? tipsSection)
	{
		List<string> tips = tipsSection?.Skip(1)?.ToList() ?? new();
		return tips;
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
