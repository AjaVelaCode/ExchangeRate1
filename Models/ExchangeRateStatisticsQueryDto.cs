using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Models
{
    public class ExchangeRateStatisticsQueryDto
    {
        public HashSet<DateTime> Dates { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
    }
}
