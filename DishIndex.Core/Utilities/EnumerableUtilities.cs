namespace DishIndex.Core.Utilities;

internal static class EnumerableUtilities
{
	public static int IndexOf<T>(this IEnumerable<T> values, Func<T, bool> predicate)
	{
		int i = 0;
		IEnumerator<T> enumerator = values.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (predicate(enumerator.Current))
			{
				return i;
			}
			i++;
		}
		return -1;
	}

	public static int IndexOf<T>(this IEnumerable<T> values, Func<int, T, bool> predicate)
	{
		int i = 0;
		IEnumerator<T> enumerator = values.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (predicate(i, enumerator.Current))
			{
				return i;
			}
			i++;
		}
		return -1;
	}

	public static bool AllEqual<T>(this IEnumerable<T>? values, IEnumerable<T>? otherValues)
	{

		if (values == null && otherValues == null)
		{
			return true;
		}
		if (values == null || otherValues == null)
		{
			return false;
		}

		if (values.Count() != otherValues.Count())
		{
			return false;
		}

		return values.Zip(otherValues).All(pair => pair.First == null && pair.Second == null || pair.First != null && pair.First.Equals(pair.Second));
	}
}
