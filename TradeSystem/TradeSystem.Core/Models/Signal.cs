using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSystem.Core.Models
{
    public enum SignalType
    {
        None,
        LongEntry,
        ShortEntry,
        LongExit,
        ShortExit,
    }
    public class Signal
    {
        #region Properties

        public SignalType Type { get; set; }
        public int Strength { get; set; }
        public decimal Price { get; set; }
        #endregion

        #region Constructor

        public Signal()
        {

        }

        public Signal(SignalType type, int strength)
        {
            this.Type = type;
            this.Strength = strength;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"Price = {Price}, Type = {Type}, Strength = {Strength}%";
        }

        #endregion

    }
}
