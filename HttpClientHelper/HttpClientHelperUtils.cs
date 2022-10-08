using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpClientHelper
{
    public class HttpClientHelperUtils
    {
        public static string GetURLEncoded(Dictionary<string, object> parameters)
        {
            return string.Format("{0}", parameters.Count == 0 ? "" :
                string.Join("&", parameters.Select(
                        e => string.Format("{0}={1}",
                        HttpUtility.HtmlEncode(e.Key),
                        HttpUtility.HtmlEncode(e.Value.ToString())))
                        ));
        }
        public static string ConvertKeyValuePairToJSONString(Dictionary<string, object> parameters)
        {
            JObject json = new JObject();

            foreach (KeyValuePair<string, object> item in parameters)
            {
                if (item.Value.GetType() == typeof(JObject))
                {
                    json.Add(item.Key, (JObject)item.Value);
                }
                if (item.Value.GetType() == typeof(JArray))
                {
                    json.Add(item.Key, (JArray)item.Value);
                }
                else if (item.Value.GetType() == typeof(string))
                {
                    json.Add(item.Key, (string)item.Value);
                }
                else if (item.Value.GetType() == typeof(int))
                {
                    json.Add(item.Key, (int)item.Value);
                }
                else
                {
                    throw new Exception();
                }
            }

            return json.ToString();
        }
    }
}