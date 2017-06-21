using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosuresXhtmlBuilder
{
  public class Builder
  {
    public void Html(Action<Builder> action)
    {
      action.Invoke(this);
    }

    public void Body(Action<Builder> action)
    {
      action.Invoke(this);
    }

    public void Div(Action<Builder> action, string styles = "")
    {
      action.Invoke(this);
    }

    public void Content(string helloWorld)
    {
      Console.WriteLine("Hello World");
    }
  }


  public class Lolek
  {
    public void Main()
    {
      new Builder().Html(html =>
        {
          html.Body(body =>
            {
              body.Div(div =>
                {
                  body.Content("Hellp world!");
                }, styles: "color: gray");
            });
        });
    }
  }
}
