using Newtonsoft.Json;

namespace TVA.MODEL.SocialNetwork
{
    public class UserSocialNetwork
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

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
