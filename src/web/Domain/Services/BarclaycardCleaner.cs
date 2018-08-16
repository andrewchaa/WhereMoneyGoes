using System.Text.RegularExpressions;
using FunctionalWay.Extensions;

namespace Calme.Domain.Services
{
    public class BarclaycardCleaner : IClean
    {
        public string Clean(string row)
        {
            return row
                .Pipe(r => Regex.Replace(r, "(\"[^\",]+),([^\"]+\")", "$1$2"))
                .Pipe(r => Regex.Replace(r, "\\sPOUND STERLING (GREAT BRITAIN|LUXEMBOURG)", string.Empty))
                .Pipe(r => Regex.Replace(r, "\"[0-9]+\"", string.Empty))
                ;
        }
    }
}