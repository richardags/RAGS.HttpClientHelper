using System;
using System.Net;

namespace HttpClientHelper
{
    public class HttpClientHelperResult
    {
        public string data;
        public HttpStatusCode httpStatusCode;
        public Exception error;

        public HttpClientHelperResult(string data, HttpStatusCode httpStatusCode)
        {
            this.data = data;
            this.httpStatusCode = httpStatusCode;
        }
        public HttpClientHelperResult(Exception error)
        {
            this.error = error;
        }
    }
}