using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionalWay.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wmg.App.Domain.Models;
using Wmg.App.Domain.Services;

namespace Wmg.App.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IStatementParser _statementParser;
        private readonly IClean _transactionCleaner;

        public TransactionsController(
            ILogger<TransactionsController> logger,
            IStatementParser statementParser, 
            IClean transactionCleaner)
        {
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
