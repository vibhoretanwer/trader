using KiteConnect;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSystem.Common
{
    public class TickManager
    {
        #region Data Members

        private Ticker ticker;
        KiteAPIManager kiteAPIManager = KiteAPIManager.Instance;

        #endregion

        #region Constructor

        public TickManager()
        {

        }

        #endregion

        #region Methods

        public void Start()
        {
            ticker = kiteAPIManager.Ticker;
            InitTicker();
        }

        private void InitTicker()
        {
            ticker.OnTick += OnTick;
            ticker.OnReconnect += OnReconnect;
            ticker.OnNoReconnect += OnNoReconnect;
            ticker.OnError += OnError;
            ticker.OnClose += OnClose;
            ticker.OnConnect += OnConnect;
            ticker.OnOrderUpdate += OnOrderUpdate;

            ticker.EnableReconnect(Interval: 5, Retries: 50);
            ticker.Connect();

            SubscribeToInstruments();
        }

        private void SubscribeToInstruments()
        {
            //var tokens = new UInt32[] { 54425863, 54489351 };
            var tokens = new UInt32[] { 306691 };
            ticker.Subscribe(tokens);
            ticker.SetMode(tokens, Mode: Constants.MODE_FULL);
        }

        //internal void OnLogin(object sender, StringEventArgs args)
        internal void OnLogin(object sender)
        {
            Start();
        }

        #region Kite Events

        private void OnConnect()
        {
        }

        private void OnClose()
        {
        }

        private void OnError(string Message)
        {
        }

        private void OnNoReconnect()
        {
        }

        private void OnReconnect()
        {
        }

        private void OnTick(Tick tickData)
        {

        }

        private void OnOrderUpdate(Order OrderData)
        {
        }

        #endregion



        #endregion
    }
}
