using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSystem.Common
{
    public static class NumberExtensions
    {
        public static decimal Percentage(this decimal number, decimal total)
        {
            return total <= 0 ? 0 : (number / total) * 100;
        }

        public static int Percentage(this int number, int total)
        {
            return total <= 0 ? 0 : (number / total) * 100;
        }
    }
}
