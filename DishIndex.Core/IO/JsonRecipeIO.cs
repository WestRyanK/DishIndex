
using DishIndex.Models;
using S = System.Text.Json;
using N = Newtonsoft.Json;
using System.Text.Json;

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

public interface IJsonSerializer
{
	public string Serialize<T>(T obj);

	public T Deserialize<T>(string json);
}

public class SystemJsonSerializer : IJsonSerializer
{
	private static readonly S.JsonSerializerOptions _SerializerOptions = new S.JsonSerializerOptions(S.JsonSerializerDefaults.General)
	{
		WriteIndented = true,
	};

	public T Deserialize<T>(string json)
	{
		return S.JsonSerializer.Deserialize<T>(json)!;
	}

	public string Serialize<T>(T obj)
	{
		return S.JsonSerializer.Serialize(obj, _SerializerOptions);
	}
}

public class NewtonsoftJsonSerializer : IJsonSerializer
{
	private N.JsonSerializer _jsonSerializer;

	public NewtonsoftJsonSerializer()
	{
		_jsonSerializer = new N.JsonSerializer();
		_jsonSerializer.NullValueHandling = N.NullValueHandling.Ignore;
	}
	public T Deserialize<T>(string json)
	{
		using StringReader reader = new StringReader(json);
		using N.JsonTextReader jsonReader = new N.JsonTextReader(reader);
		return _jsonSerializer.Deserialize<T>(jsonReader)!;
	}

	public string Serialize<T>(T obj)
	{
		using StringWriter writer = new StringWriter();
		_jsonSerializer.Serialize(writer, obj);
		return writer.ToString();
	}
}

