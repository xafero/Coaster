using Coaster.API.Mod;
using Coaster.API.Tree;

namespace Coaster.API.Part
{
    public interface IProperty : INamed, ITyped, IVisible, IMember, IApplier, IInherited
    {
        PropMode Mode { get; }

        IBody Get { get; }

        IBody Init { get; }

        IBody Set { get; }
    }
}