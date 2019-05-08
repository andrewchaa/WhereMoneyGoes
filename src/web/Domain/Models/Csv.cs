using System.Collections.Generic;
using System.Linq;

namespace Wmg.App.Domain.Models
{
    public class Csv
    {
        public IEnumerable<Row> Rows { get; }
        public IEnumerable<string> Headers { get; set; }
        public int Count => Rows.Count();

        private Csv(IList<string> headers, IList<string> rows)
        {
            Headers = headers;
            Rows = rows
                .Select(r => new Row(r));
        }
        
        public static Csv From(Bank bank, string csvString)
        {
            var lines = csvString
                .Split("\n")
                .Where(l => l.Length > 0)
                .Select(r => r.Replace("))),", string.Empty))
                .ToList();

            if (bank == Bank.Hsbc)
            {
                return new Csv(lines.First().Split(","), lines.Skip(1).ToList());
            }
            
            return new Csv(
                new List<string>(), 
                lines.ToList());
            
        }
    }
}