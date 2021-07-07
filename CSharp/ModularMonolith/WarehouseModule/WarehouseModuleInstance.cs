namespace WarehouseModule
{
  public class WarehouseModuleInstance
  {
    public WarehouseModuleInstance()
    {
      CommandFactory = new();
    }

    public WarehouseCommandFactory CommandFactory { get; }
  }
}