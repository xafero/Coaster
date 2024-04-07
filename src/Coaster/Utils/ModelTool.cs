using System;

namespace Coaster.Utils
{
    public static class ModelTool
    {
        public static T IfZero<T>(this T current, T defaultValue) where T : Enum
        {
            var zeroVal = default(T);
            var res = (current?.Equals(zeroVal) ?? true) ? defaultValue : current;
            return res;
        }

        public static T[] AsArray<T>(this T value)
        {
            var zeroVal = default(T);
            var res = (value?.Equals(zeroVal) ?? true) ? Array.Empty<T>() : [value];
            return res;
        }
    }
}