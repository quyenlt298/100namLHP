using System.Collections.Generic;

namespace TVA.COMMON.Http.Type
{
    public class HeaderValue
    {
        public string AuthorizationType { set; get; }

        public string AuthorizationValue { set; get; }

        public string SecretUser { set; get; }

        public string SecretPass { set; get; }

        public Dictionary<string, string> ListHeader { set; get; }
    }
}
