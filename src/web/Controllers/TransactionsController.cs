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
                .Pipe(trans => new Summary(trans))
                .Pipe(summary => Ok(summary));
        }

        private static string Cleanse(string row)
        {
            return row
                    .Pipe(r => Regex.Replace(r, "(\\)\\)\\)|VIS|CR|BP|DD|SO|DR),", string.Empty))
                    .Pipe(r => Regex.Replace(r, "(?<=[a-zA-Z]),", string.Empty))
                    .Pipe(r => Regex.Replace(r, ", ", ",\"0\""));
        }
    }
}
