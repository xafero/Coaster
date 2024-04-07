using Coaster.API.Part;

namespace Coaster.API.Tree
{
    public interface IApplier
    {
        void Apply(IHasMembers owner);
    }
}