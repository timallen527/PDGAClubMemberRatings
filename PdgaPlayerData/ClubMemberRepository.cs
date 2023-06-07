using System.Collections.Generic;
using System.IO;

namespace PdgaPlayerData
{
    public class ClubMemberRepository
    {
        private const int PlayerNameColumnIndex = 1;
        private const int PlayerPDGAColumnIndex = 3;
        private readonly string filepath;

        public ClubMemberRepository(string filepath)
        {
            this.filepath = filepath;
        }

        public IEnumerable<PlayerModel> GetPlayerModels()
        {
            var playerModels = new List<PlayerModel>();

            using (var reader = new StreamReader(filepath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var playerAttributes = line.Split(',');

                    playerModels.Add(new PlayerModel() 
                    { 
                        Name = playerAttributes[PlayerNameColumnIndex], 
                        PdgaNumber = playerAttributes[PlayerPDGAColumnIndex] 
                    });
                }
            }

            return playerModels;
        }
    }
}
