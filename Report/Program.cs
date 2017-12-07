using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Report
{
    class Program
    {
        static void Main(string[] args)
        {
            var uncategorisedExpenses = new List<string>();
            
            var file = args[0];

            var lines = File.ReadAllLines(file).Skip(1);
            var expenses = new List<Expense>();
            
            foreach (var line in lines)
            {
                var records = SplitCsv(line).ToArray();
                var expense = Process(records);
                expenses.Add(expense);
            }

            var categoriesText = File.ReadAllText("Report/Categories.json");
            var categories = JsonConvert.DeserializeObject<IEnumerable<TokenCategory>>(categoriesText);
            
            var reports = new Dictionary<string, decimal>();
            foreach (var category in categories)
            {
                if (reports.ContainsKey(category.Category)) continue;
                
                reports.Add(category.Category, 0m);
            }
            
//            reports.Add("Books", 0m);
//            reports.Add("Charity", 0m);
//            reports.Add("Communications", 0m);
//            reports.Add("Dividend", 0m);
//            reports.Add("Equipment", 0m);
//            reports.Add("Fixtures", 0m);
//            reports.Add("Insurance", 0m);
//            reports.Add("Motor Expenses", 0m);
//            reports.Add("Office Supplies", 0m);
//            reports.Add("Postage", 0m);
//            reports.Add("Public Transport", 0m);
//            reports.Add("Maintenance", 0m);
//            reports.Add("Software", 0m);
//            reports.Add("Subsistence", 0m);
//            reports.Add("Taxi", 0m);
//            reports.Add("Tools", 0m);
//            reports.Add("Travel", 0m);
            
            foreach (var expense in expenses)
            {
                var category = categories.SingleOrDefault(c => expense.Description.ToLower().Contains(c.Token.ToLower()));
                if (category != null)
                {
                    reports[category.Category] += expense.PaidOut;
                    
                }
                else
                {
                    uncategorisedExpenses.Add($"{expense.Description} - {expense.PaidOut}");
                }
            }

            Console.WriteLine("Report ...");
            foreach (var report in reports)
            {
                Console.WriteLine($"{report.Key} - {report.Value}");
            }

            Console.WriteLine("\r\nUncategorised ...");
            foreach (var uncategorisedExpense in uncategorisedExpenses)
            {
                Console.WriteLine(uncategorisedExpense);
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
        
        private static IEnumerable<string> SplitCsv(string input)
        {
            var csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

            foreach (Match match in csvSplit.Matches(input))
            {
                yield return match.Value.TrimStart(',');
            }
        }        
    }

    class TokenCategory
    {
        public string Token { get; set; }
        public string Category { get; set; }
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
            Date = DateTime.ParseExact(dateTime, "dd MMM yyyy", CultureInfo.InvariantCulture);
            ExpenseType = type;
            Description = description;
            PaidOut = string.IsNullOrWhiteSpace(paidOut) ? 0 : decimal.Parse(paidOut);
            PaidIn = string.IsNullOrWhiteSpace(paidIn) ? 0 : decimal.Parse(paidIn);
        }

    }
}