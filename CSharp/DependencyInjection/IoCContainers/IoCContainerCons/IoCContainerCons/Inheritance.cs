using System;
using System.Collections.Generic;

namespace IoCContainerCons
{
    public class Lol
    {
        public void DoWork()
        {
            var yearlyReport = new YearlyReport();
            yearlyReport.Setup();
            yearlyReport.Print();
            
            var monthlyReport = new MonthlyReport();
            monthlyReport.Setup();
            monthlyReport.Print();
        }
    }
    public abstract class AbstractReport
    {
        private readonly List<string> _lines = new List<string>();
        
        public void Setup()
        {
            AddLine($"This is a {Period()} report");
            FillTheRestOfTheReport();
        }

        public void Print()
        {
            Console.WriteLine(Period().ToUpperInvariant() + string.Join(Environment.NewLine, _lines));
        }

        protected void AddLine(string line)
        {
            Console.WriteLine($"Added {line}");
            _lines.Add(line);
        }

        protected abstract void FillTheRestOfTheReport();

        protected abstract string Period();
    }

    public class YearlyReport : AbstractReport
    {
        protected override void FillTheRestOfTheReport()
        {
            AddLine("We're not on the market a full year yet.");
        }


        protected override string Period()
        {
            return "yearly";
        }
    }

    public class MonthlyReport : AbstractReport
    {
        protected override void FillTheRestOfTheReport()
        {
            var incomes = MonthlyIncomes.PullDataForLastMonth();
            foreach (var income in incomes)
            {
                AddLine(income);
            }
        }

        protected override string Period()
        {
            return "monthly for" + DateTime.Now.Month;
        }
    }

    public static class MonthlyIncomes
    {
        public static List<string> PullDataForLastMonth()
        {
            return new List<string>() {"1", "2"};
        }
    }
}
