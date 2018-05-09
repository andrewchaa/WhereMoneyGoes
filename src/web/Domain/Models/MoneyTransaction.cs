using System;
using System.Collections.Generic;
using System.Globalization;
using Library;
using Microsoft.Extensions.Logging;
using QuickExpense.Controllers;

namespace QuickExpense.Domain.Models
{
    public class MoneyTransaction
    {
        public DateTime Date { get; }
        public string Description { get; }
        public string Category { get; }
        public decimal PaidOut { get; }
        public decimal PaidIn { get; }

        public MoneyTransaction(DateTime date, 
            string description,
            string category,
            decimal paidOut, 
            decimal paidIn)
        {
            Date = date;
            Description = description;
            Category = category;
            PaidOut = paidOut;
            PaidIn = paidIn;
        }

        public static MoneyTransaction Parse(Bank bank, IList<string> columns)
        {
            var description = GetDescription(bank, columns);
            return new MoneyTransaction(
                GetDate(bank, columns),
                description,
                description.Map(d => FindCategory(d)),
                GetPaidOut(bank, columns),
                GetPaidIn(bank, columns)
                );
        }

        private static decimal GetPaidIn(Bank bank, IList<string> columns)
        {
            if (bank == Bank.Hsbc)
                return !string.IsNullOrEmpty(columns[4].Trim()) ? decimal.Parse(columns[4].Trim()) : 0;

            return 0m;
        }

        private static decimal GetPaidOut(Bank bank, IList<string> columns)
        {
            if (bank == Bank.Hsbc)
                return !string.IsNullOrEmpty(columns[3].Trim()) ? decimal.Parse(columns[3].Trim()) : 0;
            
            return !string.IsNullOrEmpty(columns[6].Trim()) ? decimal.Parse(columns[6].Trim()) : 0;
        }

        private static string GetDescription(Bank bank, IList<string> columns)
        {
            if (bank == Bank.Hsbc)
                return columns[2].Trim();

            return columns[1].Trim();
        }

        private static DateTime GetDate(Bank bank, IList<string> columns)
        {
            var col = columns[0];

            if (bank == Bank.Hsbc)
            {
                return DateTime.ParseExact(col, "dd MMM yyyy", CultureInfo.InvariantCulture);
            }
            
            return DateTime.ParseExact(col, "dd MMM yy", CultureInfo.InvariantCulture);
        }

        private static string FindCategory(string description)
        {
            return description.Map(d => Categories.Items.ContainsKey(d)
                ? Categories.Items[d]
                : "Uncategories");
        }
    }
}