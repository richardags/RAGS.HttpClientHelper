using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HttpClientHelper
{
    public class HttpClientHelperNS
    {
        public static HttpClientHelperResult Response(
            string URL,
            HttpMethod httpMethod,
            Dictionary<string, string> requestHeaders = null,
            Dictionary<string, object> keyPairContent = null,
            ContentType contentType = ContentType.NONE,
            CookieContainer cookies = null,
            Encoding responseEncoding = null,
            string stringContent = null,
            string? requestUri = null,
            X509Certificate2 certificate = null,
            SslProtocols certificateSSLProtocol = SslProtocols.None
    )
        {
            try
            {
                if (httpMethod == HttpMethod.Get && contentType != ContentType.NONE)
                {
                    throw new Exception("HttpClientHelper.cs Response() - error: Method.GET don't have 'body' => parameters/rawParameters!");
                }

                if ((keyPairContent != null || stringContent != null) && contentType == ContentType.NONE)
                {
                    throw new Exception("HttpClientHelper.cs Response() - error: parameters/rawParameters need a contentType!");
                }

                if (responseEncoding == null)
                {
                    responseEncoding = Encoding.UTF8;
                }

                var request = new HttpRequestMessage(httpMethod, requestUri);

                if (stringContent != null)
                {
                    request.Content = new StringContent(stringContent, Encoding.UTF8, contentType.GetString());
                }
                else if (contentType == ContentType.JSON)
                {
                    request.Content = new StringContent(HttpClientHelperUtils.ConvertKeyValuePairToJSONString(keyPairContent), Encoding.UTF8, contentType.GetString());
                }
                else if (contentType == ContentType.URLEncoded)
                {
                    request.Content = new StringContent(HttpClientHelperUtils.GetURLEncoded(keyPairContent), Encoding.UTF8, contentType.GetString());
                }

                if (requestHeaders != null)
                {
                    foreach (KeyValuePair<string, string> item in requestHeaders)
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }

                using HttpClientHandler handler = new();

                if (certificate != null)
                {
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.SslProtocols = certificateSSLProtocol;
                    handler.ClientCertificates.Add(certificate);
                }

                if (cookies != null)
                {
                    handler.CookieContainer = cookies;
                }


                using var client = new HttpClient(handler);
                client.BaseAddress = new Uri(URL);

                //request.PreAuthenticate = true;
                //request.KeepAlive = true;
                //request.UserAgent = "coinmaster/122350 CFNetwork/1209 Darwin/20.2.0";
                //request.Host = "vik-game.moonactive.net";
                //request.AutomaticDecompression = DecompressionMethods.GZip; 
                //request.Accept = "application/json";
                //request.Timeout = 3600;
                //request.ServicePoint.ConnectionLimit = 30;
                //request.ServicePoint.Expect100Continue = true;

                using (HttpResponseMessage httpResponseMessage = client.Send(request))
                {
                    if (httpResponseMessage == null)
                    {
                        throw new NotImplementedException();
                        //return new WebRequestHelperResult(data: data, httpStatusCode: HttpStatusCode.ExpectationFailed);
                    }

                    string data = ReadStream(httpResponseMessage, responseEncoding);

                    return new HttpClientHelperResult(data: data, httpStatusCode: httpResponseMessage.StatusCode);
                }
            }
            /*
            catch (WebException webException)
            {
                //INTERNAL ERROR SERVER +500 is not showed? maybe because ProtocolError no hadling above +500
                if (webException.Status == WebExceptionStatus.ProtocolError)
                {
                    string data = ReadStream(webException.Response.GetResponseStream(), responseEncoding);

                    HttpStatusCode httpStatusCode = ((HttpWebResponse)webException.Response).StatusCode;

                    return new WebRequestHelperResult(data: data, httpStatusCode: httpStatusCode);
                }
                else //(HttpWebResponse)webException.Response is null
                {
                    return new WebRequestHelperResult(error: webException);
                }
            }
            */
            catch (Exception exception)
            {
                return new HttpClientHelperResult(error: exception);
            }
        }

        private static string ReadStream(HttpResponseMessage response, Encoding encoding)
        {
            using (StreamReader reader = new(response.Content.ReadAsStream(), encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}