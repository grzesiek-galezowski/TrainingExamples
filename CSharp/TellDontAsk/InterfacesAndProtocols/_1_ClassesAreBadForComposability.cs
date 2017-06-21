using System;
using System.Data.SqlClient;

namespace InterfacesAndProtocols
{
  public class PersistentStorage
  {
    private readonly SqlConnection _c;

    public PersistentStorage(SqlConnection c)
    {
      _c = c;
    }

    public virtual void Save(string content)
    {
      // save with connection
      Console.WriteLine(content);
    }
  }

  public class XmlStorage : PersistentStorage
  {
    public XmlStorage()
      : base(new SqlConnection()) //!!!
    {

    }

    public override void Save(string content)
    {
      //save in XML
    }

  }

  public class Main
  {
    public Main()
    {
      var storage = new PersistentStorage(new SqlConnection());
      storage.Save("aa");
      var storage2 = new XmlStorage();
      storage2.Save("bb");
      new PersistentStorage2(new WrongConnection()).Save("aa");
    }
  }
}
