using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
        
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var csv = Csv.From(Encoding.UTF8.GetString(stream.ToArray()));

                var transactions = new List<MoneyTransaction>();
                foreach (var row in csv.Rows)
                {
                    _logger.LogInformation($"row: {row}");
                    transactions.Add(MoneyTransaction.Parse(
                        row.Cells[0],
                        row.Cells[2],
                        row.Cells[3],
                        row.Cells[4]
                        ));
                }
//                var transactions = csv.Rows.Select(r => new MoneyTransaction(
//                    DateTime.ParseExact(r.Cells[0], "dd MMM yyyy", CultureInfo.CurrentCulture),
//                    r.Cells[2],
//                    Decimal.Parse(r.Cells[3]),
//                    Decimal.Parse(r.Cells[4])
//                ));
                
                
                return Ok(transactions);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
