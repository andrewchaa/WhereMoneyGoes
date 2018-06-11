using System;
using System.Collections.Generic;
using QuickExpense.Domain.Models;

namespace QuickExpense.Domain.Services
{
    public class BarclaycardParser : IStatementParser
    {
        public ExpenseTransaction Parse(IList<string> columns)
        {
            throw new NotImplementedException();
        }
    }
}