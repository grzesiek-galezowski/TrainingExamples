using System;

namespace GitAttempt2
{
    public sealed class AnalysisResult : IEquatable<AnalysisResult>
    {
      public string Text { get; }
      public double Complexity { get; }

      public AnalysisResult(string text, double complexity)
      {
        Text = text;
        Complexity = complexity;
      }

      public bool Equals(AnalysisResult other)
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
        return Equals((AnalysisResult) obj);
      }

      public override int GetHashCode()
      {
        return (Text != null ? Text.GetHashCode() : 0);
      }

      public static bool operator ==(AnalysisResult left, AnalysisResult right)
      {
        return Equals(left, right);
      }

      public static bool operator !=(AnalysisResult left, AnalysisResult right)
      {
        return !Equals(left, right);
      }
    }
}