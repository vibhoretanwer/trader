using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TradeSystem.Common;
using TradeSystem.Common.TrueData;
using TradeSystem.Core;
using TradeSystem.Core.Models;
using TradeSystem.Entities;

namespace TradeSystem.TestConsole
{
    class Program
    {
        static Analyzer analyzer = new Analyzer(0);

        static void Main(string[] args)
        {
            TickManager tickManager = new TickManager();
            tickManager.Start();

            Console.ReadKey();
        }
    }
}
