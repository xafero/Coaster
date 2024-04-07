using Coaster.API.Tree;

namespace Coaster.API.Top
{
    public interface IMethod : INamed, ITyped, IVisible, IHasParameters, IHasBody, IMember, IApplier
    {
    }
}