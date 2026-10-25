using System.Diagnostics;
using TddXt.AnyRoot;

namespace TrackClinicServiceSpecification.Automation;

public static class AnyExtensions
{
    extension(Any)
    {
        public static string TraceId()
        {
            using var activity = new Activity("Test");
            activity.Start();

            return activity.TraceId.ToString();
        }
    }
}
