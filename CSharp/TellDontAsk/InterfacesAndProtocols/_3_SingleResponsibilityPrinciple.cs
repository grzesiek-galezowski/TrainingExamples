using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesAndProtocols3
{
  public class Main
  {
    public void Lol()
    {
      new PersistentStorageWithLogging(new RealConnection());
      new PersistentStorage(new RealConnection(), new RealLog());
    }
  }

  public class PersistentStorageWithLogging
  {
    private readonly Connection _connection;

    public PersistentStorageWithLogging(Connection connection)
    {
      _connection = connection;
    }

    public void Save(string content)
    {
      WriteToLog("Starting command");
      _connection.Open();
      _connection.Write(content);
      _connection.Close();
      WriteToLog("finished command");

    }

    private void WriteToLog(string startingCommand)
    {
      //a lot of logging logic here!!
    }
  }

  public class PersistentStorage
  {
    private readonly Connection _connection;
    private readonly Log _log;

    public PersistentStorage(Connection connection, Log log)
    {
      _connection = connection;
      _log = log;
    }

    public void Save(string content)
    {
      _log.Write("Starting command");
      _connection.Open();
      _connection.Write(content);
      _connection.Close();
      _log.Write("finished command");

    }
  }

  public interface Log
  {
    void Write(string finishedCommand);
  }

  class RealLog : Log
  {
    public void Write(string finishedCommand)
    {
      throw new NotImplementedException();
    }
  }

  public interface ConnectionWithLogging
  {
    void Open();
    void Write(string content);
    void Close();
    void Log(string startingConnection);
  }

  public interface Connection
  {
    void Close();
    void Write(string content);
    void Open();
  }

  class RealConnection : Connection
  {
    public void Close()
    {
      throw new NotImplementedException();
    }

    public void Write(string content)
    {
      throw new NotImplementedException();
    }

    public void Open()
    {
      throw new NotImplementedException();
    }
  }
}
