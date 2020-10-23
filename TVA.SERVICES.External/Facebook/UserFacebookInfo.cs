using Newtonsoft.Json;

namespace TVA.SERVICES.External.Facebook
{
    public class UserFacebookInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("picture")]
        public picture Picture { get; set; }
    }

    public class picture
    {
        public data data { get; set; }
    }

    public class data
    {
        public string url { get; set; }
    }
}