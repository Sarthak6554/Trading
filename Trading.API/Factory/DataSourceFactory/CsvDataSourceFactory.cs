using Trading.DAL.Interface;
using Trading.DAL.Services;

namespace Trading.API.Factory.DataSourceFactory
{
    public class CsvDataSourceFactory : IDataSourceFactory
    {
        public IDataSourceAdapter CreateStockReader()
        {
            return new CsvDataSource();
        }
    }
}
