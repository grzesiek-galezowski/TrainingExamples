using System;
using System.Text.RegularExpressions;

namespace ApplicationLogic
{
    public sealed class Change
    {
        public string Path { get; }
        public DateTimeOffset ChangeDate { get; }
        public string Text { get; }
        public double Complexity { get; }

        public Change(string path, string text, double complexity, DateTimeOffset changeDate)
        {
            Path = path;
            ChangeDate = changeDate;
            Text = text;
            Complexity = complexity;
            TextWithoutWhitespaces = Regex.Replace(Text, @"\s+", "");
        }

        public string TextWithoutWhitespaces { get; }
    }
}
    