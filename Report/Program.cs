using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

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
                var records = line.Replace("\"", string.Empty).Split(',');
                expenses.Add(new Expense(records[0], records[1], records[2], records[3], records[4]));
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