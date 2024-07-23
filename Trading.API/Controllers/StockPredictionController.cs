using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trading.API.Interface;
using Trading.API.Models;

namespace Trading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockPredictionController : ControllerBase
    {
        private readonly IStockPrediction _stockPrediction;

        public StockPredictionController(IStockPrediction stockPrediction)
        {
            _stockPrediction = stockPrediction;
        }

        [HttpPost("predictStockValues")]
        public IActionResult PredictStockValues([FromBody] List<SampleDetails> sampleDetails)
        {
            try
            {
                var result = _stockPrediction.PredictValues(sampleDetails);
                return Convert.ToString(result?.Status) == "Success" ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Invalid Request", Description = ex.Message });
            }

        }
    }
}
