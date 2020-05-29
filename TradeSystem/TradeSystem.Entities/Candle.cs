﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSystem.Entities
{
    public class Candle : OHLCV
    {
        #region Properties
        public uint Index { get; set; }
        public bool IsRed => Open > Close;

        public bool IsGreen => Open <= Close;

        public decimal Body => Math.Abs(Close - Open);

        public decimal Length => Math.Abs(High - Low);

        public decimal LowerWick => IsGreen ? Open - Low : Close - Low;

        public decimal UpperWick => IsGreen ? High - Open : High - Close;

        public bool HasLowerWick => LowerWick > 0;

        public bool HasUpperWick => UpperWick > 0;

        public bool IsDoji => Open == Close && Low != Open && High != Open;

        public bool IsMarubozu => Open == Low && Close == High;

        public bool IsValid => Open > 1.0m && High > 1.0m && Low > 1.0m && Close > 1.0m;

        public bool IsClosed { get; set; }

        public ulong CandleVolume { get; set; }

        #endregion

        #region Methods

        public bool IsBullishHarami(Candle previousCandle)
        {
            if (previousCandle.IsGreen)
                return false;
            if (Open < previousCandle.Open && Open > previousCandle.Close && Close < previousCandle.Open && Close > previousCandle.Close)
                return true;
            return false;
        }

        public bool IsBearishHarami(Candle previousCandle)
        {
            if (previousCandle.IsRed)
                return false;
            if (Open > previousCandle.Open && Open < previousCandle.Close && Close > previousCandle.Open && Close < previousCandle.Close)
                return true;
            return false;
        }

        public bool IsBullishEngulfing(Candle previousCandle)
        {
            if (previousCandle.IsGreen)
                return false;
            if (Open < previousCandle.Open && Open < previousCandle.Close && Close > previousCandle.Open && Close > previousCandle.Close)
                return true;
            return false;
        }

        public bool IsBearishEngulfing(Candle previousCandle)
        {
            if (previousCandle.IsRed)
                return false;
            if (Open > previousCandle.Open && Open > previousCandle.Close && Close < previousCandle.Open && Close < previousCandle.Close)
                return true;
            return false;
        }

        public bool IsPiercingLine(Candle previousCandle)
        {
            if (previousCandle.IsGreen)
                return false;
            decimal previousCandleMidPoint = previousCandle.Low + (previousCandle.High - previousCandle.Low) / 2;
            if (Open < previousCandle.Low && Close > previousCandleMidPoint)
                return true;
            return false;
        }

        public bool IsDarkCloudCover(Candle previousCandle)
        {
            if (previousCandle.IsRed)
                return false;
            decimal previousCandleMidPoint = previousCandle.Low + (previousCandle.High - previousCandle.Low) / 2;
            if (Open < previousCandle.High && Close < previousCandleMidPoint)
                return true;
            return false;
        }

        public bool IsHammer(Candle previousCandle)
        {
            decimal height = High - Low;
            decimal body = Close - Open;

            decimal lowerShadow = Close - Low;
            decimal upperShadow = High - Open;

            if (lowerShadow / body >= 2 && upperShadow < body)
                return true;
            return false;
        }

        public bool IsHangingMan(Candle previousCandle)
        {
            decimal height = High - Low;
            decimal body = Close - Open;

            decimal lowerShadow = Close - Low;
            decimal upperShadow = High - Open;

            if (upperShadow / body >= 2 && lowerShadow < body)
                return true;
            return false;
        }

        public bool IsContainedIn(Candle candle)
        {
            return candle.Open < Open && candle.Close > Close;
        }

        public bool Contains(Candle candle)
        {
            return Open < candle.Open && Close > candle.Close;
        }

        public override string ToString()
        {
            string color = "None";
            color = IsRed ? "Red" : "Green";
            return $"O={Open}, H={High}, L={Low}, C={Close}, {color}";
        }

        #endregion

        #region Helper Methods

        public void Print()
        {
            var color = IsGreen ? "Green" : "Red";
            Console.WriteLine($"T={TimeStamp.ToShortTimeString()}, O={Open.ToString("0.####")}, H={High.ToString("0.####")}, L={Low.ToString("0.####")}, C={Close.ToString("0.####")}, V={Volume}, NV={CandleVolume}, Color = {color}");
        }

        #endregion
    }
}
