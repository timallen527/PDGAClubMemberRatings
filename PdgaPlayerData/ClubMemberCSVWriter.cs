using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdgaPlayerData
{
    public static class ClubMemberCSVWriter
    {
        public static void WriteCSVFile(IEnumerable<PlayerModel> players, string outputFileName)
        {
            var fileContent = new StringBuilder();
            var headerString = $"Name, PDGA Number, Rating";
            fileContent.AppendLine(headerString);

            foreach (var player in players)
            {
                var playerString = $"{player.Name}, {player.PdgaNumber}, {player.Rating}";
                fileContent.AppendLine(playerString);
            }

            File.WriteAllText(outputFileName, fileContent.ToString());
        }
    }
}
