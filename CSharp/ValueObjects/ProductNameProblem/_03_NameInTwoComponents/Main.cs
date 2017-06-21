using System;

namespace ProductNameProblem._03_CaseInsensitive
{
  public class Main
  {
    public void DoSomething()
    {
      var products = new ProductsApi(new ForbiddenProductNames());
      products.AddProduct(new Product("mobile PC Asus tlk123", ProductTypes.Hardware));
      var laptop = products.FindBy(p => 
        p.Name.Equals("Laptop MDA 1123", StringComparison.InvariantCultureIgnoreCase)
        && p.Type == ProductTypes.Hardware
        );
      products.RemoveProduct("mobile PC Asus tlk123", ProductTypes.Hardware);
      if (products.Contain("mobile PC", ProductTypes.Hardware))
      {
        Console.WriteLine("products contain mobile PC");
      }

      products.AddForbiddenProduct("toxic drugs", ProductTypes.Medicines);
    }
  }
}