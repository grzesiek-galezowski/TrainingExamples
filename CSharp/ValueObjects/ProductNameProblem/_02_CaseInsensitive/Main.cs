using System;

namespace ProductNameProblem._02_CaseInsensitive
{
  public class Main
  {
    public void DoSomething()
    {
      var products = new ProductsApi(new ForbiddenProductNames());
      products.AddProduct(new Product("mobile PC Asus tlk123"));
      var laptop = products.FindBy(p => string.Equals(p.Name, "Laptop MDA 1123", StringComparison.InvariantCultureIgnoreCase));
      products.RemoveProduct("mobile PC Asus tlk123");
      if (products.Contain("mobile PC"))
      {
        Console.WriteLine("products contain mobile PC");
      }

      products.AddForbiddenProduct("toxic drugs");
    }
  }
}