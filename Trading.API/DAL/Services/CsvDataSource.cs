using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.API.Models;
using Trading.DAL.Interface;

namespace Trading.DAL.Services
{
    public class CsvDataSource : IDataSourceAdapter
    {
        public Dictionary<string, List<StockInfo>> ReadExchangeStocks(SampleDetails sampleInfo, string filePath)
        {
            Dictionary<string, List<StockInfo>> processedStocks = new Dictionary<string, List<StockInfo>>();
            List<StockInfo> stocksInfo = new List<StockInfo>();
            int stocksToPredict = 0;

            try
            {
                string[] files = Directory.GetFiles(filePath, $"*.{sampleInfo.SourceType.ToString().ToLower()}");
                if(sampleInfo.Number > 0)
                {
                    stocksToPredict = sampleInfo.Number > files.Length ? files.Length : sampleInfo.Number;
                }

                for (int i = 0; i < stocksToPredict; i++)
                {
                    stocksInfo = new();
                    string stockPath = Path.Combine(filePath, files[i]);
                    var lines = File.ReadAllLines(stockPath);

                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        stocksInfo.Add(new StockInfo
                        {
                            Id = parts[0],
                            Date = DateTime.ParseExact(parts[1], "dd-MM-yyyy", null),
                            Price = double.Parse(parts[2])
                        });
                    }

                    processedStocks.Add(stocksInfo[0].Id, stocksInfo);
                }       
            }
            catch(Exception ex)
            {
                throw;
            }

            return processedStocks;
        }
    }
}
