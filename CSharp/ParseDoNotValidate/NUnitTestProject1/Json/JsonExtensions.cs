namespace NUnitTestProject1.Json
{
    public static class JsonExtensions
    {
        public static string QuotedOrNull(this string value)
        {
            if (value == null)
            {
                return "null";
            }
            else
            {
                return "\"" + value + "\"";
            }
        }
    }
}