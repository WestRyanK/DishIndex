using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DishIndex.Core.Utilities;

internal class Secrets
{

	private static readonly IConfigurationRoot _config = new ConfigurationBuilder()
		.AddUserSecrets(Assembly.GetExecutingAssembly())
		.Build();

	private const string OpenAiApiKeyName = "OpenAiApiKey";
	public static string OpenAiApiKey => _config[OpenAiApiKeyName];
}
