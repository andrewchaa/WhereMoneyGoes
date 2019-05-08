using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using FunctionalWay.Extensions;
using Microsoft.Extensions.Logging;
using SanPellgrino;
using Wmg.App.Domain.Categories;
using Wmg.App.Domain.Models;

namespace Wmg.App.Domain.Services
{
    public class HsbcParser : IStatementParser
    {
        private readonly ILogger<HsbcParser> _logger;

        public HsbcParser(ILogger<HsbcParser> logger)
        {
            _logger = logger;
        }
        
        public ExpenseTransaction Parse(IList<string> columns)
        {
            try
            {
                return columns
                    .Pipe(cs => _logger.LogInformation($"columns: {cs.ToCsvString()}") )
                    .Pipe(cs => new ExpenseTransaction(
                        cs[0].Pipe(c => DateTime.ParseExact(c, "dd MMM yyyy", CultureInfo.InvariantCulture)),
                        cs[1].Trim(),
                        cs[1].Trim().Pipe(FindCategory),
                        cs[2]
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

        private static Func<string, ExpenseCategories> FindCategory = description =>
            Hsbc
                .Items
                .Keys
                .FirstOrDefault(item => Regex.IsMatch(description, item, RegexOptions.IgnoreCase))
                .Pipe(key => key == null
                    ? ExpenseCategories.Uncategorized
                    : Hsbc.Items[key]);
        

    }
}