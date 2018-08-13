using System.Collections.Generic;
using Calme.Domain.Models;

namespace Calme.Domain.Services
{
    public interface IStatementParser
    {
        ExpenseTransaction Parse(IList<string> columns);
    }
}