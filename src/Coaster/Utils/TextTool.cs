namespace Coaster.Utils
{
    public static class TextTool
    {
        public static string Normalize(string text)
        {
            return text.Replace("\r\n", "\n");
        }
    }
}