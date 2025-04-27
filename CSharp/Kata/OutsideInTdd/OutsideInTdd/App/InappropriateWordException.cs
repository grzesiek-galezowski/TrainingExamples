using System;

namespace OutsideInTdd.App
{
    public class InappropriateWordException : Exception
    {
        public InappropriateWordException(string word)
        {
            Word = word;
        }

        public string Word { get; }
    }
}