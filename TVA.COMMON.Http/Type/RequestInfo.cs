using System.Collections.Generic;

namespace TVA.COMMON.Http.Type
{
    public class RequestInfo
    {
        public string UrlBase { set; get; }

        public HeaderValue HeaderValue { set; get; }

        public List<KeyValuePair<string, string>> FormValue { set; get; }
    }
}
