using Coaster.API.Mod;
using Coaster.API.Tree;

namespace Coaster.API.Part
{
    public interface IOperator : IHasBody, IMember, ITyped, IHasParameters, IVisible, IModified
    {
        OpMode Kind { get; }
    }
}