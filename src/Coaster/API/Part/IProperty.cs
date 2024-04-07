using Coaster.API.Mod;
using Coaster.API.Tree;

namespace Coaster.API.Part
{
    public interface IProperty : INamed, ITyped, IVisible, IMember, IApplier
    {
        PropMode Mode { get; }
    }
}