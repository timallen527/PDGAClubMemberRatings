using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PdgaPlayerData
{
    public class PdgaPlayerRepository
    {
        private readonly string pdgaUsername;
        private readonly string pdgaPassword;
        private static readonly HttpClient client = new HttpClient();

        public PdgaPlayerRepository(string pdgaUsername, string pdgaPassword)
        {
            this.pdgaUsername = pdgaUsername;
            this.pdgaPassword = pdgaPassword;
        }

        public IEnumerable<PlayerModel> GetPlayersRatings(IEnumerable<PlayerModel> playerModels)
        {
            var session = GetPdgaSession().Result;

            foreach (var player in playerModels)
            {
                if (!string.IsNullOrEmpty(player.PdgaNumber) && int.TryParse(player.PdgaNumber, out _))
                {
                    player.Rating = GetPlayerRating(session, player.PdgaNumber).Result;
                }
            }

            return playerModels;
        }

        private async Task<PdgaSession> GetPdgaSession()
        {
            PdgaSession session = null;

            var credentials = new Dictionary<string, string>
            {
                { "username", pdgaUsername },
                { "password", pdgaPassword }
            };
            var content = new FormUrlEncodedContent(credentials);
            HttpResponseMessage response = await client.PostAsync("https://api.pdga.com/services/json/user/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                session = JsonConvert.DeserializeObject<PdgaSession>(responseString);
            }

            return session;
        }

        private async Task<string> GetPlayerRating(PdgaSession session, string pdgaNumber)
        {
            PlayerModel player = null;
            var uri = $"https://api.pdga.com/services/json/players?pdga_number={pdgaNumber}";
            var sessionCookieValue = $"session_name={session.SessionName}";
            var playerQueryRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            playerQueryRequest.Headers.Add("Cookie", sessionCookieValue);
            HttpResponseMessage response = await client.SendAsync(playerQueryRequest);
            
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var playerResponse = JsonConvert.DeserializeObject<PlayerResponse>(responseString);

                player = playerResponse.Players.First();
            }

            return player.Rating;
        }

        private class PdgaSession
        {
            [JsonProperty("sessid")]
            public string SessionId { get; set; }
            [JsonProperty("session_name")]
            public string SessionName { get; set; }
            [JsonProperty("token")]
            public string Token { get; set; }
        }

        private class PlayerResponse
        {
            [JsonProperty("sessid")]
            public string SessionId { get; set; }
            [JsonProperty("status")]
            public int Status{ get; set; }
            [JsonProperty("players")]
            public PlayerModel[] Players { get; set; }
        }
    }
}
