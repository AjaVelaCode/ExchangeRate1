using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Models
{
    public class ExchangeRateStatisticsDto
    {
        public decimal MaximumRate { get; set; }
        public decimal MinimumRate { get; set; }
        public decimal AverageRate { get; set; }

    }
}
