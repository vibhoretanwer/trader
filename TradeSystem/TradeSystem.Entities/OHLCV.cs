using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSystem.Entities
{
    public class OHLCV : OHLC
    {
        #region Properties

        public ulong Volume { get; set; }

        #endregion
    }
}
