namespace Coaster.Utils
{
    public static class TextTool
    {
        public static string Normalize(string text)
        {
            return text.Replace("\r\n", "\n");
        }

        public static string NullIfEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text;
        }
    }
}