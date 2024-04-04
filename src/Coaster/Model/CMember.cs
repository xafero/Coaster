using Coaster.API;

namespace Coaster.Model
{
    public abstract class CMember : IHasName
    {
        public string Name { get; set; }
    }
}