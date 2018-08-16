using System.Threading.Tasks;
using Calme.Domain.Services;
using Xunit;

namespace Calme.Tests
{
    public class BarclaycardCleanerTests
    {
        [Fact]
        public async Task Clean()
        {
            // arrange
            var row =
                "19 Jul 18,\" Dorothy Perkins, Chingford25.00 POUND STERLING GREAT BRITAIN \",Visa,MS H LEE,Shopping,,25.00";
            var cleaner = new BarclaycardCleaner();

            // act
            var result = cleaner.Clean(row);
            
            // assert
            Assert.Equal("19 Jul 18,\" Dorothy Perkins Chingford25.00 \",Visa,MS H LEE,Shopping,,25.00", 
                result);

        }
    }
}