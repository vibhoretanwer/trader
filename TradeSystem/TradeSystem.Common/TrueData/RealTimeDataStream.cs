using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace TradeSystem.Common.TrueData
{
    public class RealTimeDataStream
    {
        private WebSocket realTimeWebSocket;
        
        private string trueDataUserName;
        private string trueDataPassword;
        
        public RealTimeDataStream(string trueDataUserName, string trueDataPassword)
        {
            this.trueDataUserName = trueDataUserName;
            this.trueDataPassword = trueDataPassword;
        }

        private void ConnectRealTimeDataSocket()
        {
            realTimeWebSocket = new WebSocket($"wss://push.truedata.in:8084?user={trueDataUserName}&password={trueDataPassword}");

            realTimeWebSocket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            realTimeWebSocket.OnOpen += (sender, args) => { };
            realTimeWebSocket.OnError += (sender, args) => { };
            realTimeWebSocket.OnClose += (sender, args) => { };
            realTimeWebSocket.OnMessage += (sender, args) =>
            {

            };

            try
            {
                realTimeWebSocket.Connect();
            }
            catch (Exception ex)
            {

            }
            //realTimeWebSocket.Send("{\"method\":\"gethistory\",\"interval\":\"EOD\",\"Symbol\":\"NIFTY-I\",\"from\":\"2008-03-12T00:00\",\"to\":\"2009-05-10T00:00\"}");            
        }
    }
}
