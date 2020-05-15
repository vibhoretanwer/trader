using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TradeSystem.Common;
using TradeSystem.Common.TrueData;

namespace TradeSystem.Service.Analysis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Start();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void Start()
        {
            var dataStream = TrueDataAPIManager.Instance.HistoricalDataStream;
            dataStream.Subscribe("NIFTY-I", Common.TrueData.Interval.EOD, new DateTime(2019, 01, 01), new DateTime(2020, 05, 24));
            dataStream.OnCandleRecieved += HistoricalDataStream_OnCandleRecieved;
        }

        private static void HistoricalDataStream_OnCandleRecieved(object sender, CandleRecievedEventArgs args)
        {
            Debug.WriteLine(args.Candle.ToString());
        }
    }
}
