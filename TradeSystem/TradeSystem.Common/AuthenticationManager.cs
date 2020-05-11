using KiteConnect;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSystem.Common
{
    public class AuthenticationManager
    {
        #region Data Members

        // Initialize key and secret of your app
        private string apiKey = "icgbzpgqrkyhjfq2";
        private string appSecret = "xo9ow8edaa3wgtfqu18tf2n5npnovjt6";
        private string userId = "ZW2177";

        private Kite kite;

        #endregion

        #region Constructor

        public AuthenticationManager()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public string PublicToken { get; private set; }
        public string AccessToken { get; private set; }

        #endregion

        #region Methods        

        private void Initialize()
        {
            kite = new Kite(apiKey, Debug: false);
            try
            {
                InitSession();
            }
            catch (Exception)
            {
            }
        }

        private void InitSession()
        {
            AccessToken = "7yV9NfAHx05w0n7ZhvZBfAsAdCDCR8cp";
            PublicToken = "HGnIgQII610KH8Y1Hp2I1reWWyA59Lcl";

            if (string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(PublicToken))
            {
                string url = kite.GetLoginURL();
                string requestToken = "M7mlT6dFHITJSCfhcw37UgyMxSDOfSY8";
                User user = kite.GenerateSession(requestToken, appSecret);

                AccessToken = user.AccessToken;
                PublicToken = user.PublicToken;
            }
        }

        private void OnTokenExpire()
        {
        }

        #endregion
    }
}
