using KiteConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSystem.Common;
using TradeSystem.Core;

namespace TradeSystem.TestConsole
{
    class Program
    {
        static Analyzer analyzer = new Analyzer(0);
        static void Main(string[] args)
        { 
            KiteAPIManager kiteAPIManager = KiteAPIManager.Instance;
            TickManager tickManager = new TickManager();
            kiteAPIManager.Ticker.OnTick += OnTick;
            tickManager.Start();

            Console.ReadKey();
        }

        private static void OnTick(Tick tickData)
        {
            analyzer.Update(tickData);
        }
    }
}
