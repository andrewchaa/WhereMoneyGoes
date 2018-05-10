using System.Collections.Generic;
using QuickExpense.Domain.Models;

namespace QuickExpense.Domain.Services
{
    public interface IStatementParser
    {
        MoneyTransaction Parse(IList<string> columns);
    }
}