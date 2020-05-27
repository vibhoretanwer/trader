using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSystem.Core.Models;

namespace TradeSystem.Core.Interfaces
{
    public interface IStrategy
    {
        Signal Apply(AnalysisModel model);
    }
}
