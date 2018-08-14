namespace Calme.Domain.Services
{
    public class BarclaycardCleaner : IClean
    {
        public string Clean(string row)
        {
            return row;
        }
    }

    public interface IClean
    {
    }
}