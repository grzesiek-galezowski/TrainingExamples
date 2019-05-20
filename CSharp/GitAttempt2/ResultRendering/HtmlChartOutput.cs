using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ApplicationLogic;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace ResultRendering
{
  public class HtmlChartOutput
  {
    private string _charts = "";
    private int _elementNum = 0;

    public void InstantiateTemplate(IEnumerable<ChangeLog> analysisResults)
    {
      foreach (var analysisResult in analysisResults)
      {
        _elementNum++;
        var replace = HtmlChartSingleResultTemplate.InstantiateWith(_elementNum, analysisResult);
        _charts += replace;
      }

      File.WriteAllText("output.html", File.ReadAllText("mainTemplate.html").Replace("___CHARTS___", _charts));
    }

    public void Show()
    {
      Browser.Open("output.html");
    }

  }

}