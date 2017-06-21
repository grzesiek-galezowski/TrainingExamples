using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductNameProblem._01_StringBasedVersion
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

      if (!_products.Any(p => p.Name == product.Name))
      {
        _products.Add(product);
      }
    }

    public void RemoveProduct(string name)
    {
      _products.RemoveAll(p => p.Name == name);
    }

    public bool Contain(string name)
    {
      return _products.Any(p => p.Name == name);
    }

    public Product FindBy(Func<Product, bool> criteria)
    {
      return _products.Find(p => criteria(p));
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
