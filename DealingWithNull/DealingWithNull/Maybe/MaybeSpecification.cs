using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DealingWithNull.Maybe
{
  public class MaybeSpecification
  {
    [Test]
    public void ShouldBEHAVIOR()
    {
      var maybeNothing = ReturnNull();
      Assert.AreEqual(false, maybeNothing.HasValue);
      Assert.AreEqual(default(Maybe<string>), maybeNothing);

      var maybeString = ReturnEmptyString();
      Assert.AreEqual(true, maybeString.HasValue);
      Assert.AreNotEqual(default(Maybe<string>), maybeString);
    }

    [Test]
    public void ShouldBeOkForPrimitiveAndStructTypes()
    {
      var maybeInt = new Maybe<int>(default(int));

      Assert.True(maybeInt.HasValue);
    }

    [Test]
    public void ShouldSupportLinqSelectMany()
    {
      //TODO show coverage from NCrunch
      var result = from a in new Maybe<string>("Hello World!")
                   from b in GetCustomer()
                   from c in new Maybe<DateTime>(new DateTime(2010, 1, 14))
                   select a + " " + b + c.ToShortDateString();

      Assert.AreEqual(false, result.HasValue);
    }

    [Test]
    public void ShouldSupportLinqSelect()
    {
      var name = from c in GetCustomer() select c.Name; //name implementation throws exception!

      Assert.False(name.HasValue);
    }

    [Test]
    public void ShouldSupportLinqSelect2()
    {
      var name = from customers in GetCustomers() //returns null!!!
                 select customers.First().Name.ToArray();

      Assert.False(name.HasValue);
    }


    private Maybe<Customer> GetCustomer()
    {
      return null;
    }

    private static Maybe<string> ReturnNull()
    {
      return null;
    }

    private static Maybe<string> ReturnEmptyString()
    {
      return string.Empty;
    }

    private Maybe<IEnumerable<Customer>> GetCustomers()
    {
      return null;
    }
  }

  internal class Customer
  {
    public string Name
    {
      get { throw new NotImplementedException(); }
    }
  }
}