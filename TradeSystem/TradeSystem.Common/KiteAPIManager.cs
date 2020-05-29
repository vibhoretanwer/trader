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

            accessToken = "XbJSs5KV8Hua53735X7RyrjQPxLwkids";
            publicToken = "YbTaO7DfPezvZ9BsjAII0Ld4Ij9nqrjW";

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(publicToken))
            {
                string url = Kite.GetLoginURL();
                string requestToken = "IZIzrDF44YWzxmQETxwljF5QDCS16YgZ";
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
