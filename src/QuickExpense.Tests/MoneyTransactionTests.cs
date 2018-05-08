using System.Linq;
using System.Threading.Tasks;
using QuickExpense.Domain.Models;
using Xunit;

namespace QuickExpense.Tests
{
    public class MoneyTransactionTests
    {

        [Fact]
        public void Should_handle_empty_value_for_money()
        {
            // act
            var transaction = MoneyTransaction.Parse(
                "26 Mar 2018",
                "PIZZA EXPRESS LONDON  3223",
                "3.88",
                " ");

            // assert
            Assert.Equal(3.88m, transaction.PaidOut);
            Assert.Equal(0, transaction.PaidIn);
        }

        [Fact]
        public void Should_handle_empty_value_for_paidout()
        {
            // act
            var transaction = MoneyTransaction.Parse(
                "29 Mar 2018",
                "ARROWS GROUP PROFE ARROWS GROUP PROFE ",
                " ",
                "11400.00");

            // assert
            Assert.Equal(0m, transaction.PaidOut);
            Assert.Equal(11400.00m, transaction.PaidIn);
        }

        
    }
}