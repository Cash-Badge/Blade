using System;
using System.Collections.Generic;
using System.Text;

namespace Blade
{
    internal static class Utilities
    {
        public static TValue ReverseGenerateEnumValue<TValue>(this string source, Func<string, string> converter, TValue fallback = default, bool ignoreCase = true) where TValue : struct, Enum
        {
            foreach (string name in Enum.GetNames(typeof(TValue)))
            {
                if (converter.Invoke(name).Equals(source, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture))
                    return Enum.Parse<TValue>(name, ignoreCase);
            }
            return fallback;
        }

        public static TValue ReverseGenerateEnumValue<TValue>(this string source, TValue fallback = default, bool ignoreCase = true) where TValue : struct, Enum => ReverseGenerateEnumValue(source, PlaidJsonPropertyNamingPolicy.Instance.ConvertName, fallback, ignoreCase);
    }
}
