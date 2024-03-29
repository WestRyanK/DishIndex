﻿using DishIndex.Core.Models;
using DishIndex.Core.Utilities;
using System.Text.RegularExpressions;

namespace DishIndex.Parsers;

internal class RecipeParser
{
	internal static readonly string[] RecipeHeaders = ["^Recipe$"];
	internal static readonly string[] IngredientHeaders = ["Ingredient"];
	internal static readonly string[] InstructionHeaders = ["Instruction", "Preparation"];
	internal static readonly string[] TipHeaders = ["Tip", "Memories"];
	public static Recipe Parse(string recipeText)
	{
		string[] recipeLines = GetRecipeLines(recipeText);
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

	internal static string[] GetRecipeLines(string recipeText)
	{
		return recipeText.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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
		int recipeNameStartIndex = recipeLines.IndexOf(line => !IsHeader(RecipeHeaders, line));
		recipeName = recipeLines[recipeNameStartIndex];

		int ingredientsStartIndex = recipeLines.IndexOf((i, line) => i > recipeNameStartIndex + 1 && IsHeader(IngredientHeaders, line));
		if (ingredientsStartIndex < 0)
		{
			throw new FormatException($"Recipe missing '{IngredientHeaders[0]}' header.");
		}

		int instructionsStartIndex = recipeLines.IndexOf((i, line) => i > ingredientsStartIndex && IsHeader(InstructionHeaders, line));
		if (instructionsStartIndex < 0)
		{
			throw new FormatException($"Recipe missing '{InstructionHeaders[0]}' header.");
		}

		int tipsStartIndex = recipeLines.IndexOf((i, line) => i > instructionsStartIndex && IsHeader(TipHeaders, line));

		ingredientsSection = GetSection(recipeLines, ingredientsStartIndex, instructionsStartIndex);
		instructionsSection = GetSection(recipeLines, instructionsStartIndex, tipsStartIndex > 0 ? tipsStartIndex : recipeLines.Count);
		tipsSection = (tipsStartIndex > 0) ? GetSection(recipeLines, tipsStartIndex, recipeLines.Count) : null;
	}

	internal static bool IsHeader(string[] regexes, string line) => regexes.Any(r => Regex.IsMatch(line, r, RegexOptions.IgnoreCase));

	internal static IEnumerable<string> GetSection(IList<string> recipeLines, int startIndex, int endIndex)
	{
		return recipeLines.Skip(startIndex).Take(endIndex - startIndex);
	}
}
