#region File Header & Copyright Notice
//Copyright 2015 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainSpecificLanguages
{
  public class LiteralExtensionsExample
  {
    public void Main()
    {
      var date = 5.Minutes().Ago() + 6.Seconds();
      var date2 = 632.Days().Before(5.Minutes().FromNow() + 6.Seconds());
    }
  }

  public static class DateTimeLiteralExtensions
  {
    public static TimeSpan Minutes(this int num)
    {
      return TimeSpan.FromMinutes(num);
    }

    public static TimeSpan Seconds(this int num)
    {
      return TimeSpan.FromSeconds(num);
    }

    public static TimeSpan Milliseconds(this int num)
    {
      return TimeSpan.FromMilliseconds(num);
    }

    public static TimeSpan Days(this int num)
    {
      return TimeSpan.FromDays(num);
    }

    public static TimeSpan Hours(this int num)
    {
      return TimeSpan.FromHours(num);
    }


    public static DateTime Ago(this TimeSpan ts)
    {
      return DateTime.Now - ts;
    }

    public static DateTime Before(this TimeSpan ts, DateTime dt)
    {
      return dt - ts;
    }

    public static DateTime FromNow(this TimeSpan ts)
    {
      return DateTime.Now + ts;
    }

  }
}
