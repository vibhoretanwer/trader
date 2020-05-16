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
        static AutoResetEvent waitHandle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            //List<Candle> candles = ReadTestData();

            Start();
            waitHandle.WaitOne();
            Console.ReadKey();
        }

        private static void Start()
        {
            var dataStream = TrueDataAPIManager.Instance.HistoricalDataStream;
            dataStream.Subscribe("NIFTY-I", Common.TrueData.Interval.EOD, new DateTime(2019, 05, 07), new DateTime(2020, 05, 30));
            dataStream.OnCandleRecieved += HistoricalDataStream_OnCandleRecieved;
        }

        static decimal pl;
        static decimal totalPL;
        static List<Signal> signals = new List<Signal>();
        private static void HistoricalDataStream_OnCandleRecieved(object sender, CandleRecievedEventArgs args)
        {
            //Console.WriteLine(args.Candle.ToString());
            analyzer.Update(args.Candle);
            var signal = analyzer.Analyze();
            if(signal.Type != SignalType.None)
            {
                pl = 0;
                if (signal.Type == SignalType.LongEntry)
                {
                    var lastSignal = signals.LastOrDefault();

                    if (lastSignal != null && lastSignal.Type == SignalType.ShortEntry) pl = (lastSignal.Price - signal.Price);
                }
                if (signal.Type == SignalType.ShortEntry)
                {
                    var lastSignal = signals.LastOrDefault();
                    if (lastSignal != null && lastSignal.Type == SignalType.LongEntry) pl = (signal.Price - lastSignal.Price);
                }
                totalPL += pl;
                if (signals.Count == 0 || signals.Last().Type != signal.Type)
                    signals.Add(signal);
                Console.WriteLine(analyzer.Model.Candles.Last().TimeStamp.ToShortDateString() + $" - {signal.Type} @ {signal.Price}, PL = {pl}, Total PL = {totalPL}");
                Console.ReadKey();
            }
        }

        private static List<Candle> ReadTestData()
        {
            uint index = 1;
            List<Candle> candles = new List<Candle>();

            string[] records = File.ReadAllLines(@"D:\StockData\Nifty 50.csv");
            records = records.Skip(1).Reverse().ToArray();
            foreach (string record in records)
            {
                string[] fields = record.Split(',');
                DateTime date = DateTime.Parse(fields[0]);
                decimal close = decimal.Parse(fields[1].Trim('\"'));
                decimal open = decimal.Parse(fields[2].Trim('\"'));
                decimal high = decimal.Parse(fields[3].Trim('\"'));
                decimal low = decimal.Parse(fields[4].Trim('\"'));
                ulong volume = (ulong)(decimal.Parse(fields[5].Substring(0, fields[5].Length - 1))) * 1000;

                Candle candle = new Candle()
                {
                    Index = index++,
                    TimeStamp = date,
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume,
                };
                candles.Add(candle);
            }

            return candles;
        }
    }
}
