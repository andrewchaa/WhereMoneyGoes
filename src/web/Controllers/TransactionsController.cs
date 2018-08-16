using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Calme.Domain.Models;
using Calme.Domain.Services;
using FunctionalWay.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Calme.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IStatementParser _statementParser;
        private readonly IStatementCleaner _cleaner;
        private readonly IClean _transactionCleaner;

        public TransactionsController(
            ILogger<TransactionsController> logger,
            IStatementParser statementParser, 
            IClean transactionCleaner)
        {
            _logger = logger;
            _statementParser = statementParser;
            _transactionCleaner = transactionCleaner;
        }
        
        [HttpPost, Route("{bank}")]
        public async Task<IActionResult> Post([FromRoute] Bank bank, IFormFile file)
        {
            string csvString;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                csvString = Encoding.UTF8.GetString(stream.ToArray());
            }

            return csvString
                .Split("\n")
                .Where(line => line.Length > 0)
                .Select(r => _transactionCleaner.Clean(r))
                .Skip(1)
                .Select(line => new Row(line))
                .Select(row => _statementParser.Parse(row.Cells))
                .Pipe(trans => new Summary(trans))
                .Pipe(summary => Ok(summary));
        }
    }
}
