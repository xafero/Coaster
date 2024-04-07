using System;

namespace Coaster.Utils
{
    public static class ModelTool
    {
        public static T IfZero<T>(this T current, T defaultValue) where T : Enum
        {
            var zeroVal = default(T);
            var res = current.Equals(zeroVal) ? defaultValue : current;
            return res;
        }
    }
}