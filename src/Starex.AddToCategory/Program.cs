using System;
using Amazon.DynamoDBv2;

namespace Starex.AddToCategory
{
    class Program
    {
        static void Main(string[] args)
        {
            var cilent = new AmazonDynamoDBClient();
            var categories = Table.LoadTable()
            
            Console.WriteLine("Hello World!");
            
        }
    }
}