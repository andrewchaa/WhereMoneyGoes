using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FunctionalWay.Extensions;

namespace Calme.Domain.Models
{
    public class Row
    {
        public IList<string> Cells { get;  }
        
        public Row(string row)
        {
            var matches = new Regex("\".+?\"|[^\"]+?(?=,)|(?<=,)[^\"]+").Matches(row);
            Cells = matches
                .Select(m => m.Value
                    .Pipe(v => v.Replace("\"", string.Empty))
                    .Pipe(v => v.Replace(",", string.Empty))                    
                    .Pipe(v => v.Trim())
                )
                .ToList();
        }

        public override string ToString()
        {
            return string.Join(", ", Cells);
        }
    }
}