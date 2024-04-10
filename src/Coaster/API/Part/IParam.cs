using Coaster.API.Mod;

namespace Coaster.API.Part
{
    public interface IParam : INamed, ITyped, IValued
    {
        ParamMod Mod { get; }
    }
}