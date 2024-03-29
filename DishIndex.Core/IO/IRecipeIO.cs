﻿using DishIndex.Core.Models;

namespace DishIndex.Core.Generators;

public interface IRecipeIO
{
	public Task Export(Recipe recipe, string filePath);
}
