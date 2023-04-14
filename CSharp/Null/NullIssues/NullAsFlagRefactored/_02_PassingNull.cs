using System;

namespace NullAsFlagRefactored;

class _02_PassingNull
{
    static void Main2(string[] args)
    {
        var myNotificationsEngine = new MyNotificationsEngine();

        var primaryDataCenter = new DataCenter();
        var secondaryDataCenter = new DataCenter();

        var logic = new SomeKindOfLogic(
            myNotificationsEngine, 
            primaryDataCenter, 
            secondaryDataCenter);

        //somewhere
        logic.HandleMessageFromUser("message1");


        //somewhere else
        logic.HandleReplicatedMessage("message2");
    }
}

internal class SomeKindOfLogic
{
    private readonly MyNotificationsEngine _myNotificationsEngine;
    private readonly IDataCenter _primaryDataCenter;
    private readonly IDataCenter _secondaryDataCenter;

    public SomeKindOfLogic(
        MyNotificationsEngine myNotificationsEngine, 
        IDataCenter primaryDataCenter, 
        IDataCenter secondaryDataCenter)
    {
        _myNotificationsEngine = myNotificationsEngine;
        _primaryDataCenter = primaryDataCenter;
        _secondaryDataCenter = secondaryDataCenter;
    }

    public void HandleMessageFromUser(string message)
    {
        _myNotificationsEngine.NotifyNewData(
            _primaryDataCenter, 
            _secondaryDataCenter, 
            message);
    }

    public void HandleReplicatedMessage(string message)
    {
        _myNotificationsEngine.NotifyNewData(
            _primaryDataCenter, 
            new IgnoreThisDataCenter(), 
            message);
    }
}

internal class IgnoreThisDataCenter : IDataCenter
{
    public void Send(string message)
    {
        //EMPTY
    }
}

public interface IDataCenter
{
    void Send(string message);
}

public class DataCenter : IDataCenter
{
    public void Send(string message)
    {
        Console.WriteLine(message);     
    }
}

internal class MyNotificationsEngine
{
    public void NotifyNewData(
        IDataCenter primaryDataCenter, 
        IDataCenter secondaryDataCenter, 
        string message)
    {
        primaryDataCenter.Send(message);
        secondaryDataCenter.Send(message);
    }
}