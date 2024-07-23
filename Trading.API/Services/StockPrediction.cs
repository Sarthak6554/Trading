using System.Text;
using Trading.API.Factory.DataSourceFactory;
using Trading.API.Interface;
using Trading.API.Models;
using Trading.DAL.Interface;

namespace Trading.API.Services
{
    public class StockPrediction : IStockPrediction
    {
        private readonly DataSourceFactoryResolver _factoryResolver;
        private readonly IStockPredictionAlgorithm _stockPredictionAlgorithm;
        private readonly IConfiguration _configuration;

        public StockPrediction(DataSourceFactoryResolver factoryResolver, IConfiguration configuration, IStockPredictionAlgorithm stockPredictionAlgorithm)
        {
            _factoryResolver = factoryResolver;
            _configuration = configuration;
            _stockPredictionAlgorithm = stockPredictionAlgorithm;
        }
        public dynamic PredictValues(List<SampleDetails> sampleDetails)
        {
            List<string> existingExchangeDirectories = new();
            Dictionary<string, List<StockInfo>> processedStocks = new();
            int batchSize = 0;

            try
            {
                if (sampleDetails.Count == 0)
                {
                    return new { Status = "Failed", Description = "No stocks to process" };
                }

                string[] directories = Directory.GetDirectories(_configuration["exchangeRootPath"]);
                int.TryParse(_configuration["batchSize"], out batchSize);

                // Print all directory names
                foreach (string directory in directories)
                {
                    existingExchangeDirectories.Add(Path.GetFileName(directory));
                }

                foreach (var requestedSample in sampleDetails)
                {
                    string requestStockExchange = existingExchangeDirectories.FirstOrDefault(x => x.ToLower() == requestedSample.StockExchange.ToString().ToLower());
                    if (!string.IsNullOrEmpty(requestStockExchange))
                    {
                        string stockFilePath = Path.Combine(_configuration["exchangeRootPath"], requestStockExchange);
                        var factory = _factoryResolver.GetFactory(requestedSample.SourceType.ToString());
                        var stockReader = factory.CreateStockReader();

                        processedStocks = factory.CreateStockReader().ReadExchangeStocks(requestedSample, stockFilePath);

                        foreach (var stock in processedStocks)
                        {
                            List<StockInfo> randomStockSeries = GetRandomDataPoints(stock.Value, batchSize);

                            List<StockInfo> predictedStocksPrices = _stockPredictionAlgorithm.ForecastStockPrices(randomStockSeries);

                            CreateCSVFile(Path.Combine(stockFilePath, $"{stock.Key}_Output.csv"), randomStockSeries, predictedStocksPrices);
                        }
                    }
                }
                return new { Status = "Success", Description = "All Stock Predicted Successfully" };
            }
            catch (Exception ex)
            {
                return new { Status = "Failed", Description = ex.Message };
            }
        }

        private List<StockInfo> GetRandomDataPoints(List<StockInfo> stocks, int batchSize)
        {
            List<StockInfo> randomStockSeries = new();
            if (stocks.Count > batchSize)
            {
                Random rand = new Random();
                int startIndex = rand.Next(stocks.Count - 10);
                randomStockSeries = stocks.Skip(startIndex).Take(10).ToList();
            }

            return randomStockSeries;
        }

        private bool CreateCSVFile(string outputFilePath, List<StockInfo> initialValues, List<StockInfo> predictedStocksPrices)
        {
            StringBuilder csvContent = new StringBuilder();

            foreach (var item in initialValues)
            {
                csvContent.AppendLine($"{item.Id},{item.Date:dd-MM-yyyy},{item.Price}");
            }

            foreach (var item in predictedStocksPrices)
            {
                csvContent.AppendLine($"{item.Id},{item.Date:dd-MM-yyyy},{item.Price}");
            }

            // Write to file
            File.WriteAllText(outputFilePath, csvContent.ToString());

            return true;
        }
    }
}
