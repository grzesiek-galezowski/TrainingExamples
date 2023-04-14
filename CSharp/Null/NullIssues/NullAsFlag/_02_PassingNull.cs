using System;

namespace NullAsFlag;

class _02_PassingNull
{
    //null as do-nothing/flag - parameter
    //see drawing on slides
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
        {
          logic.HandleMessageFromUser("message");
        }

        //somewhere else
        {
          logic.HandleReplicatedMessage("message");
        }
    }
}

internal class SomeKindOfLogic
{
    private readonly MyNotificationsEngine _myNotificationsEngine;
    private readonly DataCenter _primaryDataCenter;
    private readonly DataCenter _secondaryDataCenter;

    public SomeKindOfLogic(
        MyNotificationsEngine myNotificationsEngine, 
        DataCenter primaryDataCenter, 
        DataCenter secondaryDataCenter)
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
            null, 
            message);
    }
}

public class DataCenter
{
    public void Send(string message)
    {
        Console.WriteLine(message);     
    }
}

internal class MyNotificationsEngine
{
    public void NotifyNewData(
        DataCenter primaryDataCenter, 
        DataCenter secondaryDataCenter /* = null  */, 
        string message)
    {
        primaryDataCenter.Send(message);
        if (secondaryDataCenter != null) //null propagation doesn't help
        {
            secondaryDataCenter.Send(message);
        }
    }
}