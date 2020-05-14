using System;
using System.Collections.Generic;
using System.Text;
using TradeSystem.Entities;

namespace TradeSystem.Common.TrueData.Models
{
    public class HistoricalDataStreamContent
    {
        public string Status { get; set; }
        public string SymbolId { get; set; }
        public string Symbol { get; set; }
        public string Interval { get; set; }

        public List<List<string>> data = new List<List<string>>();
    }
}
