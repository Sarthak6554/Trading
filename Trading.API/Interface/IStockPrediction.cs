using Trading.API.Models;

namespace Trading.API.Interface
{
    public interface IStockPrediction
    {
        public dynamic PredictValues(List<SampleDetails> sampleDetails);
    }
}
