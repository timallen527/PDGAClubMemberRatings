using PdgaPlayerData;

namespace PDGAClubMember
{
    public class Program
    {
        static void Main(string[] args)
        {
            var clubMembersFilePath = args[0];
            var outputCSVFileName = args[1];
            var pdgaUsername = args[2];
            var pdgaPassword = args[3];

            var clubMemberRepository = new ClubMemberRepository(clubMembersFilePath);
            var clubMembers = clubMemberRepository.GetPlayerModels();    
            
            var pdgaPlayerRepository = new PdgaPlayerRepository(pdgaUsername, pdgaPassword);
            var membersWithRatings = pdgaPlayerRepository.GetPlayersRatings(clubMembers);

            ClubMemberCSVWriter.WriteCSVFile(membersWithRatings, outputCSVFileName);
        }
    }
}
