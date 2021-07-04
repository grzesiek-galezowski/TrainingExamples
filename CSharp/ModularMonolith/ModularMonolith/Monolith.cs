using ShopModule;

namespace ModularMonolith
{
  public class Monolith
  {
    private readonly ShopModuleInstance _shopModule;

    public Monolith()
    {
      _shopModule = new ShopModuleInstance();
    }

    public GetProductsEndpoint CreateProductsEndpoint(ShopDbContext shopDbContext) 
      => new(_shopModule, shopDbContext);
  }
}