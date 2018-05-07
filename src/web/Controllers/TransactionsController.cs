using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly ILogger _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var csvs = Csv.From(Encoding.UTF8.GetString(stream.ToArray()));
                _logger.LogInformation($"Count: {csvs.Count}");
                return Ok(csvs);
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
