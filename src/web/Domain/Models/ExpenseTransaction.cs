using System;
using System.Collections.Generic;
using System.Globalization;
using FunctionalWay.Extensions;
using Wmg.App.Domain.Categories;

namespace Wmg.App.Domain.Models
{
    public class ExpenseTransaction
    {
        public DateTime Date { get; }
        public string Description { get; }
        public ExpenseCategories ExpenseCategories { get; }
        public decimal PaidOut { get; }
        public decimal PaidIn { get; }

        public ExpenseTransaction(DateTime date, 
            string description,
            ExpenseCategories expenseCategories,
            decimal paidOut, 
            decimal paidIn)
        {
            Date = date;
            Description = description;
            ExpenseCategories = expenseCategories;
            PaidOut = paidOut;
            PaidIn = paidIn;
        }

        public static ExpenseTransaction Parse(Bank bank, IList<string> columns)
        {
            var description = GetDescription(bank, columns);
            return new ExpenseTransaction(
                GetDate(bank, columns),
                description,
                FindCategory(description),
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

        private static ExpenseCategories FindCategory(string description)
        {
            return description.Pipe(d => Hsbc.Items.ContainsKey(d)
                ? Hsbc.Items[d]
                : Models.ExpenseCategories.Uncategorized);
        }
    }
}