using System;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace ProductNameProblem._02_CaseInsensitive
{
  public class ProductsApi
  {
    private readonly ForbiddenProductNames _forbiddenProductNames;
    private readonly List<Product> _products = new List<Product>();

    public ProductsApi(ForbiddenProductNames forbiddenProductNames)
    {
      _forbiddenProductNames = forbiddenProductNames;
    }

    public void AddProduct(Product product)
    {
      if (_forbiddenProductNames.Contain(product.Name))
      {
        return;
      }

      if (!_products.Any(p => string.Equals(p.Name, product.Name, StringComparison.InvariantCultureIgnoreCase)))
      {
        _products.Add(product);
      }
    }

    public void RemoveProduct(string name)
    {
      _products.RemoveAll(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));
    }

    public bool Contain(string name)
    {
      return _products.Any(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));
    }

    public Product FindBy(Func<Product, bool> criteria)
    {
      return _products.Find(p => criteria(p)); //!!! look where this critria is given
    }

    public void AddForbiddenProduct(string name)
    {
      _forbiddenProductNames.Add(name);
    }
  }

  public class Product
  {

    public Product(string name)
    {
      Name = name;
    }

    public string Name { get; }
  }
}
