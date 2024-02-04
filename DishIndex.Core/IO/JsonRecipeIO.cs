
using DishIndex.Core.Utilities;
using DishIndex.Core.Models;

namespace DishIndex.Core.Generators;

public class JsonRecipeIO
{
	public static IJsonSerializer JsonSerializer { get; set; } = new NewtonsoftJsonSerializer();

	public static void Export(Recipe recipe, string filePath)
	{
		using FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
		using StreamWriter writer = new StreamWriter(stream);
		string json = JsonSerializer.Serialize(recipe);
		writer.Write(json);
	}

	public static Recipe Import(string filePath)
	{
		using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		using StreamReader reader = new StreamReader(stream);
		Recipe? recipe = JsonSerializer.Deserialize<Recipe>(reader.ReadToEnd());
		return recipe!;
	}
}

