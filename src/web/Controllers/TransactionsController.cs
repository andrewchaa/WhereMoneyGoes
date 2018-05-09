using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using QuickExpense.Domain.Models;

namespace QuickExpense.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost]
        [Route("{bank}")]
        public async Task<IActionResult> Post([FromRoute] Bank bank, 
            IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var csv = Csv.From(bank, Encoding.UTF8.GetString(stream.ToArray()));

                var transactions = new List<MoneyTransaction>();
                foreach (var row in csv.Rows)
                {
                    _logger.LogInformation($"row: {row}");
                    transactions.Add(MoneyTransaction.Parse(bank, row.Cells));
                }

                return Ok(new Summary(transactions));
            }
        }
    }
}
