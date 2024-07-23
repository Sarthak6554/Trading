using Trading.DAL.Interface;

namespace Trading.API.Factory.DataSourceFactory
{
    public interface IDataSourceFactory
    {
        IDataSourceAdapter CreateStockReader();
    }
}
