using System;
using System.Collections.Generic;
using System.Globalization;
using Calme.Domain.Models;
using FunctionalWay.Extensions;
using Microsoft.Extensions.Logging;
using SanPellgrino;

namespace Calme.Domain.Services
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
                    .Tee(cs => _logger.LogInformation($"columns: {cs.ToCsvString()}") )
                    .Map(cs => new ExpenseTransaction(
                        cs[0]
                            .Tee(c => _logger.LogInformation($"date column: {c}"))
                            .Map(c => DateTime.ParseExact(c, "dd MMM yyyy", CultureInfo.InvariantCulture)),
                        cs[1].Trim(),
                        cs[1].Trim().Map(FindCategory),
                        cs[2]
                            .Map(c => c.Trim())
                            .Map(c =>
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
        
        private static Category FindCategory(string description)
        {
            return description.Map(d => Categories.Items.ContainsKey(d)
                ? Categories.Items[d]
                : Category.Uncategorized);
        }

    }
}