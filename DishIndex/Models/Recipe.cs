namespace DishIndex.Models;

internal class Recipe
{
    private string? _name;
    public string? Name
    {
        get => _name;
        set => _name = value;
    }

    private List<IngredientGroup> _ingredientGroups = new();
    public List<IngredientGroup> IngredientGroups
    {
        get => _ingredientGroups;
        set => _ingredientGroups = value;
    }

    private List<InstructionsGroup> _instructionsGroups = new();
    public List<InstructionsGroup> InstructionsGroups
    {
        get => _instructionsGroups;
        set => _instructionsGroups = value;
    }

    private List<string> _tips = new();
    public List<string> Tips
    {
        get => _tips;
        set => _tips = value;
    }

    public Recipe(string name, List<IngredientGroup> ingredientGroups, List<InstructionsGroup> instructionsGroups, List<string>? tips = null)
    {
        _name = name;
        _ingredientGroups = ingredientGroups;
        _instructionsGroups = instructionsGroups;
        _tips = tips ?? new();
    }

    public Recipe(string name, List<Ingredient> ingredients, List<InstructionStep> instructions, List<string>? tips = null)
    {
        _name = name;
        _ingredientGroups = [new IngredientGroup(ingredients: ingredients)];
        _instructionsGroups = [new InstructionsGroup(steps: instructions)];
        _tips = tips ?? new();
    }
}

internal class InstructionsGroup
{
    private string? _groupName = "Instructions";
    public string? GroupName
    {
        get => _groupName;
        set => _groupName = value;
    }

    private List<InstructionStep> _steps = new();
    public List<InstructionStep> Steps
    {
        get => _steps;
        set => _steps = value;
    }

    public InstructionsGroup(string groupName = "Instructions", List<InstructionStep>? steps = null)
    {
        _groupName = groupName;
        _steps = steps ?? new();
    }
}

internal class InstructionStep
{
    private string _instructions;
    public string Instructions
    {
        get => _instructions;
        set => _instructions = value;
    }

    public InstructionStep(string instructions)
    {
        _instructions = instructions;
    }
}

internal class IngredientGroup
{
    private string? _groupName = "Ingredients";
    public string? GroupName
    {
        get => _groupName;
        set => _groupName = value;
    }

    private List<Ingredient> _ingredients = new();
    public List<Ingredient> Ingredients
    {
        get => _ingredients;
        set => _ingredients = value;
    }

    public IngredientGroup(string groupName = "Ingredients", List<Ingredient>? ingredients = null)
    {
        _groupName = groupName;
        _ingredients = ingredients ?? new();
    }
}

internal class Ingredient
{
    private VolumeQuantity _quantity;

    public VolumeQuantity Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    private string? _name;
    public string? Name
    {
        get => _name;
        set => _name = value;
    }

    private string? _instruction;
    public string? Instruction
    {
        get => _instruction;
        set => _instruction = value;
    }

    public Ingredient(string name, VolumeQuantity quantity, string? instruction = null)
    {
        _name = name;
        _quantity = quantity;
        _instruction = instruction;
    }
}

