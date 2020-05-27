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
            TickManager tickManager = new TickManager();
            tickManager.Start();

            Console.ReadKey();
        }
    }
}
