using System.Collections.Generic;
using Calme.Domain.Models;

namespace Calme.Domain.Categories
{
    public class Barclaycard
    {
        public static IDictionary<string, ExpenseCategories> Items = new Dictionary<string, ExpenseCategories>
        {
            {"Amt Coffee Eastbourne", ExpenseCategories.Subsistence},
            {"Bagelman Brighton", ExpenseCategories.Subsistence},
            {"Caffe Nero Eastbourne Eastbourne", ExpenseCategories.Subsistence},
            {"Costa [A-Z|a-z|\\s|@]+", ExpenseCategories.Subsistence},
            {"Coffee Station London E", ExpenseCategories.Subsistence},
            {"Dominos Pizza UK & Irelan Milton Keynes", ExpenseCategories.Subsistence},
            {"Eat London ECM", ExpenseCategories.Subsistence},
            {"GB Stratford Westfi London", ExpenseCategories.Subsistence},
            {"Greggs Chingford", ExpenseCategories.Subsistence},
            {"Hotel Chocolat Eastbourne", ExpenseCategories.Subsistence},
            {"IZ *Urban Ground Eastbourne", ExpenseCategories.Subsistence},
            {"JustgivingCom London N BN", ExpenseCategories.Charity},
            {"Painting Pottery Cafe Brighton BN", ExpenseCategories.Subsistence},
            {"Percy Ingle", ExpenseCategories.Subsistence},
            {"Pret A Manger .*", ExpenseCategories.Subsistence},
            {"Starbucks Eastbourne Eastbourne", ExpenseCategories.Subsistence},
            {"[\\s|A-Z|a-z|/]?Tfl Travel CH[\\s|A-Z|a-z|/]?", ExpenseCategories.PublicTransport},
            {"New Southern Railw Eastbourn", ExpenseCategories.PublicTransport},
            {"Trainline London", ExpenseCategories.PublicTransport},
            {"The Works Eastbourne", ExpenseCategories.Stationery},
            {"Uber Eats ZLRH HelpUber helpubercom", ExpenseCategories.Subsistence},
            {"Wasabi Liverpool Kiosk London", ExpenseCategories.Subsistence},
            {"Waterstones Eastbourne", ExpenseCategories.Book},
            {"WwwPatisserie-Valerie", ExpenseCategories.Subsistence    },
            {"Yoku Sushi Limited Eastbourne", ExpenseCategories.Subsistence},
        };
    }
}