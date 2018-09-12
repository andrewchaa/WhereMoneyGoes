using System.Collections.Generic;

namespace Calme.Domain.Models
{
    public class BarclaycardCategories
    {
        public static IDictionary<string, Category> Items = new Dictionary<string, Category>
        {
            {"Amt Coffee Eastbourne", Category.Subsistence},
            {"Bagelman Brighton", Category.Subsistence},
            {"Caffe Nero Eastbourne Eastbourne", Category.Subsistence},
            {"Costa [A-Z|a-z|\\s|@]+", Category.Subsistence},
            {"Coffee Station London E", Category.Subsistence},
            {"Dominos Pizza UK & Irelan Milton Keynes", Category.Subsistence},
            {"Eat London ECM", Category.Subsistence},
            {"GB Stratford Westfi London", Category.Subsistence},
            {"Greggs Chingford", Category.Subsistence},
            {"Hotel Chocolat Eastbourne", Category.Subsistence},
            {"IZ *Urban Ground Eastbourne", Category.Subsistence},
            {"JustgivingCom London N BN", Category.Charity},
            {"Painting Pottery Cafe Brighton BN", Category.Subsistence},
            {"Percy Ingle", Category.Subsistence},
            {"Pret A Manger .*", Category.Subsistence},
            {"Starbucks Eastbourne Eastbourne", Category.Subsistence},
            {"[\\s|A-Z|a-z|/]?Tfl Travel CH[\\s|A-Z|a-z|/]?", Category.PublicTransport},
            {"New Southern Railw Eastbourn", Category.PublicTransport},
            {"Trainline London", Category.PublicTransport},
            {"The Works Eastbourne", Category.Stationery},
            {"Uber Eats ZLRH HelpUber helpubercom", Category.Subsistence},
            {"Wasabi Liverpool Kiosk London", Category.Subsistence},
            {"Waterstones Eastbourne", Category.Book},
            {"WwwPatisserie-Valerie", Category.Subsistence},
            {"Yoku Sushi Limited Eastbourne", Category.Subsistence},
        };
    }
}