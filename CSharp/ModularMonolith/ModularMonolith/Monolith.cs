using ShopModule;

namespace ModularMonolith
{
  public class Monolith
  {
    public Monolith(DaoFactory daoFactory)
    {
      var shopModule = new ShopModuleInstance();
      GetProductsEndpoint = new GetProductsEndpoint(shopModule, daoFactory);
      BuyProductEndpoint = new BuyProductEndpoint(shopModule, daoFactory);
    }

    public GetProductsEndpoint GetProductsEndpoint { get; }
    public BuyProductEndpoint BuyProductEndpoint { get; }
  }
}