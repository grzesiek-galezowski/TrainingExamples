using System;

namespace KataTrainReservationTddEbook;

public class TodoReservationInProgressFactory : IReservationInProgressFactory
{
  public IReservationInProgress FreshInstance()
  {
    throw new NotImplementedException();
  }
}