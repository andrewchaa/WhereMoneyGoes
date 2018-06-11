namespace QuickExpense.Domain.Services
{
    public class StatmentCleaner : IStatementCleaner
    {
        public string Clean(string input)
        {
            return input.Replace("))),", string.Empty);
        }        
    }
}