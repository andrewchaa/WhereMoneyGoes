using System.Collections.Generic;
using System.Linq;

namespace QuickExpense.Domain.Models
{
    public class Row
    {
        public IList<string> Cells { get;  }
        
        public Row(string row)
        {
            Cells = row
                .Split(",")
                .Select(c => c.Replace("\"", string.Empty))
                .ToList();
        }

        public override string ToString()
        {
            return string.Join(", ", Cells);
        }
    }
}