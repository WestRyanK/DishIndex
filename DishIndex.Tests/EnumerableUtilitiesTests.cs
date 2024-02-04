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

	[Fact]
	public void AllEqual_Test()
	{
		List<string?> left = ["A", "B", "C"];

		Assert.True(left.AllEqual(["A", "B", "C"]));
		Assert.False(left.AllEqual(["A", "B", "D"]));
		Assert.False(left.AllEqual(["A", "B"]));
		Assert.False(left.AllEqual(["A", "B", null]));
		Assert.False(left.AllEqual(null));
	}

	[Fact]
	public void AllEqual_Empty_Test()
	{
		List<string?> left = [];

		Assert.True(left.AllEqual([]));
		Assert.False(left.AllEqual(["A", "B"]));
		Assert.False(left.AllEqual(null));
	}

	[Fact]
	public void AllEqual_Null_Test()
	{
		List<string?>? left = null;

		Assert.True(left.AllEqual(null));
		Assert.False(left.AllEqual(["A", "B"]));
		Assert.False(left.AllEqual([]));
	}

	[Fact]
	public void AllEqual_NullEntry_Test()
	{
		List<string?> left = ["A", null, "B", "C"];

		Assert.True(left.AllEqual(["A", null, "B", "C"]));
		Assert.False(left.AllEqual(["A", null, "B", "D"]));
		Assert.False(left.AllEqual(["A", null, "B"]));
		Assert.False(left.AllEqual(["A", "B", null]));
		Assert.False(left.AllEqual(null));
	}
}
