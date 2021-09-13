using ExchangeRate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private static readonly HttpClient client = new();

        [HttpPost]
        public async Task<ExchangeRateStatisticsDto> GetData([FromBody]ExchangeRateStatisticsQueryDto exchangeRateStatisticsQueryDto)
        {
            List<Task<string>> exchangeRatesResponse = exchangeRateStatisticsQueryDto.Dates
                .Select(async date => await client.GetStringAsync($"https://api.exchangerate.host/convert?" +
                    $"from={exchangeRateStatisticsQueryDto.BaseCurrency}" +
                    $"&to={exchangeRateStatisticsQueryDto.TargetCurrency}" +
                    $"&date={date.ToString("yyyy-MM-dd")}"))
                .ToList();

            var exchangeRatesQuery = exchangeRatesResponse.Select(response => 
                {
                    try
                    {
                        return JObject.Parse(response.Result)["result"].ToObject<decimal>();
                    }
                    catch 
                    {
                        throw new InvalidOperationException("Not possible to fetch data for given parameters");
                    }
                 });
            var exchangeRateStatisticsDto = new ExchangeRateStatisticsDto
            {
                AverageRate = exchangeRatesQuery.Average(),
                MinimumRate = exchangeRatesQuery.Min(),
                MaximumRate = exchangeRatesQuery.Max()
            };
            return exchangeRateStatisticsDto;
        }
    }
}
