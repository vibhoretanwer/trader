using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TradeSystem.Entities;

namespace TradeSystem.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Candle> candles = ReadTestData();

            Console.WriteLine("Hello World!");
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
