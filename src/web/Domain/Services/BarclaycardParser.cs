using System;
using System.Collections.Generic;
using System.Globalization;
using FunctionalWay;
using Microsoft.Extensions.Logging;
using QuickExpense.Domain.Models;

namespace QuickExpense.Domain.Services
{
    public class BarclaycardParser : IStatementParser
    {
        private readonly ILogger<BarclaycardParser> _logger;

        public BarclaycardParser(ILogger<BarclaycardParser> logger)
        {
            _logger = logger;
        }
        
        public MoneyTransaction Parse(IList<string> columns)
        {
            return new MoneyTransaction(
                columns[0].Map(c => DateTime.ParseExact(c, "dd MMM yy", CultureInfo.InvariantCulture)),
                columns[1].Trim(),
                columns[1].Trim().Map(FindCategory),
                columns[6]
                    .Map(c => c.Trim())
                    .Map(c =>
                    {
                        try { return !string.IsNullOrEmpty(c) ? decimal.Parse(c) : 0; }
                        catch (Exception e)
                        {
                            _logger.LogError(e, $"Input: {c}");
                            throw;
                        }
                    }),
                0
            );
        }
        
        private static string FindCategory(string description)
        {
            return description.Map(d => Categories.Items.ContainsKey(d)
                ? Categories.Items[d]
                : "Uncategories");
        }

    }
}