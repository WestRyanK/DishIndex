using DishIndex.Core.Utilities;
using System.Text.RegularExpressions;

namespace DishIndex.Tests;

public class SecretsTest
{
	[Fact]
	public void OpenAiApiKey_Test()
	{
		string apiKey = Secrets.OpenAiApiKey;
		Assert.NotNull(apiKey);
#pragma warning disable xUnit2008 // Do not use boolean check to match on regular expressions
		Assert.True(Regex.IsMatch(apiKey, @"^sk-\w{48}$"), "API key doesn't match expected regular expression.");
#pragma warning restore xUnit2008 // Do not use boolean check to match on regular expressions
	}
}
