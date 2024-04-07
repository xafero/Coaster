using Coaster.API.Tree;

namespace Coaster.API.Top
{
    public interface IProperty : INamed, ITyped, IVisible, IMember, IApplier
    {
        PropMode Mode { get; }
    }
}