using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSystem.Common.TrueData
{
    public class HistoricalDataSubscriptionModel
    {
        public string Method { get; set; } = "gethistory";
        public string Interval { get; set; }
        public string Symbol { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
