using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;

namespace GuildRosterAnalyzer.Web.Models.API
{
    public class SwgohGgApi
    {
        const string BaseUrl = "https://swgoh.gg/api/";

        //readonly string _accountSid;
        //readonly string _secretKey;

        public SwgohGgApi(/*string accountSid, string secretKey*/)
        {
            //_accountSid = accountSid;
            //_secretKey = secretKey;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            //client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            //request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var swgohGgException = new ApplicationException(message, response.ErrorException);
                throw swgohGgException;
            }
            return response.Data;
        }
    }
}