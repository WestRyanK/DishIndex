using S = System.Text.Json;
using N = Newtonsoft.Json;

namespace DishIndex.Core.Utilities;

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
		_jsonSerializer.Formatting = N.Formatting.Indented;
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

	public static string GetSchema<T>()
	{
		N.Schema.Generation.JSchemaGenerator generator = new();
		generator.GenerationProviders.Add(new N.Schema.Generation.StringEnumGenerationProvider());
		N.Schema.JSchema schema = generator.Generate(typeof(T));
		return schema.ToString();
	}
}
