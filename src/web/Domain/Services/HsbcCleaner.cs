using System.Text.RegularExpressions;
using FunctionalWay.Extensions;

namespace Calme.Domain.Services
{
    public class HsbcCleaner : IClean
    {
        public string Clean(string row)
        {
            return row
                .Pipe(r => Regex.Replace(r, "(\\)\\)\\)|VIS|CR|BP|DD|SO|DR),", string.Empty))
                .Pipe(r => Regex.Replace(r, "(?<=[a-zA-Z]),", string.Empty))
                .Pipe(r => Regex.Replace(r, ", ", ",\"0\""));
        }
    }
}