# Quick Expense

## APIs

* POST /api/transactions: send csv statement to parse
* GET /api/items/unmatched: show the unmatched expense items

Since I started contracting, I have to submit monthly expense report to the account system. This is to help and to alleviate the pain of summarising all expenses from my bank and card statements.

To run for reporting

    dotnet Report/bin/Debug/netcoreapp2.0/Report.dll ~/Downloads/Transactions.csv
