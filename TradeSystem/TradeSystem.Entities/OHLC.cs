using System;

namespace TradeSystem.Entities
{
    /// <summary>
    /// OHLC class for Open, High, Low, Close data
    /// </summary>
    public class OHLC
    {
        #region Properties

        public string Symbol { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }

        #endregion
    }
}
