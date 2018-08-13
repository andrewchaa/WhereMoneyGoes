using System;
using System.Collections.Generic;
using Calme.Domain.Models;

namespace Calme.Domain.Services
{
    public class BarclaycardParser : IStatementParser
    {
        public ExpenseTransaction Parse(IList<string> columns)
        {
            throw new NotImplementedException();
        }
    }
}