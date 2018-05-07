using System;
using System.Threading.Tasks;
using QuickExpense.Controllers;
using QuickExpense.Domain.Models;
using Xunit;

namespace QuickExpense.Tests
{
    public class CsvTests
    {
        private readonly string _csvString;

        public CsvTests()
        {
            _csvString = "Date,Type,Description,Paid out,Paid in,Balance\n" +
                         "\"26 Mar 2018\",VIS,\"PIZZA EXPRESS LONDON  3223 \",\"23.50\", ,\n" +
                         "\"26 Mar 2018\",))),\"TESCO STORES 5158 LONDON \",\"0.38\", ,\n" +
                         "\"26 Mar 2018\",))),\"THAI EXPRESS LONDON \",\"6.85\", ,\n";
            
        }
        
        [Fact]
        public async Task Should_parse_new_line()
        {
            // act
            var csvs = Csv.From(_csvString);

            // assert
            Assert.Equal(3, csvs.Count);
        }

        [Fact]
        public async Task Should_populate_csv()
        {
            
        }
    }
}