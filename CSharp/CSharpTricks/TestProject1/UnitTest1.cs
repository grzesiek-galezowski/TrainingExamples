using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions.Extensions;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        private List<IGrouping<string, string>> _toList;

        [Test]
        public void ShouldXXXXXXXXXXXXX() //bug
        {
            //GIVEN
            
            //WHEN

            //THEN
            Assert.Fail("unfinished");
        }


        [Test]
        public async Task Test3()
        {
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine(i);
            }


            string x = S(123, "a");

            _toList = new int[] { 1, 2, 3 }
                .Where(z => z > 3)
                .Select(z => z.ToString())
                .GroupBy(z => z)
                .ToList();

            var x2 = (((1 + 3) + 4) + 5);

            NewMethod3();
        }

        private static void NewMethod3()
        {
            NewMethod2();
        }

        private static void NewMethod2()
        {
            NewMethod1();
        }

        private static void NewMethod1()
        {
            NewMethod();
        }

        private static void NewMethod()
        {
            Console.WriteLine("lol");
            //throw new Exception("lol");
        }

        private static string S(int i, string s)
        {
            return null;
        }

        [Test]
        public void Test4()
        {
            Assert.Pass();
            if (DateTime.Now.Ticks < 1)
            {
                Console.WriteLine();
            }
        }
    }
}