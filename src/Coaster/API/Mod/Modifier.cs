using System;

namespace Coaster.API
{
    [Flags]
    public enum Modifier
    {
        None = 0,

        Readonly = 1,

        Static = 1 << 1
    }
}