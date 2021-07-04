namespace ModularMonolith
{
  public class Monolith
  {
    public Monolith()
    {
      var shopModule = new ShopModule.ShopModule();
      GetProductsEndpoint = new GetProductsEndpoint(shopModule);
    }

    public GetProductsEndpoint GetProductsEndpoint { get; }
  }
}