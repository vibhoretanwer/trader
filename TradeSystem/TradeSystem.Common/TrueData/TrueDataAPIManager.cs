using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TradeSystem.Common.TrueData.Models;

namespace TradeSystem.Common
{
    public class TrueDataAPIManager
    {
        #region Constants

        private const string API_ROOT = @"https://api.fyers.in/api/v1";
        private const string GET_TOKEN_URL = @"https://api.fyers.in/api/v1/genrateToken?authorization_code={0}&appId={1}";

        // Initialize key and secret of your app
        private const string apiKey = "A9E7GKQVEJ";
        private const string appSecret = "ZQKZFUKHYH";
        private const string trueDataUserName = "FYERS072";
        private const string trueDataPassword = "pHRQfJc8";

        #endregion

        #region Data Members

        private string accessToken;
        private string publicToken;

        private static TrueDataAPIManager instance;

        #endregion

        #region Properties

        public static TrueDataAPIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TrueDataAPIManager();
                    instance.Initialize();
                }
                return instance;
            }
        }

        #endregion

        #region Constructor

        private TrueDataAPIManager()
        {

        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RestClient client = new RestClient(API_ROOT);

            IRestRequest request = new RestRequest("auth", Method.POST);
            request.AddJsonBody(new AuthRequestPayload() { app_id = apiKey, secret_key = appSecret });

            var response = client.Execute<AuthResponsePayload>(request);
            if (response.IsSuccessful)
            {
                AuthResponsePayload authResponse = response.Data;
                string authCode = authResponse.authorization_code;

                // Use this URL in browser to obtain accesstoken
                string url = $"https://api.fyers.in/api/v1/genrateToken?authorization_code={authCode}&appId=A9E7GKQVEJ";

                string accessToken = @"gAAAAABeutVDwq1WjaZrxiuKkPcs-rnVHNdGiub9HRI0MNBN2iBiY3-jlPR_HnL4Q3FLWNQDpGuK9HS5BuE7B4hFCw9gn00ZlmcvOcqMAufywxqxVNrQjQQ=";

            }

        }
        #endregion
    }
}
