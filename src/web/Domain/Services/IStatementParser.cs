using System.Collections.Generic;
using QuickExpense.Domain.Models;

namespace QuickExpense.Domain.Services
{
    public interface IStatementParser
    {
        ExpenseTransaction Parse(IList<string> columns);
    }
}