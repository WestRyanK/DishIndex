
using DishIndex.Models;
using System.Text.Json;

namespace DishIndex.Core.Generators;

public class JsonRecipeIO
{
	public static async Task Export(Recipe recipe, string filePath)
	{
		using FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
		await JsonSerializer.SerializeAsync(stream, recipe);
	}

	public static async Task<Recipe> Import(string filePath)
	{
		using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		Recipe? recipe = await JsonSerializer.DeserializeAsync<Recipe>(stream);
		return recipe!;
	}
}
