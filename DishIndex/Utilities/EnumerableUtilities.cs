namespace DishIndex.Utilities;

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
}
