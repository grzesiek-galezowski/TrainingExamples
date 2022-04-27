using System;

namespace NullAsFlag;

class Program2
{
    //null as do-nothing/flag - parameter
    //see drawing on slides
    static void Main2(string[] args)
    {
        var myNotificationsEngine = new MyNotificationsEngine();

        var localDataCenter = new DataCenter();
        var remoteDataCenter = new DataCenter();

        var logic = new SomeKindOfLogic(
            myNotificationsEngine, 
            localDataCenter, 
            remoteDataCenter);

        //somewhere
        logic.HandleMessageFromUser("message");


        //somewhere else
        logic.HandleReplicatedMessage("message");
    }
}

internal class SomeKindOfLogic
{
    private readonly MyNotificationsEngine _myNotificationsEngine;
    private readonly DataCenter _localDataCenter;
    private readonly DataCenter _remoteDataCenter;

    public SomeKindOfLogic(
        MyNotificationsEngine myNotificationsEngine, 
        DataCenter localDataCenter, 
        DataCenter remoteDataCenter)
    {
        _myNotificationsEngine = myNotificationsEngine;
        _localDataCenter = localDataCenter;
        _remoteDataCenter = remoteDataCenter;
    }

    public void HandleMessageFromUser(string message)
    {
        _myNotificationsEngine.NotifyNewData(
            _localDataCenter, 
            _remoteDataCenter, 
            message);
    }

    public void HandleReplicatedMessage(string message)
    {
        _myNotificationsEngine.NotifyNewData(
            _localDataCenter, 
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
        DataCenter localDataCenter, 
        DataCenter remoteDataCenter /* = null  */, 
        string message)
    {
        localDataCenter.Send(message);
        if (remoteDataCenter != null)
        {
            remoteDataCenter.Send(message);
        }
    }
}