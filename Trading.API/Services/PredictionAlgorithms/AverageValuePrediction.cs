using Trading.API.Models;
using System.Collections;
using Trading.API.Interface;

namespace Trading.API.Services.PredictionAlgorithms
{
    public class AverageValuePrediction : IStockPredictionAlgorithm
    {
        public List<StockInfo> ForecastStockPrices(List<StockInfo> initialValues)
        {
            List<StockInfo> predictedValues = new List<StockInfo>();

            // Atleast 2 values should be there to predict the values
            if (initialValues.Count >= 2)
            {
                double secondHighestValue = initialValues.OrderByDescending(stock => stock.Price).Skip(1).First().Price;

                double n1 = secondHighestValue;
                double n2 = initialValues[initialValues.Count - 1].Price + (n1 - initialValues[initialValues.Count - 1].Price) / 2;
                double n3 = n1 + (n2 - n1) / 4;

                DateTime lastTimestamp = initialValues[initialValues.Count - 1].Date;

                predictedValues.Add(new StockInfo { Id = initialValues[0].Id, Date = lastTimestamp.AddDays(1), Price = Math.Round(n1, 2) });
                predictedValues.Add(new StockInfo { Id = initialValues[0].Id, Date = lastTimestamp.AddDays(2), Price = Math.Round(n2, 2) });
                predictedValues.Add(new StockInfo { Id = initialValues[0].Id, Date = lastTimestamp.AddDays(3), Price = Math.Round(n3, 2) });
            }
            return predictedValues;
        }
    }
}
