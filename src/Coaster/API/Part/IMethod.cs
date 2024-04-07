using Coaster.API.Mod;
using Coaster.API.Tree;

namespace Coaster.API.Part
{
    public interface IMethod : INamed, ITyped, IVisible, IHasParameters,
        IHasBody, IMember, IApplier, IModified, IInherited
    {
    }
}