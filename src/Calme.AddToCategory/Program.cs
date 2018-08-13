using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace Starex.AddToCategory
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new AmazonDynamoDBClient();
           
            var categories = Table.LoadTable(client, "expense-categories");
            
            var category = new Document();
            category["Category"] = "Test";
            categories.PutItemAsync(category);
            
            Console.WriteLine("Hello World!");
            
        }
    }
}