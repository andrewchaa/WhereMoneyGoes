using System;
using System.Globalization;
using Library;
using Microsoft.Extensions.Logging;

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

        public static MoneyTransaction Parse(string date, string description, string paidOut, string paidIn)
        {
            return new MoneyTransaction(
                DateTime.ParseExact(date, "dd MMM yyyy", CultureInfo.InvariantCulture),
                description.Trim(),
                description.Map(d => FindCategory(d)),
                !string.IsNullOrEmpty(paidOut.Trim()) ? decimal.Parse(paidOut.Trim()) : 0,
                !string.IsNullOrEmpty(paidIn.Trim()) ? decimal.Parse(paidIn.Trim()) : 0
                );
        }

        private static string FindCategory(string description)
        {
            return description.Map(d => Categories.Items.ContainsKey(d)
                ? Categories.Items[d]
                : "Uncategories");
        }
    }
}