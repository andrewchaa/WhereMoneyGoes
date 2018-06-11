using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FunctionalWay;
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
        private readonly IStatementCleaner _cleaner;

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
            var csvString = string.Empty;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                csvString = Encoding.UTF8.GetString(stream.ToArray());
            }

            return csvString
                .Split("\n")
                .Where(line => line.Length > 0)
                .Select(r => Cleanse(r))
                .Skip(1)
                .Select(line => new Row(line))
                .Select(row => _statementParser.Parse(row.Cells))
                .Map(trans => new Summary(trans))
                .Map(summary => Ok(summary));
        }

        private static string Cleanse(string row)
        {
            return Regex.Replace(row, 
                "(\\)\\)\\)|VIS|CR|BP|DD|SO|DR),", 
                string.Empty)
                .Map(r => Regex.Replace(r, ", ", ",\"0\""));
        }
    }
}
