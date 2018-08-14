using System.Linq;
using Calme.Domain.Models;
using Xunit;

namespace Calme.Tests
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
        public void Should_parse_new_line()
        {
            // act
            var csv = Csv.From(Bank.Hsbc, _csvString);

            // assert
            Assert.Equal(3, csv.Count);
        }

        [Fact]
        public void Should_populate_csv()
        {
            // act
            var csv = Csv.From(Bank.Hsbc, _csvString);

            // assert
            Assert.Equal(6, csv.Headers.Count());
            Assert.Equal(3, csv.Rows.Count());
        }

        [Fact]
        public void Should_strip_off_double_quotes()
        {
            // act
            var csv = Csv.From(Bank.Hsbc, _csvString);

            // assert
            Assert.Equal("26 Mar 2018", csv.Rows.First().Cells[0]);
        }

        [Fact]
        public void Should_handle_comma_in_the_middle()
        {
            // act
            var csv = Csv.From(Bank.Hsbc, "Date,Type,Description,Paid out,Paid in,Balance\n" + 
                               "\"23 Apr 2018\",))),\"MOTO READING EAST, READING \",\"3.88\", , \n");
            
            // assert
            Assert.Equal("MOTO READING EAST, READING", csv.Rows.First().Cells[2]);
        }

        [Fact]
        public void Should_handle_empty_value()
        {
            // act
            var csv = Csv.From(Bank.Hsbc, "Date,Type,Description,Paid out,Paid in,Balance\n" + 
                               "\"23 Apr 2018\",))),\"MOTO READING EAST, READING \",\"3.88\", , \n");
            
            // assert
            Assert.Equal(string.Empty, csv.Rows.First().Cells[4]);
        }
        
    }
}