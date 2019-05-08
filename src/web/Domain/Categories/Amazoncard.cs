using System.Collections.Generic;
using Wmg.App.Domain.Models;

namespace Wmg.App.Domain.Categories
{
    public class Amazoncard
    {
        public static IDictionary<string, ExpenseCategories> Items = new Dictionary<string, ExpenseCategories>
        {
            { "1ST SOUVENIRS LTD .+", ExpenseCategories.Subsistence},
            { "Amazon.co.uk .+", ExpenseCategories.Equipment},
            { "AMZN Mktp UK .+", ExpenseCategories.Equipment},
            { "APPLE ONLINE STORE .+", ExpenseCategories.Equipment},
            { "AROMA CHINGFORD LTD .+", ExpenseCategories.Subsistence},
            { "BANH MI BAY .+", ExpenseCategories.Subsistence},
            { "BILLS EASTBOURNE .+", ExpenseCategories.Subsistence},
            { "BP CHURCH VIEW CONNECT .+", ExpenseCategories.Subsistence},
            { "Cineworld ENF CP .+", ExpenseCategories.Subsistence},
            { "CO-OP GROUP FOOD .+", ExpenseCategories.Subsistence},
            { "COSTA COFFEE .+", ExpenseCategories.Subsistence},
            { "EAT .+", ExpenseCategories.Subsistence},
            { "FOOMUM .+", ExpenseCategories.Subsistence},
            { "ICELAND .+", ExpenseCategories.Subsistence},
            { "IKEA LIMITED .+", ExpenseCategories.Fixture},
            { "IZ \\*Foodgangnamstyle .+", ExpenseCategories.Subsistence},
            { "IZ \\*Handcrafted Coffee .+", ExpenseCategories.Subsistence},
            { "Just Eat .+", ExpenseCategories.Subsistence},
            { "KIMCHEE HOLBORN .+", ExpenseCategories.Subsistence},
            { "NON STERLING TRANSACTION FEE", ExpenseCategories.Sundries},
            { "NOO NOODLES .+", ExpenseCategories.Subsistence},
            { "PAYGATE_INTERNET SHOP .+", ExpenseCategories.Book},
            { "PAYMENT RECEIVED - THANK YOU", ExpenseCategories.Exclude},
            { "PHO ST PAUL'S .+", ExpenseCategories.Subsistence},
            { "PIZZA UNION .+", ExpenseCategories.Subsistence},
            { "POD .+", ExpenseCategories.Subsistence},
            { "PRET A MANGER .+", ExpenseCategories.Subsistence},
            { "SAINSBURYS SACAT .+", ExpenseCategories.Subsistence},
            { "SHELL LARKSHALL RD .+", ExpenseCategories.Subsistence},
            { "STARBUCKS .+", ExpenseCategories.Subsistence},
            { "SUBWAY HIGHAM PARK .+", ExpenseCategories.Subsistence},
            { "SUMUP  \\*O BITE2 .+", ExpenseCategories.Subsistence},
            { "TESCO STORES .+", ExpenseCategories.Subsistence},
            { "TEX NORTH WEALD .+", ExpenseCategories.Subsistence},
            { "TFL TRAVEL CH .+", ExpenseCategories.PublicTransport},
            { "THE CITY PRIDE .+", ExpenseCategories.PublicTransport},
            { "WASABI .+", ExpenseCategories.PublicTransport},
            { "WELLBEING KITCHEN .+", ExpenseCategories.PublicTransport},
            { "WORDPRESS .+", ExpenseCategories.Software},
            { "WWW.BUSYBEES.COM .+", ExpenseCategories.Childcare},
            { "WWW.DOMINOS.CO.UK .+", ExpenseCategories.Subsistence},
            { "YEN .+", ExpenseCategories.Subsistence},
            { "YOKU SUSHI .+", ExpenseCategories.Subsistence},
        };
    }
}