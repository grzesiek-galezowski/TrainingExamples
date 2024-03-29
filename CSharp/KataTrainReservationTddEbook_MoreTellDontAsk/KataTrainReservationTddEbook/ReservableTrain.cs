﻿using System.Collections.Generic;
using System.Linq;
using Core.Maybe;

namespace KataTrainReservationTddEbook;

public class ReservableTrain : ITrain, IReservableTrain
{
  private readonly IReadOnlyList<ICoach> _coaches;
  private readonly uint _totalSeats;
  private readonly uint _reservedSeats;

  public ReservableTrain(IReadOnlyList<ICoach> coaches, uint totalSeats, uint reservedSeats)
  {
    _coaches = coaches;
    _totalSeats = totalSeats;
    _reservedSeats = reservedSeats;
  }

  public void UpdateIn(IFleet fleet)
  {
    throw new System.NotImplementedException();
  }

  public void EvaluateIndividualCoachReservability(uint requestedSeatCount, IReservationInProgress reservationInProgress,
    CoachReservabilityVisitor coachReservabilityVisitor)
  {
    var coachWithEnoughPlace = GetCoachWithEnoughPlaceFor(requestedSeatCount);
    if (coachWithEnoughPlace.HasValue)
    {
      coachReservabilityVisitor.CoachCanBeReserved(coachWithEnoughPlace.Value(), requestedSeatCount, reservationInProgress);
    }
    else
    {
      reservationInProgress.NotEnoughSeatsInAnyCoachToFit(requestedSeatCount);
    }
  }

  public void EvaluateIndividualCoachSoftLimitReservability(
    uint requestedSeatCount,
    IReservationInProgress reservationInProgress, 
    CoachSoftLimitReservabilityVisitor coachSoftLimitReservabilityVisitor)
  {
    var coach = GetCoachWithEnoughPlaceWithinSoftLimitFor(requestedSeatCount);
    if (coach.HasValue)
    {
      coachSoftLimitReservabilityVisitor.CoachFound(coach.Value(), requestedSeatCount, reservationInProgress);
    }
    else
    {
      coachSoftLimitReservabilityVisitor.CoachNotFound(requestedSeatCount, reservationInProgress, this);
    }
  }

  public void EvaluateUpFrontTrainReservability(uint requestedSeatCount, IReservationInProgress reservationInProgress,
    TrainHardLimitReservabilityVisitor visitor)
  {
    if (HasTotalUpFrontFreeSeatsAtLeast(requestedSeatCount))
    {
      visitor.TrainIsReservable(this, requestedSeatCount, reservationInProgress);
    }
    else
    {
      visitor.TrainIsNotReservable(requestedSeatCount, reservationInProgress);
    }
  }

  private Maybe<ICoach> GetCoachWithEnoughPlaceWithinSoftLimitFor(uint requestedSeatCount)
  {
    return _coaches.FirstOrDefault(c => c.HasPlaceWithingTheSoftLimitFor(requestedSeatCount)).ToMaybe();
  }

  private Maybe<ICoach> GetCoachWithEnoughPlaceFor(uint requestedSeatCount)
  {
    return _coaches.FirstOrDefault(c => c.HasPlaceFor(requestedSeatCount)).ToMaybe();
  }

  private bool HasTotalUpFrontFreeSeatsAtLeast(uint requestedSeatCount)
  {
    var reservableSeats = _totalSeats * 0.7;
    var freeSeats = reservableSeats - _reservedSeats;
    return requestedSeatCount <= freeSeats;
  }
}