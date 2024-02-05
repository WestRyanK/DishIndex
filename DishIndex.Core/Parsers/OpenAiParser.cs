using Azure;
using Azure.AI.OpenAI;
using DishIndex.Core.Generators;
using DishIndex.Core.Models;
using DishIndex.Core.Utilities;

namespace DishIndex.Core.Parsers;

public class OpenAiParser
{

	public static Recipe Parse(string recipeText)
	{
		string json = FormatRecipeAsJson(recipeText);
		Recipe recipe = JsonRecipeIO.JsonSerializer.Deserialize<Recipe>(json);
		return recipe;
	}

	public static string FormatRecipeAsJson(string recipeText)
	{
		OpenAIClient client = new(Secrets.OpenAiApiKey);

		ChatCompletionsOptions options = new()
		{
			DeploymentName = "gpt-4",
			Messages =
			{
				new ChatRequestSystemMessage(
					"Convert the recipe into json data. " +
					"Do not include any extra commentary with the json response. " +
					"If an ingredient 'Unit' does not match any value in the 'Unit' enum of the json schema, use a value of 'None'. " +
					"Use the following json schema to format the response:\n\n" +
					NewtonsoftJsonSerializer.GetSchema<Recipe>()),

				new ChatRequestUserMessage(recipeText),
			}
		};

		Response<ChatCompletions> response = client.GetChatCompletions(options);
		ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
		return responseMessage.Content;
	}
}
