using HttpClientHelper;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            //GET
            HttpClientHelperResult result = HttpClientHelperNS.Response("https://google.com/", HttpMethod.Get);

            if(result.error == null)
            {
                Console.WriteLine(result.httpStatusCode);
                Console.WriteLine(result.data);
            }
            else
            {
                Console.WriteLine(result.error);
            }

            //POST
            HttpClientHelperResult result2 = HttpClientHelperNS.Response("https://google.com/", HttpMethod.Post, stringContent: "data");

            if (result2.error == null)
            {
                Console.WriteLine(result2.httpStatusCode);
                Console.WriteLine(result2.data);
            }
            else
            {
                Console.WriteLine(result2.error);
            }

            //POST 2
            Dictionary<string, object> keyPairContent = new();
            keyPairContent.Add("id", 13);
            keyPairContent.Add("category", "movie");

            HttpClientHelperResult result3 = HttpClientHelperNS.Response("https://google.com/", HttpMethod.Post, contentType: ContentType.URLEncoded, keyPairContent: keyPairContent);

            if (result3.error == null)
            {
                Console.WriteLine(result3.httpStatusCode);
                Console.WriteLine(result3.data);
            }
            else
            {
                Console.WriteLine(result3.error);
            }

            //POST 2
            Dictionary<string, object> keyPairContent2 = new();
            keyPairContent2.Add("token", "123456789");

            HttpClientHelperResult result4 = HttpClientHelperNS.Response("https://google.com/", HttpMethod.Post, contentType: ContentType.URLEncoded, keyPairContent: keyPairContent2);

            if (result4.error == null)
            {
                Console.WriteLine(result4.httpStatusCode);
                Console.WriteLine(result4.data);
            }
            else
            {
                Console.WriteLine(result4.error);
            }


            Console.ReadKey();
        }
    }
}