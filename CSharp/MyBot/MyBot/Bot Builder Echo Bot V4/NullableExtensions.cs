using System;

namespace BotBuilderEchoBotV4
{
  public static class NullableExtensions
  {
    public static T ValueOrThrow<T>(this T? value, string valueName)
      where T : class
    {
      return value ?? throw new ArgumentNullException(valueName);
    }
  }
}