using System.Diagnostics;
using TddXt.AnyRoot;

namespace TrackClinicServiceSpecification.Automation;

public static class AnyExtensions
{
  extension(Any)
  {
    public static ActivityTraceId TraceId()
    {
      return ActivityTraceId.CreateRandom();
    }
  }
}