namespace Sample
{
    public delegate void Callback(string message, double value);
    public record Person(string FirstName, string LastName);
    public struct Coords
    {
        public Coords(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public override string ToString() => $"({X}, {Y})";
    }

    public interface IPoint
    {
        int X { get; set; }

        int Y { get; set; }

        double Distance { get; }

        void SampleMethod();
    }

    public enum ErrorCode : ushort
    {
        None = 0,
        Unknown,
        ConnectionLost = 100,
        OutlierReading = 200
    }
}