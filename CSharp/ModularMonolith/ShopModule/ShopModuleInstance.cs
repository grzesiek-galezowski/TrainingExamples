namespace ShopModule
{
  public class ShopModuleInstance
  {
    public ShopModuleInstance()
    {
      CommandFactory = new ShopCommandFactory();
    }

    public ShopCommandFactory CommandFactory { get; }
  }
}