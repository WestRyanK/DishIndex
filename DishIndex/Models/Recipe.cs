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
}

internal class InstructionsGroup
{
    private string? _groupName;
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
}

internal class InstructionStep
{
    private string? _instructions;
    public string? Instructions
    {
        get => _instructions;
        set => _instructions = value;
    }
}

internal class IngredientGroup
{
    private string? _groupName;
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
}

internal class Ingredient
{
    private Quantity _quantity;

    public Quantity Quantity
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
}

internal struct Quantity
{
    private double _amount;

    public double Amount
    {
        get => _amount;
        set => _amount = value;
    }

    private Unit _unit;

    public Unit Unit
    {
        get => _unit;
        set => _unit = value;
    }
}

internal enum Unit
{
    None,
    Teaspoon,
    Tablespoon,
    Cup,
    Pint,
    Quart,
    Gallon,
    Item,
}
