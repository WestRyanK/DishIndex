using DishIndex.Core.Utilities;
using S = System.Text.Json.Serialization;
using N = Newtonsoft.Json;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DishIndex.Core.Models;

[Description("The entire recipe including ingredients, instruction steps, and any tips or memories.")]
public class Recipe
{
	private string _name;
	[Description("The name or title of the recipe.")]
	public string Name
	{
		get => _name;
		set => _name = value;
	}

	private List<IngredientsGroup> _ingredientsGroups = new();
	[Description("A list of all the groups of ingredients in the recipe. If the recipe has several major components, the recipe often splits them into different groups.")]
	public List<IngredientsGroup> IngredientsGroups
	{
		get => _ingredientsGroups;
		set => _ingredientsGroups = value;
	}

	private List<InstructionsGroup> _instructionsGroups = new();
	[Description("A list of all the groups of instructions in the recipe. If the recipe has several major components, the recipe often splits them into different groups.")]
	public List<InstructionsGroup> InstructionsGroups
	{
		get => _instructionsGroups;
		set => _instructionsGroups = value;
	}

	private List<string> _tips = new();
	[Description("Any additional tips, memories, or recipe modifications belong in this section.")]
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

[Description("If a recipe has several different components, the steps are often split into separate InstructionsGroups for each component.")]
public class InstructionsGroup
{
	private string? _groupName;
	[S.JsonIgnore(Condition = S.JsonIgnoreCondition.WhenWritingNull)]
	[Description("The name of the instructions group. When there are several groups of instructions, each group is named after the component it makes. But if there's just one group, it's usually just named 'Instructions'.")]
	public string? GroupName
	{
		get => _groupName;
		set => _groupName = value;
	}

	private List<InstructionStep> _steps = new();
	[Description("A list of all the instruction steps in this instruction group, in the same order as was found in the recipe.")]
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

[Description("The instructions for a single step of the recipe.")]
public class InstructionStep
{
	private string _instructions;
	[Description("The instructions for a single step of the recipe.")]
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

[Description("If a recipe has several different components, the ingredients are often split into separate IngredientGroups for each component.")]
public class IngredientsGroup
{
	private string? _groupName;
	[S.JsonIgnore(Condition = S.JsonIgnoreCondition.WhenWritingNull)]
	[Description("The name of the ingredients group. When there are several groups of ingredients, each group is named after the component it makes. But if there's just one group, it's usually just named 'Ingredients'.")]
	public string? GroupName
	{
		get => _groupName;
		set => _groupName = value;
	}

	private List<Ingredient> _ingredients = new();
	[Description("A list of all the ingredients found in this group.")]
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

[Description("An item of food required for the recipe, specified as the name of the item, the quantity needed, and any special processing instructions.")]
public class Ingredient
{
	private VolumeQuantity? _quantity;

	[Description("The amount of the ingredient in the given unit of measurement.")]
	public VolumeQuantity? Quantity
	{
		get { return _quantity; }
		set { _quantity = value; }
	}

	private string _name;
	[Description("The name of the ingredient item excluding any quantities or processing steps.")]
	public string Name
	{
		get => _name;
		set => _name = value;
	}

	private string? _instruction;
	[S.JsonIgnore(Condition = S.JsonIgnoreCondition.WhenWritingNull)]
	[Description("Any special processing instructions included in the ingredient line.")]
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

