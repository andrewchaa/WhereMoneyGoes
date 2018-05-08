using System;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace QuickExpense.Domain.Models
{
    public class MoneyTransaction
    {
        public DateTime Date { get; }
        public string Description { get; }
        public decimal PaidOut { get; }
        public decimal PaidIn { get; }

        public MoneyTransaction(DateTime date, string description, decimal paidOut, decimal paidIn)
        {
            Date = date;
            Description = description;
            PaidOut = paidOut;
            PaidIn = paidIn;
        }

        public static MoneyTransaction Parse(string date, string description, string paidOut, string paidIn)
        {
            return new MoneyTransaction(
                DateTime.ParseExact(date, "dd MMM yyyy", CultureInfo.InvariantCulture),
                description.Trim(),
                !string.IsNullOrEmpty(paidOut.Trim()) ? decimal.Parse(paidOut.Trim()) : 0,
                !string.IsNullOrEmpty(paidIn.Trim()) ? decimal.Parse(paidIn.Trim()) : 0
                );
        }
    }
}