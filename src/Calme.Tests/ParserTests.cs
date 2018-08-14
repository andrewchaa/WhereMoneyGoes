using System;
using Calme.Domain.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Calme.Tests
{
    public class ParsetTests
    {
        [Fact]
        public void Should_parse_barclaycard_statements()
        {
            // arrange
            var columns = new[]
            {
                "21 Apr 18",
                " Waitrose 834, Clifton8.10 POUND STERLING GREAT BRITAIN ",
                "Visa",
                "MS H LEE",
                "Groceries",
                "",
                "8.10"
            };

            // act
            var parser = new HsbcParser(new NullLogger<HsbcParser>());
            var transaction = parser.Parse(columns);
            
            // assert
            Assert.Equal(new DateTime(2018, 4, 21), transaction.Date);
            Assert.Equal("Waitrose 834, Clifton8.10 POUND STERLING GREAT BRITAIN", transaction.Description);
            Assert.Equal(8.10m, transaction.PaidOut);
            Assert.Equal(0m, transaction.PaidIn);
        }
    }
}