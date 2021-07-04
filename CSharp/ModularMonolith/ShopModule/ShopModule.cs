namespace ShopModule
{
  public class ShopModule
  {
    public ShopModule()
    {
      CommandFactory = new ShopCommandFactory();
    }

    public ShopCommandFactory CommandFactory { get; }
  }
}