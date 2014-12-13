using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace LinkedInRecommendation
{
    // This class takes care of talking to LinkedIn: getting accesstoken, verify identity with code, and api protocol
    // for getting user and connection data
    class OAuthUtilHelper
    {
        // API keys and tokens should be written in a separate file, since we are time constraints,
        // we are leaving them here for now.
        private string _consumerKey = "ENTER YOUR OWN";
        private string _consumerSecretKey = "ENTER YOUR OWN";
        //private string _accessToken = "ENTER YOUR OWN";
        //private string _accessTokenSecretKey = "ENTER YOUR OWN";

        // Links used to verify consumer key and code
        string _linkedInRequestTokenUrl = "https://api.linkedin.com/uas/oauth/requestToken";
        string _linkedInAccessTokenUrl = "https://api.linkedin.com/uas/oauth/accessToken";

        // Links used to get information back from LinkedIn
        string _requestPeopleUrl = "http://api.linkedin.com/v1/people/~";
        string _requestConnectionsUrl = "http://api.linkedin.com/v1/people/~/connections";

        // Declaring Global Variables
        private OAuthUtil oAuthUtil;
        private string _oauth_token;
        private string _oauth_token_secret;
        private string _oAuthAuthorizeLink;
        private string _verificationCode;
        private string _accessToken;
        private string _accessTokenSecretKey;
        private string _linkedinData;

        /// <summary>
        /// Constructor
        /// </summary>
        public OAuthUtilHelper() 
        {
            // Initializing OAuthUtil
            oAuthUtil = new OAuthUtil();
        }

        /// <summary>
        /// Getter of _oAuthAuthorizeLink
        /// </summary>
        /// <returns>string</returns>
        public string getAuthorizeLink()
        {
            return this._oAuthAuthorizeLink;
        }

        /// <summary>
        /// Setter of _verificationCode
        /// </summary>
        /// <param name="verificationCode"></param>
        public void setVerificationCode(string verificationCode)
        {
            this._verificationCode = verificationCode;
        }

        public string getLinkedInData()
        {
            return this._linkedinData;
        }

        /// <summary>
        /// Get request token from consumer key
        /// </summary>
        /// <returns></returns>
        public async Task getRequestToken()
        {
            string nonce = oAuthUtil.GetNonce();
            string timeStamp = oAuthUtil.GetTimeStamp();

            string sigBaseStringParams = "oauth_consumer_key=" + _consumerKey;

            sigBaseStringParams += "&" + "oauth_nonce=" + nonce;
            sigBaseStringParams += "&" + "oauth_signature_method=" + "HMAC-SHA1";
            sigBaseStringParams += "&" + "oauth_timestamp=" + timeStamp;
            sigBaseStringParams += "&" + "oauth_version=1.0";
            string sigBaseString = "POST&";
            sigBaseString += Uri.EscapeDataString(_linkedInRequestTokenUrl) + "&" + Uri.EscapeDataString(sigBaseStringParams);

            string signature = oAuthUtil.GetSignature(sigBaseString, _consumerSecretKey);

            var responseText = await oAuthUtil.PostData(_linkedInRequestTokenUrl, sigBaseStringParams + "&oauth_signature=" + Uri.EscapeDataString(signature));

            if (!string.IsNullOrEmpty(responseText))
            {
                string oauth_token = null;
                string oauth_token_secret = null;
                string oauth_authorize_url = null;
                string[] keyValPairs = responseText.Split('&');

                for (int i = 0; i < keyValPairs.Length; i++)
                {
                    String[] splits = keyValPairs[i].Split('=');
                    switch (splits[0])
                    {
                        case "oauth_token":
                            oauth_token = splits[1];
                            break;
                        case "oauth_token_secret":
                            oauth_token_secret = splits[1];
                            break;
                        case "xoauth_request_auth_url":
                            oauth_authorize_url = splits[1];
                            break;
                    }
                }

                _oauth_token = oauth_token;
                _oauth_token_secret = oauth_token_secret;
                _oAuthAuthorizeLink = Uri.UnescapeDataString(oauth_authorize_url + "?oauth_token=" + oauth_token);
            }
        }

        /// <summary>
        /// Get access token from verification code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async Task getAccessToken()
        {
            string nonce = oAuthUtil.GetNonce();
            string timeStamp = oAuthUtil.GetTimeStamp();

            string sigBaseStringParams = "oauth_consumer_key=" + _consumerKey;
            sigBaseStringParams += "&" + "oauth_nonce=" + nonce;
            sigBaseStringParams += "&" + "oauth_signature_method=" + "HMAC-SHA1";
            sigBaseStringParams += "&" + "oauth_timestamp=" + timeStamp;
            sigBaseStringParams += "&" + "oauth_token=" + _oauth_token;
            sigBaseStringParams += "&" + "oauth_verifier=" + _verificationCode;
            sigBaseStringParams += "&" + "oauth_version=1.0";
            string sigBaseString = "POST&";
            sigBaseString += Uri.EscapeDataString(_linkedInAccessTokenUrl) + "&" + Uri.EscapeDataString(sigBaseStringParams);

            // LinkedIn requires both consumer secret and request token secret
            string signature = oAuthUtil.GetSignature(sigBaseString, _consumerSecretKey, _oauth_token_secret);

            var responseText = await oAuthUtil.PostData(_linkedInAccessTokenUrl, sigBaseStringParams + "&oauth_signature=" + Uri.EscapeDataString(signature));

            if (!string.IsNullOrEmpty(responseText))
            {
                string oauth_token = null;
                string oauth_token_secret = null;
                string[] keyValPairs = responseText.Split('&');

                for (int i = 0; i < keyValPairs.Length; i++)
                {
                    String[] splits = keyValPairs[i].Split('=');
                    switch (splits[0])
                    {
                        case "oauth_token":
                            oauth_token = splits[1];
                            break;
                        case "oauth_token_secret":
                            oauth_token_secret = splits[1];
                            break;
                    }
                }

                _accessToken = oauth_token;
                _accessTokenSecretKey = oauth_token_secret;
            }
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <returns></returns>
        public async Task requestUserProfile()
        {
            await requestLinkedInApi(_requestPeopleUrl);
        }

        /// <summary>
        /// Get user connection
        /// </summary>
        /// <returns></returns>
        public async Task requestUserConnection()
        {
            await requestLinkedInApi(_requestConnectionsUrl);
        }

        /// <summary>
        /// Request LinkedIn api from authenticated verification code
        /// </summary>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        private async Task requestLinkedInApi(string url)
        {
            string nonce = oAuthUtil.GetNonce();
            string timeStamp = oAuthUtil.GetTimeStamp();

            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.MaxResponseContentBufferSize = int.MaxValue;
                httpClient.DefaultRequestHeaders.ExpectContinue = false;
                HttpRequestMessage requestMsg = new HttpRequestMessage();
                requestMsg.Method = new HttpMethod("GET");
                requestMsg.RequestUri = new Uri(url, UriKind.Absolute);

                string sigBaseStringParams = "oauth_consumer_key=" + _consumerKey;
                sigBaseStringParams += "&" + "oauth_nonce=" + nonce;
                sigBaseStringParams += "&" + "oauth_signature_method=" + "HMAC-SHA1";
                sigBaseStringParams += "&" + "oauth_timestamp=" + timeStamp;
                sigBaseStringParams += "&" + "oauth_token=" + _accessToken;
                sigBaseStringParams += "&" + "oauth_verifier=" + _verificationCode;
                sigBaseStringParams += "&" + "oauth_version=1.0";
                string sigBaseString = "GET&";
                sigBaseString += Uri.EscapeDataString(url) + "&" + Uri.EscapeDataString(sigBaseStringParams);

                // LinkedIn requires both consumer secret and request token secret
                string signature = oAuthUtil.GetSignature(sigBaseString, _consumerSecretKey, _accessTokenSecretKey);

                string data = "realm=\"http://api.linkedin.com/\", oauth_consumer_key=\"" + _consumerKey
                              +
                              "\", oauth_token=\"" + _accessToken +
                              "\", oauth_verifier=\"" + _verificationCode +
                              "\", oauth_nonce=\"" + nonce +
                              "\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"" + timeStamp +
                              "\", oauth_version=\"1.0\", oauth_signature=\"" + Uri.EscapeDataString(signature) + "\"";
                requestMsg.Headers.Authorization = new AuthenticationHeaderValue("OAuth", data);
                var response = await httpClient.SendAsync(requestMsg);
                var text = await response.Content.ReadAsStringAsync();
                _linkedinData = text;
            }
            catch (Exception Err)
            {
                throw;
            }
        }



    }
}
