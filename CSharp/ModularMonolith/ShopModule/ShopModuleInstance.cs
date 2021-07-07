namespace ShopModule
{
  public class ShopModuleInstance
  {
    public ShopModuleInstance(IWarehouseApi warehouseApi)
    {
      CommandFactory = new ShopCommandFactory(warehouseApi);
    }

    public ShopCommandFactory CommandFactory { get; }
  }
}