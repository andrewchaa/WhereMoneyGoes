using System.Collections.Generic;
using System.Linq;

namespace QuickExpense.Domain.Models
{
    public class Summary
    {
        public IEnumerable<string> Uncategorised { get; }
        public IEnumerable<Expense> Expenses { get; }
        public int Count { get; }
        public decimal Total { get; }
        
        public Summary(IList<MoneyTransaction> transactions)
        {
            
            Uncategorised = transactions
                .Where(t => t.Category == "Uncategorised")
                .Select(t => t.Description)
                .Distinct()
                .OrderBy(t => t);
            
            Expenses = transactions
                .OrderBy(t => t.Category)
                .GroupBy(t => t.Category)
                .Select(i => new Expense(i.Key, i.Sum(s => s.PaidOut)));
            
            Total = transactions.Sum(t => t.PaidOut);
            Count = transactions.Count();

        }

    }
}