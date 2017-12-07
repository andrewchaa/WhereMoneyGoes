using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Report
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args[0];

            var lines = File.ReadAllLines(file).Skip(1);
            var expenses = new List<Expense>();
            
            foreach (var line in lines)
            {
                var records = SplitCsv(line).ToArray();
                var expense = Process(records);
                expenses.Add(expense);
            }
        }

        private static Expense Process(string[] records)
        {
            return new Expense(
                records[0].Replace("\"", string.Empty), 
                records[1], 
                records[2].Replace("\"", string.Empty), 
                records[3].Replace("\"", string.Empty), 
                records[4].Replace("\"", string.Empty)
                );
        }
        
        public static IEnumerable<string> SplitCsv(string input)
        {
            var csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

            foreach (Match match in csvSplit.Matches(input))
            {
                yield return match.Value.TrimStart(',');
            }
        }        
    }

    class Expense
    {
        public DateTime Date { get; set; }
        public string ExpenseType { get; set; }
        public string Description { get; set; }
        public Decimal PaidOut { get; set; }
        public Decimal PaidIn { get; set; }

        public Expense(string dateTime, string type, string description, string paidOut, string paidIn)
        {
            Console.WriteLine($"{dateTime}, {type}, {description}, {paidOut}, {paidIn}");
            Date = DateTime.ParseExact(dateTime, "dd MMM yyyy", CultureInfo.InvariantCulture);
//            Date = DateTime.Parse(dateTime);
            ExpenseType = type;
            Description = description;
            PaidOut = string.IsNullOrWhiteSpace(paidOut) ? 0 : decimal.Parse(paidOut);
            PaidIn = string.IsNullOrWhiteSpace(paidIn) ? 0 : decimal.Parse(paidIn);
        }

    }
}