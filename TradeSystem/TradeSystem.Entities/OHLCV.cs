using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSystem.Entities
{
    public class OHLCV : OHLC
    {
        #region Properties

        public ulong Volume { get; set; }

        #endregion
    }
}
