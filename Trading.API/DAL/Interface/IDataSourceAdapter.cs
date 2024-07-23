using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.API.Models;

namespace Trading.DAL.Interface
{
    public interface IDataSourceAdapter
    {
        public Dictionary<string, List<StockInfo>> ReadExchangeStocks(SampleDetails sampleInfo, string filePath);
    }
}
