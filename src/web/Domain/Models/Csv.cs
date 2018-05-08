using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace QuickExpense.Domain.Models
{
    public class Csv
    {
        public IEnumerable<Row> Rows { get; }
        public IEnumerable<string> Headers { get; set; }
        public int Count => Rows.Count();

        private Csv(IList<string> headers, IList<string> rows)
        {
            Headers = headers;
            Rows = rows.Select(r => new Row(r));
        }
        
        public static Csv From(string csvString)
        {
            var lines = csvString
                .Split("\n")
                .Where(l => l.Length > 0)
                .ToList();

            return new Csv(
                lines.First().Split(","),
                lines.Skip(1).ToList()
                );
        }
    }

    public class Row
    {
        public IEnumerable<string> Cells { get;  }
        
        public Row(string row)
        {
            Cells = row.Split(",");
        }
    }

}