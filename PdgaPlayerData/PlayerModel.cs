using Newtonsoft.Json;

namespace PdgaPlayerData
{
    public class PlayerModel
    {
        [JsonProperty("pdga_number")]
        public string PdgaNumber { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("rating")]
        public string Rating { get; set; }
        public string Name { get; set; }
    }
}
