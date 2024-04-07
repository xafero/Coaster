using Coaster.API.Mod;
using Coaster.API.Part;

namespace Coaster.API.Top
{
    public interface IRecord : INamed, IHasInterfaces, IHasParameters, IMember, IVisible,
        IInherited, IHasBase, IHasMembers
    {
        RecMode Mode { get; }
    }
}