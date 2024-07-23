namespace Trading.API.Factory.DataSourceFactory
{
    public class DataSourceFactoryResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DataSourceFactoryResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataSourceFactory GetFactory(string dataSourceType)
        {
            switch (dataSourceType.ToLower())
            {
                case "csv":
                    return _serviceProvider.GetService<CsvDataSourceFactory>();
                default:
                    throw new ArgumentException("Unsupported data source type.");
            }
        }
    }
}
