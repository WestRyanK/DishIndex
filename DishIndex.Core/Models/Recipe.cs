using DishIndex.Core.Utilities;
using S = System.Text.Json.Serialization;
using N = Newtonsoft.Json;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DishIndex.Core.Models;

[Description("Defines a recipe, including its ingredients, preparation steps, helpful tips, or memories associated with the recipe.")]
public class Recipe
{
	private string _name;
	[Description("Title of the recipe.")]
	public string Name
	{
		get => _name;
		set => _name = value;
	}

	private List<IngredientsGroup> _ingredientsGroups = new();
	[Description("Categorizes ingredients into groups for complex recipes. For simple recipes, there is a single group.")]
	public List<IngredientsGroup> IngredientsGroups
	{
		get => _ingredientsGroups;
		set => _ingredientsGroups = value;
	}

	private List<InstructionsGroup> _instructionsGroups = new();
	[Description("Categorizes instructions into groups for complex recipes. For simple recipes, there is a single group.")]
	public List<InstructionsGroup> InstructionsGroups
	{
		get => _instructionsGroups;
		set => _instructionsGroups = value;
	}

	private List<string> _tips = new();
	[Description("Offers additional advice, serving suggestions, or variations.")]
	public List<string> Tips
	{
		get => _tips;
		set => _tips = value;
	}


	[S.JsonConstructor]
	[N.JsonConstructor]
	public Recipe(string name, List<IngredientsGroup> ingredientsGroups, List<InstructionsGroup> instructionsGroups, List<string>? tips = null)
	{
		_name = name;
		_ingredientsGroups = ingredientsGroups;
		_instructionsGroups = instructionsGroups;
		_tips = tips ?? new();
	}

	public Recipe(string name, List<Ingredient> ingredients, List<InstructionStep> instructions, List<string>? tips = null)
	{
		_name = name;
		_ingredientsGroups = [new IngredientsGroup(ingredients: ingredients)];
		_instructionsGroups = [new InstructionsGroup(steps: instructions)];
		_tips = tips ?? new();
	}

	public override bool Equals(object? obj)
	{
		if (obj is not Recipe other)
		{
			return false;
		}

		return
			this.Name == other.Name &&
			this.IngredientsGroups.AllEqual(other.IngredientsGroups) &&
			this.InstructionsGroups.AllEqual(other.InstructionsGroups) &&
			this.Tips.AllEqual(other.Tips);
	}
}

[Description("Organizes instructions into groups by recipe component, such as 'filling' or 'sauce'.")]
public class InstructionsGroup
{
	private string? _groupName;
	[S.JsonIgnore(Condition = S.JsonIgnoreCondition.WhenWritingNull)]
	[Description("Labels the instruction group, named after the component it makes. If there's just one group, it's named 'Instructions'.")]
	public string? GroupName
	{
		get => _groupName;
		set => _groupName = value;
	}

	private List<InstructionStep> _steps = new();
	[Description("Lists steps within this group.")]
	public List<InstructionStep> Steps
	{
		get => _steps;
		set => _steps = value;
	}

	[S.JsonConstructor]
	[N.JsonConstructor]
	public InstructionsGroup(string? groupName = null, List<InstructionStep>? steps = null)
	{
		_groupName = groupName;
		_steps = steps ?? new();
	}

	public override bool Equals(object? obj)
	{
		if (obj is not InstructionsGroup other)
		{
			return false;
		}

		return
			this.GroupName == other.GroupName &&
			this.Steps.AllEqual(other.Steps);
	}
}

[Description("Describes a single preparation step.")]
public class InstructionStep
{
	private string _instructions;
	[Description("Outlines the action to be taken.")]
	public string Instructions
	{
		get => _instructions;
		set => _instructions = value;
	}

	[S.JsonConstructor]
	[N.JsonConstructor]
	public InstructionStep(string instructions)
	{
		_instructions = instructions;
	}

	public override bool Equals(object? obj)
	{
		if (obj is not InstructionStep other)
		{
			return false;
		}

		return this.Instructions == other.Instructions;
	}
}

[Description("Groups ingredients by recipe component, such as 'filling' or 'sauce'.")]
public class IngredientsGroup
{
	private string? _groupName;
	[S.JsonIgnore(Condition = S.JsonIgnoreCondition.WhenWritingNull)]
	[Description("Labels the ingredient group, named after the component it makes. If there's just one group, it's named 'Ingredients'.")]
	public string? GroupName
	{
		get => _groupName;
		set => _groupName = value;
	}

	private List<Ingredient> _ingredients = new();
	[Description("Lists ingredients in this group.")]
	public List<Ingredient> Ingredients
	{
		get => _ingredients;
		set => _ingredients = value;
	}

	[S.JsonConstructor]
	[N.JsonConstructor]
	public IngredientsGroup(string? groupName = null, List<Ingredient>? ingredients = null)
	{
		_groupName = groupName;
		_ingredients = ingredients ?? new();
	}

	public override bool Equals(object? obj)
	{
		if (obj is not IngredientsGroup other)
		{
			return false;
		}

		return
			this.GroupName == other.GroupName &&
			this.Ingredients.AllEqual(other.Ingredients);
	}
}
[Description("Details an ingredient including its name, required amount, and preparation instructions.")]
public class Ingredient
{
	private VolumeQuantity? _quantity;

	[Description("Specifies ingredient quantity in a chosen unit.")]
	public VolumeQuantity? Quantity
	{
		get { return _quantity; }
		set { _quantity = value; }
	}

	private string _name;
	[Description("Identifies the ingredient.")]
	public string Name
	{
		get => _name;
		set => _name = value;
	}

	private string? _instruction;
	[S.JsonIgnore(Condition = S.JsonIgnoreCondition.WhenWritingNull)]
	[Description("Preparation instructions for the ingredient.")]
	public string? Instruction
	{
		get => _instruction;
		set => _instruction = value;
	}

	[S.JsonConstructor]
	[N.JsonConstructor]
	public Ingredient(string name, VolumeQuantity? quantity, string? instruction = null)
	{
		_name = name;
		_quantity = quantity;
		_instruction = instruction;
	}

	public override bool Equals(object? obj)
	{
		if (obj is not Ingredient other)
		{
			return false;
		}

		return
			this.Name == other.Name &&
			this.Instruction == other.Instruction &&
				(this.Quantity == null && other.Quantity == null ||
				this.Quantity != null && this.Quantity.Equals(other.Quantity));
	}
}

