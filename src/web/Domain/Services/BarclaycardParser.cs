using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Calme.Domain.Models;
using FunctionalWay.Extensions;
using Microsoft.Extensions.Logging;
using SanPellgrino;

namespace Calme.Domain.Services
{
    public class BarclaycardParser : IStatementParser
    {
        private readonly ILogger<BarclaycardParser> _logger;

        public BarclaycardParser(ILogger<BarclaycardParser> logger)
        {
            _logger = logger;
        }
        
        public ExpenseTransaction Parse(IList<string> columns)
        {
            try
            {
                return columns
                    .Pipe(cs => new ExpenseTransaction(
                        cs[0].Pipe(c => DateTime.ParseExact(c, "dd MMM yy", CultureInfo.InvariantCulture)),
                        cs[1]
                            .Pipe(c => Regex.Replace(c, "[0-9]+", string.Empty))
                            .Pipe(c => Regex.Replace(c, "[\\.]", string.Empty))
                            .Pipe(c => c.Replace("  ", " "))
                            .Trim(),
                        cs[1]
                            .Trim()
                            .Pipe(c => Regex.Replace(c, "[0-9]+", string.Empty))
                            .Pipe(c => c.Replace(".", string.Empty))
                            .Pipe(c => c.Replace("  ", " "))
                            .Pipe(FindCategory),
                        cs[6]
                            .Pipe(c => c.Trim())
                            .Pipe(c =>
                            {
                                try { return !string.IsNullOrEmpty(c) ? decimal.Parse(c) : 0; }
                                catch (Exception e)
                                {
                                    _logger.LogError(e, $"Input: {c}");
                                    _logger.LogInformation(columns.ToCsvString());
                                    throw;
                                }
                            }),
                        0)
                    );
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"the input: {string.Join(",", columns)}");
                throw;
            }

        }

        private static Func<string, Category> FindCategory = description =>
            CategoryMatches
                .BarclaycardItems
                .Keys
                .FirstOrDefault(item => Regex.IsMatch(description, item, RegexOptions.IgnoreCase))
                .Pipe(key => key == null
                    ? Category.Uncategorized
                    : CategoryMatches.BarclaycardItems[key]);
    }
}