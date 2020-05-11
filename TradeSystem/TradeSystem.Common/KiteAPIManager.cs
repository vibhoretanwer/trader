using KiteConnect;
using System;

namespace TradeSystem.Common
{    
    public class KiteAPIManager
    {
        #region Data Members

        // Initialize key and secret of your app
        private string apiKey = "icgbzpgqrkyhjfq2";
        private string appSecret = "xo9ow8edaa3wgtfqu18tf2n5npnovjt6";
        private string userId = "ZW2177";
        private string accessToken;
        private string publicToken;

        private static KiteAPIManager instance;

        #endregion

        #region Properties

        public Kite Kite { get; private set; }

        public Ticker Ticker { get; private set; }

        public static KiteAPIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KiteAPIManager();
                    instance.Initialize();
                }
                return instance;
            }
        }

        #endregion

        #region Constructor

        private KiteAPIManager()
        {

        }

        #endregion

        #region Methods

        private void Initialize()
        {
            Kite = new Kite(apiKey, Debug: false);

            //accessToken = "7yV9NfAHx05w0n7ZhvZBfAsAdCDCR8cp";
            //publicToken = "HGnIgQII610KH8Y1Hp2I1reWWyA59Lcl";

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(publicToken))
            {
                string url = Kite.GetLoginURL();
                string requestToken = "M7mlT6dFHITJSCfhcw37UgyMxSDOfSY8";
                User user = Kite.GenerateSession(requestToken, appSecret);

                accessToken = user.AccessToken;
                publicToken = user.PublicToken;
            }

            Kite.SetSessionExpiryHook(OnTokenExpire);
            Kite.SetAccessToken(accessToken);
            Ticker = new Ticker(apiKey, accessToken);
        }

        private void OnTokenExpire()
        {
        }

        #endregion
    }
}
