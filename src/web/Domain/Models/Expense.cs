namespace Calme.Domain.Models
{
    public class Expense
    {
        public string Category { get; }
        public decimal Amount { get; }

        public Expense(string category, decimal amount)
        {
            Category = category;
            Amount = amount;
        }
    }
}