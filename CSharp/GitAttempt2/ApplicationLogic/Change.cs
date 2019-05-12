using System;

namespace ApplicationLogic
{
    public sealed class Change : IEquatable<Change>
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
      }

      public bool Equals(Change other) //bug change these equals to something else
      {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Text, other.Text);
      }

      public override bool Equals(object obj)
      {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Change) obj);
      }

      public override int GetHashCode()
      {
        return (Text != null ? Text.GetHashCode() : 0);
      }

      public static bool operator ==(Change left, Change right)
      {
        return Equals(left, right);
      }

      public static bool operator !=(Change left, Change right)
      {
        return !Equals(left, right);
      }
    }
}