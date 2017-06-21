using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DomainSpecificLanguages
{
    public class PathExample
    {
      public class CopyProcess
      {
        public string Source { get; set; }
        public string Destination { get; set; }
      }


      public static void Main()
      {
        new CopyProcess
        {
          Source      = File        ("C:")/"lolek"/"Zenek.txt",
          Destination = Directory   ("C:")/"Destination"/"Somewhere"/"Deep"/"In the Ocean",
        };
      }

      private static FluentPath File(string s)
      {
        return new FluentPath(s);
      }

      private static FluentPath Directory(string s)
      {
        return new FluentPath(s);
      }


      public class FluentPath
      {
        private string _content;

        public FluentPath(string content)
        {
          _content = content;
        }

        public static FluentPath operator /(FluentPath path, string rest)
        {
          return path.Join(rest);
        }

        public static implicit operator string(FluentPath path)
        {
          return path._content;
        }

        private FluentPath Join(string rest)
        {
          return new FluentPath(Path.Combine(_content, rest));
        }
      }

    }

}
