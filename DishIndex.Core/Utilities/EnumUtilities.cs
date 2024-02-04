using System.Diagnostics.CodeAnalysis;

namespace DishIndex.Core.Utilities;

internal static class EnumUtilities
{
    public static bool TryGetEnumAttribute<E, A>(this E enumMember, [NotNullWhen(true)] out A? attribute) where E : Enum where A : Attribute
    {
        attribute =
            (A?)typeof(E)
            .GetMember(enumMember.ToString())
            .FirstOrDefault()
            ?.GetCustomAttributes(typeof(A), false)
            ?.FirstOrDefault();

        return attribute != null;
    }
}
