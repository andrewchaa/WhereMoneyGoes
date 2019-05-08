using System;
using Wmg.App.Domain.Models;
using Xunit;

namespace Calme.Tests
{
    public class MoneyTransactionTests
    {

        [Fact]
        public void Should_handle_empty_value_for_money()
        {
            // act
            var transaction = ExpenseTransaction.Parse(Bank.Hsbc, new []{
                "26 Mar 2018",
                "xxx",
                "PIZZA EXPRESS LONDON  3223",
                "3.88",
                " "});

            // assert
            Assert.Equal(3.88m, transaction.PaidOut);
            Assert.Equal(0, transaction.PaidIn);
        }

        [Fact]
        public void Should_handle_empty_value_for_paidout()
        {
            // act
            var transaction = ExpenseTransaction.Parse(Bank.Hsbc, new []
            {
                "29 Mar 2018",
                "xxx",
                "ARROWS GROUP PROFE ARROWS GROUP PROFE ",
                " ",
                "11400.00"                
            });

            // assert
            Assert.Equal(0m, transaction.PaidOut);
            Assert.Equal(11400.00m, transaction.PaidIn);
        }

        [Fact]
        public void Should_handle_csv_for_barclaycard()
        {
            // act
            var transaction = ExpenseTransaction.Parse(Bank.Barclaycard, new []
            {
                "21 Apr 18", 
                " Waitrose 834, Clifton8.10 POUND STERLING GREAT BRITAIN ", 
                "Visa",
                "MS H LEE", 
                "Groceries", 
                "", 
                "8.10"
            });

            // assert
            Assert.Equal(new DateTime(2018, 4, 21), transaction.Date);
            Assert.Equal("Waitrose 834, Clifton8.10 POUND STERLING GREAT BRITAIN", transaction.Description);
            Assert.Equal(8.10m, transaction.PaidOut);
        }

        
    }
}