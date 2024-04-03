namespace Coaster.Impl
{
    public static class Coast
    {
        public static T Create<T>() where T : new()
        {
            return new T();
        }

        public static T Parse<T>(string code) where T : new()
        {
            // TODO 
            return new T();
        }
    }
}