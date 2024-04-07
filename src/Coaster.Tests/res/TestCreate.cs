using System.Runtime.Serialization;

namespace Example
{
    public class Person : System.Runtime.Serialization.IExtensibleDataObject
    {
        private long serialVersionUID;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ExtensionDataObject ExtensionData { get; set; }

        public void SetIt()
        {
        }

        public event System.EventHandler WebOpened;
    }

    public enum Funny
    {
    }

    public interface IConductor
    {
    }

    public struct Half
    {
    }

    public record DailyTemperature();
    public delegate void EventHandler();
}