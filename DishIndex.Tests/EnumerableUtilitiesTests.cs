using DishIndex.Utilities;

namespace DishIndex.Tests;

public class EnumerableUtilitiesTests
{
	[Fact]
	public void IndexOf_Test()
	{
		List<int> list = Enumerable.Range(1, 10).ToList();

		Assert.Equal(2, list.IndexOf(x => x == 3));
		Assert.Equal(2, list.IndexOf(x => x % 3 == 0));
	}

	[Fact]
	public void IndexOf_None_Test()
	{
		List<int> list = Enumerable.Range(1, 10).ToList();

		Assert.Equal(-1, list.IndexOf(x => x == 12));
	}

	[Fact]
	public void IndexOf_Empty_Test()
	{
		List<int> list = Enumerable.Empty<int>().ToList();

		Assert.Equal(-1, list.IndexOf(x => x == 12));
	}

	[Fact]
	public void IndexOfWithIndex_Test()
	{
		List<int> list = Enumerable.Range(1, 10).ToList();

		Assert.Equal(3, list.IndexOf((i, x) => i + x == 7));
	}

	[Fact]
	public void IndexOfWithIndex_None_Test()
	{
		List<int> list = Enumerable.Range(1, 10).ToList();

		Assert.Equal(-1, list.IndexOf((i, x) => i + x == 40));
	}

	[Fact]
	public void IndexOfWithIndex_Empty_Test()
	{
		List<int> list = Enumerable.Empty<int>().ToList();

		Assert.Equal(-1, list.IndexOf((i, x) => i + x == 12));
	}
}
