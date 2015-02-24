using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesAndProtocols
{
  public class PersistentStorage2
  {
    private readonly Connection _connection;

    public PersistentStorage2(Connection connection)
    {
      _connection = connection;
    }

    public void Save(string content)
    {
      _connection.Open();
      _connection.Write(content);
      _connection.Close();

    }
  }

  public interface Connection
  {
    void Open();
    void Write(string content);
    void Close();
  }

  public class WrongConnection : Connection //!!!
  {
    private readonly SqlConnection _sql;

    public WrongConnection()
    {
      _sql = new SqlConnection();
    }

    public void Open()
    {
      _sql.Close();
    }

    public void Write(string content)
    {
      _sql.CreateCommand().ExecuteScalar(); //dummy
    }

    public void Close()
    {
      _sql.Open();
    }
  }
}
