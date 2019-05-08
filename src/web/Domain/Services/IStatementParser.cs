using System.Collections.Generic;
using Wmg.App.Domain.Models;

namespace Wmg.App.Domain.Services
{
    public interface IStatementParser
    {
        ExpenseTransaction Parse(IList<string> columns);
    }
}