using System;
using System.Collections.Generic;
using QuickExpense.Domain.Models;

namespace QuickExpense.Domain.Services
{
    public class HsbcParser : IStatementParser
    {
        public MoneyTransaction Parse(IList<string> columns)
        {
            throw new NotImplementedException();
        }
    }
}