using TddXt.AnyExtensibility;
using TddXt.AnyRoot.Strings;

namespace Lib
{
  public static class AnyExtensions
  {
    public static string Password(this BasicGenerator any)
    {
      return any.String(nameof(Password));
    }

    public static string Login(this BasicGenerator any)
    {
      return any.String(nameof(Login));
    }
  }
}