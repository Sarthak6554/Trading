using Trading.API.Models;

namespace Trading.API.Interface
{
    public interface IStockPredictionAlgorithm
    {
        public List<StockInfo> ForecastStockPrices(List<StockInfo> initialValues);
    }
}
