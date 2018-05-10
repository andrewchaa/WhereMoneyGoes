using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickExpense.Domain.Models;
using QuickExpense.Domain.Services;

namespace QuickExpense.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IStatementParser _statementParser;

        public TransactionsController(
            ILogger<TransactionsController> logger,
            IStatementParser statementParser)
        {
            _logger = logger;
            _statementParser = statementParser;
        }
        
        [HttpPost, Route("{bank}")]
        public async Task<IActionResult> Post(
            [FromRoute] Bank bank, 
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
                    transactions.Add(_statementParser.Parse(row.Cells));
                }

                return Ok(new Summary(transactions));
            }
        }
    }
}
