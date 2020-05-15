using System;
using System.Collections.Generic;
using System.Text;
using TradeSystem.Core.Models;

namespace TradeSystem.Core.Interfaces
{
    public interface IStrategy
    {
        Signal Apply(AnalysisModel model);
    }
}
