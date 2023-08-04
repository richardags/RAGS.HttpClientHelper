using System;
using System.Net;
using System.Net.Http.Headers;

namespace RAGS.HttpClientHelper
{
    public class HttpClientHelperResult
    {
        public string data;
        public HttpStatusCode httpStatusCode;
        public Exception error;
        public HttpResponseHeaders headers;

        public HttpClientHelperResult(string data, HttpStatusCode httpStatusCode, HttpResponseHeaders headers)
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