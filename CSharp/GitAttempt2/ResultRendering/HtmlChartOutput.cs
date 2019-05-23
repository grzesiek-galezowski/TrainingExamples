using System.Collections.Generic;
using System.IO;
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