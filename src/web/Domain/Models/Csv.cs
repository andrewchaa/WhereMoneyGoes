using System.Collections.Generic;
using System.Linq;

namespace QuickExpense.Domain.Models
{
    public class Csv
    {
        public string Line { get; }

        private Csv(string line)
        {
            Line = line;
        }
        
        public static IList<Csv> From(string csvString)
        {
            var lines = csvString
                .Split("\n")
                .Skip(1)
                .Where(l => l.Length > 0)
                .ToList();

            return lines.Select(l => new Csv(l)).ToList();
        }
    }
}