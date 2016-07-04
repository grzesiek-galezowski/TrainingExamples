using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductNameProblem._04_HelperAttempt
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
      if (_forbiddenProductNames.Contain(product.Name, product.Type))
      {
        return;
      }

      if (!_products.Any(p => ProductNameHelper.AreProductNamesEqual(
        product.Type, product.Name, p.Type, p.Name)))
      {
        _products.Add(product);
      }
    }

    public void RemoveProduct(string name, ProductTypes type)
    {
      _products.RemoveAll(p => 
        ProductNameHelper.AreProductNamesEqual(type, name, p.Type, p.Name));
    }

    public bool Contain(string name, ProductTypes type)
    {
      return _products.Any(p => ProductNameHelper.AreProductNamesEqual(p.Type, p.Name, type, name));
    }

    public Product FindBy(Func<Product, bool> criteria)
    {
      return _products.Find(p => criteria(p)); //!!! look where this critria is given
    }

    public void AddForbiddenProduct(string name, ProductTypes type)
    {
      _forbiddenProductNames.Add(name, type);
    }
  }

  public enum ProductTypes
  {
    Personal,
    Domestic,
    Medicines,
    Hardware
  }

  public class Product
  {

    public Product(string name, ProductTypes type)
    {
      Name = name;
      Type = type;
    }

    public string Name { get; }
    public ProductTypes Type { get; }
  }
}
