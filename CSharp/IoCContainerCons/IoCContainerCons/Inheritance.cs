using System;
using System.Collections.Generic;
using System.Text;

namespace IoCContainerCons
{
    public abstract class AbstractReport
    {
        private readonly List<string> _lines = new List<string>();
        
        public void Setup()
        {
            AddLine($"This is a {Period()} report");
            FillTheRestOfTheReport();
        }

        protected void AddLine(string line)
        {
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
            return "yearly";
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
