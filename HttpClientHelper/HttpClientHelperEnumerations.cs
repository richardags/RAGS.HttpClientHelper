namespace RAGS.HttpClientHelper
{
    public static class ContentTypeExtensions
    {
        public static string? GetString(this ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.NONE:
                    return null;
                case ContentType.JSON:
                    return "application/json";
                case ContentType.URLEncoded:
                    return "application/x-www-form-urlencoded";
                default:
                    throw new System.Exception("WebRequestHelper.WebRequestHelperEnumerations.ContentTypeExtensions.GetString - unknown contentType: " + contentType);
            }
        }
    }

    public enum ContentType
    {
        NONE, JSON, URLEncoded
    }
}