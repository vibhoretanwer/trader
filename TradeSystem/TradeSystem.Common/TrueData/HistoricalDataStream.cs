using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TradeSystem.Common.TrueData.Models;
using WebSocketSharp;

namespace TradeSystem.Common.TrueData
{
    public class HistoricalDataStream
    {
        private WebSocket historicalWebSocket;

        private string trueDataUserName;
        private string trueDataPassword;

        Dictionary<Interval, string> intervals = new Dictionary<Interval, string>()
        {
            { Interval.Tick, "tick"},
            { Interval.Minute, "1min"},
            { Interval.FifteenMinute, "15min"},
            { Interval.ThirtyMinute, "30min"},
            { Interval.EOD, "EOD"},
        };

        public HistoricalDataStream(string trueDataUserName, string trueDataPassword)
        {
            this.trueDataUserName = trueDataUserName;
            this.trueDataPassword = trueDataPassword;
        }

        ulong totalVolume;
        ulong averageVolume;
        ulong index;

        private void ConnectHistoricalDataSocket()
        {
            totalVolume = 0;
            averageVolume = 0;
            index = 1;
            if (historicalWebSocket == null)
            {
                historicalWebSocket = new WebSocket($"wss://push.truedata.in:8094?user={trueDataUserName}&password={trueDataPassword}");
                historicalWebSocket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                historicalWebSocket.OnOpen += (sender, args) => { };
                historicalWebSocket.OnError += (sender, args) => { };
                historicalWebSocket.OnClose += (sender, args) => { };                

                try
                {
                    historicalWebSocket.Connect();

                    historicalWebSocket.OnMessage += (sender, args) =>
                    {
                        HistoricalDataStreamContent content = JsonConvert.DeserializeObject<HistoricalDataStreamContent>(args.Data);

                        content.data.ForEach(item =>
                        {
                            ulong volume = ulong.Parse(item[5]);
                            totalVolume += volume;
                            averageVolume = totalVolume / index;
                            index++;

                            Console.Write(content.Symbol + " - ");
                            item.ForEach(i => Console.Write(i + ", "));
                            Console.Write(averageVolume);
                            Console.WriteLine();
                        });                        
                    };
                }
                catch (Exception ex)
                {

                }
            }
            //historicalWebSocket.Send("{\"method\":\"gethistory\",\"interval\":\"EOD\",\"Symbol\":\"NIFTY-I\",\"from\":\"2008-03-12T00:00\",\"to\":\"2009-05-10T00:00\"}");
        }

        public void Subscribe(string symbol, Interval interval, DateTime from, DateTime to)
        {
            ConnectHistoricalDataSocket();
            HistoricalDataSubscriptionModel model = new HistoricalDataSubscriptionModel()
            {
                Symbol = symbol,
                Interval = intervals[interval],
                From = from,
                To = to,
            };
            historicalWebSocket.Send(JsonConvert.SerializeObject(model));
        }
    }
}
