using System.Text.RegularExpressions;
using FunctionalWay.Extensions;

namespace Calme.Domain.Services
{
    public class AmazoncardCleaner : IClean
    {
        public string Clean(string row)
        {
            return row;
        }
    }
}