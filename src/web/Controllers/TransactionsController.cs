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

                return Ok(new
                {
                    Uncategorised = transactions
                        .Where(t => t.Category == "Uncategorised")
                        .Select(t => t.Description)
                        .Distinct()
                        .OrderBy(t => t),
                    Summary = transactions
                        .OrderBy(t => t.Category)
                        .GroupBy(t => t.Category)
                        .Select(i =>  
                            new
                            {
                                Category = i.Key, 
                                Total = i.Sum(s => s.PaidOut)
                            }
                        ),
                    Total = transactions.Sum(t => t.PaidOut),
                    Count = transactions.Count(),
                    transactions
                });
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
