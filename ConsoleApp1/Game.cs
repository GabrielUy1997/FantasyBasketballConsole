using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//do player stat page

namespace ConsoleApp1
{
    class Game
    {
        private string[] PlayerID;
        protected string[] PlayerName;
        private string[] PlayerPos;
        private string[] PlayerAge;
        private string[] PlayerTeam;
        private string[] PlayerGames;
        private string[] PlayerGamesStart;
        private string[] PlayerFieldGoal;
        private string[] PlayerFieldGoalAttempt;
        private string[] Player3Point;
        private string[] Player2Point;
        private string[] PlayerFreeThow;
        private string[] PlayerFreeThrowAttempt;
        private string[] PlayerOffensReb;
        private string[] PlayerDefenceReb;
        private string[] PlayerAssist;
        private string[] PlayerSteal;
        private string[] PlayerBlock;
        private string[] PlayerTurnover;
        private string[] PlayerPersFoul;
        private string[] PlayerPoints;
        public List<string> winners;
        public List<LeaugeTeam> Teams;
        public List<int> drafted;
        public List<int> Week;
        public List<int> MatchupType1;
        public List<int> MatchupType2;
        public List<int> MatchupType3;
        public List<int> MatchupType4;
        public int matchup;
        public int WinnerMatch1;
        public int WinnerMatch2;
        public decimal PlayerGamesPlayed;
        public decimal Totalgames;
        public decimal ChanceMissing;
        private double PlayerGameScore;
        public static Random RandomGen;
        List<int> TopTenFree;

        public Game()
        {
            drafted = new List<int>(1);
            winners = new List<string>(19);
            Teams = new List<LeaugeTeam>(4);
            Week = new List<int>(19);
            TopTenFree = new List<int>(10);
            matchup = 0;
            Week.Add(1);
            Week.Add(2);
            Week.Add(3);
            Week.Add(4);
            Week.Add(5);
            Week.Add(6);
            Week.Add(7);
            Week.Add(8);
            Week.Add(9);
            Week.Add(10);
            Week.Add(11);
            Week.Add(12);
            Week.Add(13);
            Week.Add(14);
            Week.Add(15);
            Week.Add(16);
            Week.Add(17);
            Week.Add(18);
            Week.Add(19);
            MatchupType1 = new List<int> { 0, 1, 2, 3 };
            MatchupType2 = new List<int> { 0, 2, 1, 3 };
            MatchupType3 = new List<int> { 0, 3, 2, 1 };
            MatchupType4 = new List<int> { 0, 2, 3, 1 };
            Totalgames = 82;
        }

        public void PrintPlayer(int pID)
        {
            Console.WriteLine("{0}: {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19} {20}",
                        GetPlayerName(pID), GetPlayerPos(pID), GetPlayerAge(pID), GetPlayerTeam(pID), GetPlayerGamesPlayed(pID), GetPlayerGamesStart(pID), GetPlayerFieldGoal(pID), GetPlayerFieldGoalAtt(pID),
                        GetPlayer3Point(pID), GetPlayer2Point(pID), GetPlayerFt(pID), GetPlayerFtAtt(pID), GetPlayerOffensiveReb(pID), GetPlayerDefenceReb(pID), GetPlayerAssist(pID),
                        GetPlayerSteal(pID), GetPlayerBlock(pID), GetPlayerTurnover(pID), GetPlayerPersFoul(pID), GetPlayerPoints(pID), pID);
        }

        public void AddTakenPlayer(int a_player)
        {
            drafted.Add(a_player);
        }

        public bool CheckIfPlayerTaken(int a_player)
        {
            foreach (int b_player in drafted)
            {
                if (b_player == a_player)
                {
                    return true;
                }
            }
            return false;
        }

        public void ComputerTeamDraft(LeaugeTeam a_team, String a_name)
        {
            int BestPick = 0;
            int MostPoints = 0;
            int index = 0;
            int totalPoints;
            foreach (string player in PlayerPoints.Skip(1))
            {
                index++;
                totalPoints = Int32.Parse(player);
                if (totalPoints > MostPoints && CheckIfPlayerTaken(index) == false && a_team.CanDraftPosition(PlayerPos,index) == true)
                {
                    MostPoints = totalPoints;
                    BestPick = index;
                }
            }
            String[] aPick = PlayerName[BestPick].Split('\\');
            PlayerName[BestPick] = aPick[0];
            Console.WriteLine("{0} Drafted: {1}", a_name, PlayerName[BestPick]);
            if(PlayerPos[BestPick].Contains('G'))
            {
                a_team.AddGuard();
            }
            else if (PlayerPos[BestPick].Contains('F'))
            {
                a_team.AddForward();
            }
            else if (PlayerPos[BestPick].Contains('C'))
            {
                a_team.AddCenters();
            }
            AddTakenPlayer(BestPick);
            a_team.AddingPlayer(BestPick);
        }

        public void HumanTeamDraft(LeaugeTeam a_player)
        {
            bool validInput = false;
            do
            {
                Console.Write("Enter players name:");
                string input = Console.ReadLine();
                string value1 = Array.Find(PlayerName,
                 element => element.StartsWith(input, StringComparison.Ordinal));
                int pID = Array.FindIndex(PlayerName, item => item == value1);
                try
                {
                    if (pID == -1)
                    {
                        throw new Exception();
                    }
                    try
                    {
                        if (CheckIfPlayerTaken(pID) == true)
                        {
                            throw new ArgumentException();
                        }
                        try
                        {
                            if(a_player.CanDraftPosition(PlayerPos, pID) == false)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                PlayerName[pID] = input;
                                AddTakenPlayer(pID);
                                a_player.AddingPlayer(pID);
                                validInput = true;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Too many players of the same position on the team");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("That Player has already been drafted");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Player entry");
                }
            } while (validInput == false);
        }
        public void Drafting()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (LeaugeTeam team in Teams)
                {
                    //HumanTeamDraft(player1);
                    //ComputerTeamDraft()
                    //figure out the snake draft here
                    if (team.CanDraft() == true)
                    {
                        HumanTeamDraft(team);
                    }
                    else
                    {
                        ComputerTeamDraft(team, team.GetName());
                    }
                }
                Teams.Reverse();
            }
            Teams.Reverse();
        }

        public void RandomizingDraftOrder()
        {
            Random rng = new Random();
            int n = Teams.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                LeaugeTeam value = Teams[k];
                Teams[k] = Teams[n];
                Teams[n] = value;
            }
        }

        public void ShowSchedule()
        {
            //https://stackoverflow.com/questions/16572362/rearrange-a-list-based-on-given-order-in-c-sharp 5/20/20
            foreach (int week in Week)
            {
                Console.WriteLine("{0}.", week);
                if (matchup == 0)
                {
                    matchup = 1;
                }
                else if (matchup == 1)
                {
                    Teams = MatchupType2.Select(i => Teams[i]).ToList();
                    matchup = 2;
                }
                else if (matchup == 2)
                {
                    Teams = MatchupType3.Select(i => Teams[i]).ToList();
                }
                Console.WriteLine("{0} vs. {1}, {2} vs. {3}", Teams[0].GetName(), Teams[1].GetName(), Teams[2].GetName(), Teams[3].GetName());
                if (week <= winners.Count)
                {
                    Console.WriteLine("{0}", winners[week - 1]);
                }
                Console.WriteLine("");
                if(matchup == 2)
                {
                    Teams = MatchupType4.Select(i => Teams[i]).ToList();
                    matchup = 0;
                }
            }
        }

        public void SetUpWeekMatchup(LeaugeTeam a_player, LeaugeTeam a_CPU1, LeaugeTeam a_CPU2, LeaugeTeam a_CPU3)
        {
            matchup = 0;
            int indexX = 0;
            RandomGen = new Random();
            foreach (int week in Week)
            {
                Console.WriteLine("week {0}", week);
                if (matchup == 0)
                {
                    matchup = 1;
                }
                else if (matchup == 1)
                {
                    Teams = MatchupType2.Select(i => Teams[i]).ToList();
                    matchup = 2;
                }
                else if (matchup == 2)
                {
                    Teams = MatchupType3.Select(i => Teams[i]).ToList();
                    Teams = MatchupType4.Select(i => Teams[i]).ToList();
                    matchup = 0;
                }
                foreach (LeaugeTeam teams in Teams)
                {
                    //index = 0;
                    for (int indexY = 0; indexY < 7; indexY++)
                    {
                        indexX = 0;
                        //https://stackoverflow.com/questions/37858551/implement-percent-chance-in-c-sharp 5/25/20
                        foreach (int player in teams.team)
                        {
                            PlayerGamesPlayed = Int32.Parse(GetPlayerGamesPlayed(player));
                            ChanceMissing = (Math.Round((PlayerGamesPlayed / Totalgames) * 100, MidpointRounding.AwayFromZero));
                            int RandomVal = RandomGen.Next(100);
                            //chance of missing a game
                            if (RandomVal < ChanceMissing)
                            {
                                RandomVal = RandomGen.Next(100);
                                //average game
                                if (15 < RandomVal && RandomVal < 85)
                                {
                                    RandomVal = RandomGen.Next(-10, 10);
                                    PlayerGameScore += RandomVal;
                                    RandomVal = RandomGen.Next(-2, 3);
                                    PlayerGameScore += Math.Round(Double.Parse(GetPlayerPoints(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round(Double.Parse(GetPlayerAssist(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round(Double.Parse(GetPlayerBlock(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round(Double.Parse(GetPlayerSteal(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round(Double.Parse(GetPlayerDefenceReb(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round(Double.Parse(GetPlayerOffensiveReb(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round(Double.Parse(GetPlayerFt(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore -= Math.Round((Double.Parse(GetPlayerFieldGoalAtt(player)) - Double.Parse(GetPlayerFieldGoal(player))) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore -= Math.Round((Double.Parse(GetPlayerFtAtt(player)) - Double.Parse(GetPlayerFt(player))) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    PlayerGameScore -= Math.Round(Double.Parse(GetPlayerTurnover(player)) / Double.Parse(GetPlayerGamesPlayed(player)), MidpointRounding.ToEven);
                                    //foul out
                                    if (Math.Round((Double.Parse(GetPlayerPersFoul(player)) / Double.Parse(GetPlayerGamesPlayed(player))) + RandomVal, MidpointRounding.ToEven) >= 5)
                                    {
                                        PlayerGameScore -= 4;
                                    }
                                }
                                //below average
                                else if (RandomVal < 15)
                                {
                                    RandomVal = RandomGen.Next(-10, 5);
                                    PlayerGameScore += RandomVal;
                                    RandomVal = RandomGen.Next(-2, 3);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerPoints(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerAssist(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerBlock(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerSteal(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerDefenceReb(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerOffensiveReb(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerFt(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore -= Math.Round(((Double.Parse(GetPlayerFieldGoalAtt(player)) - Double.Parse(GetPlayerFieldGoal(player))) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.15), MidpointRounding.ToEven);
                                    PlayerGameScore -= Math.Round((Double.Parse(GetPlayerTurnover(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.15), MidpointRounding.ToEven);
                                    if (Math.Round((Double.Parse(GetPlayerPersFoul(player)) / Double.Parse(GetPlayerGamesPlayed(player))) + RandomVal, MidpointRounding.ToEven) >= 5)
                                    {
                                        PlayerGameScore -= 4;
                                    }
                                }
                                //above average
                                else if (RandomVal >= 85)
                                {
                                    RandomVal = RandomGen.Next(-5, 15);
                                    PlayerGameScore += RandomVal;
                                    RandomVal = RandomGen.Next(-2, 3);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerPoints(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerAssist(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerBlock(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerSteal(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerDefenceReb(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerOffensiveReb(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.75), MidpointRounding.ToEven);
                                    PlayerGameScore += Math.Round((Double.Parse(GetPlayerFt(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 1.75), MidpointRounding.ToEven);
                                    PlayerGameScore -= Math.Round(((Double.Parse(GetPlayerFieldGoalAtt(player)) - Double.Parse(GetPlayerFieldGoal(player))) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    PlayerGameScore -= Math.Round((Double.Parse(GetPlayerTurnover(player)) / Double.Parse(GetPlayerGamesPlayed(player)) * 0.75), MidpointRounding.ToEven);
                                    if (Math.Round((Double.Parse(GetPlayerPersFoul(player)) / Double.Parse(GetPlayerGamesPlayed(player))) + RandomVal, MidpointRounding.ToEven) >= 5)
                                    {
                                        PlayerGameScore -= 4;
                                    }
                                }

                            }
                            //missing game
                            else
                            {
                                PlayerGameScore += 0;
                            }
                            //Console.WriteLine("{0}: {1}", PlayerName[player], PlayerGameScore);
                            teams.PlayersScores[indexX][indexY] = PlayerGameScore;
                            teams.WeekScore += PlayerGameScore;
                            PlayerGameScore = 0;
                            indexX++;
                        }
                    }
                    
                }
                WinnerMatch1 = (Teams.ElementAt(0).WeekScore < Teams.ElementAt(1).WeekScore) ? 1 : 0;
                WinnerMatch2 = (Teams.ElementAt(2).WeekScore < Teams.ElementAt(3).WeekScore) ? 3 : 2;
                winners.Add(Teams.ElementAt(WinnerMatch1).GetName() + " " + Teams.ElementAt(WinnerMatch2).GetName());
                foreach(LeaugeTeam team in Teams)
                {
                    if(WinnerMatch1 == Teams.IndexOf(team) || WinnerMatch2 == Teams.IndexOf(team))
                    {
                        team.Wins++;
                    }
                    else
                    {
                        team.Losses++;
                    }
                }
                Console.WriteLine("{0}: {1} vs. {2}: {3}", Teams.ElementAt(0).GetName(), Teams.ElementAt(0).WeekScore, Teams.ElementAt(1).GetName(), Teams.ElementAt(1).WeekScore);
                Console.WriteLine("{0}: {1} vs. {2}: {3}", Teams.ElementAt(2).GetName(), Teams.ElementAt(2).WeekScore, Teams.ElementAt(3).GetName(), Teams.ElementAt(3).WeekScore);
                Console.WriteLine("{0} wins with {1} points", Teams.ElementAt(WinnerMatch1).GetName(), Teams.ElementAt(WinnerMatch1).WeekScore);
                Console.WriteLine("{0} wins with {1} points", Teams.ElementAt(WinnerMatch2).GetName(), Teams.ElementAt(WinnerMatch2).WeekScore);
                int advance;
                advance = a_player.Menu(PlayerName, Teams);
                while (advance == 4)
                {
                    ShowSchedule();
                    advance = a_player.Menu(PlayerName, Teams);
                }
                while(advance == 5)
                {
                    ShowFreeAgents(a_player);
                    advance = a_player.Menu(PlayerName, Teams);
                }
                while(advance == 6)
                {
                    AskForTrade(a_player,a_CPU1,a_CPU2,a_CPU3);
                    advance = a_player.Menu(PlayerName, Teams);
                }
                foreach (LeaugeTeam teams in Teams)
                {
                    teams.WeekScore = 0;
                    for (int indexY = 0; indexY < 7; indexY++)
                    {
                        indexX = 0;
                        foreach (int player in teams.team)
                        {
                            teams.PlayersScores[indexX][indexY] = 0;
                            indexX++;
                        }
                    }
                }
                
            }
        }

        public void ShowFreeAgents(LeaugeTeam a_player)
        {
            int BestPick = 0;
            int MostPoints = 0;
            int index = 0;
            int totalPoints;
           
            for(int i = 0; i < 10; i++)
            {
                foreach (string player in PlayerPoints.Skip(1))
                {
                    index++;
                    totalPoints = Int32.Parse(player);
                    String[] PlayerNames = PlayerName[index].Split('\\');
                    if (totalPoints > MostPoints && CheckIfPlayerTaken(index) != true && TopTenFree.Contains(index) != true)
                    {
                        MostPoints = totalPoints;
                        BestPick = index;
                    }
                }
                String[] FreeAgent = PlayerName[BestPick].Split('\\');
                PlayerName[BestPick] = FreeAgent[0];
                TopTenFree.Add(BestPick);
                BestPick = 0;
                MostPoints = 0;
                index = 0;
                totalPoints = 0;
            }
            foreach(int player in TopTenFree)
            {
                Console.Write("{0}.", index);
                PrintPlayer(player);
                index++;
            }
            Console.WriteLine("Would you like to add/drop a player? (y/n)");
            string input = Console.ReadLine();
            if(input == "y")
            {
                AddDrop(a_player);
            }
            TopTenFree.Clear();
        }

        public void AddDrop(LeaugeTeam a_player)
        {
            int playerAdding;
            int playerDropping;
            do
            {
                Console.WriteLine("Who would you like to add?");
                string input = Console.ReadLine();
                playerAdding = Int32.Parse(input);
                a_player.ShowTeam(PlayerName);
                Console.WriteLine("Who would you like to drop?");
                input = Console.ReadLine();
                playerDropping = Int32.Parse(input);
                if(PlayerPos[TopTenFree[playerAdding]] != PlayerPos[TopTenFree[playerDropping]] && a_player.CanDraftPosition(PlayerPos, TopTenFree[playerAdding]))
                {
                    Console.WriteLine("Too many players of the same position on the team");
                }
            } while (PlayerPos[TopTenFree[playerAdding]] != PlayerPos[TopTenFree[playerDropping]] && a_player.CanDraftPosition(PlayerPos, TopTenFree[playerAdding]) == false);
            Console.WriteLine("{0} for {1}", PlayerName[TopTenFree[playerAdding]], PlayerName[a_player.team[playerDropping]]);
            a_player.AddingPlayer(TopTenFree[playerAdding]);
            AddTakenPlayer(TopTenFree[playerAdding]);
            drafted.RemoveAt(drafted.IndexOf(a_player.team[playerDropping]));
            a_player.DroppingPlayer(playerDropping);

        }

        public void AskForTrade(LeaugeTeam a_player, LeaugeTeam a_CPU, LeaugeTeam a_CPU2, LeaugeTeam a_CPU3)
        {
            foreach(LeaugeTeam team in Teams)
            {
                Console.WriteLine(team.GetName());
                team.ShowTeam(PlayerName);
            }
            Console.WriteLine("Who would you like to trade with?");
            string input = Console.ReadLine();


        }

        public string GetPlayerID(int a_player)
        {
            return PlayerID[a_player];
        }

        public string GetPlayerName(int a_player)
        {
            return PlayerName[a_player];
        }

        public string GetPlayerPos(int a_player)
        {
            return PlayerPos[a_player];
        }

        public string GetPlayerAge(int a_player)
        {
            return PlayerAge[a_player];
        }

        public string GetPlayerTeam(int a_player)
        {
            return PlayerTeam[a_player];
        }

        public string GetPlayerGamesPlayed(int a_player)
        {
            return PlayerGames[a_player];
        }

        public string GetPlayerGamesStart(int a_player)
        {
            return PlayerGamesStart[a_player];
        }

        public string GetPlayerFieldGoal(int a_player)
        {
            return PlayerFieldGoal[a_player];
        }

        public string GetPlayerFieldGoalAtt(int a_player)
        {
            return PlayerFieldGoalAttempt[a_player];
        }

        public string GetPlayer3Point(int a_player)
        {
            return Player3Point[a_player];
        }

        public string GetPlayer2Point(int a_player)
        {
            return Player2Point[a_player];
        }

        public string GetPlayerFt(int a_player)
        {
            return PlayerFreeThow[a_player];
        }

        public string GetPlayerFtAtt(int a_player)
        {
            return PlayerFreeThrowAttempt[a_player];
        }

        public string GetPlayerOffensiveReb(int a_player)
        {
            return PlayerOffensReb[a_player];
        }
        public string GetPlayerDefenceReb(int a_player)
        {
            return PlayerDefenceReb[a_player];
        }

        public string GetPlayerAssist(int a_player)
        {
            return PlayerAssist[a_player];
        }

        public string GetPlayerSteal(int a_player)
        {
            return PlayerSteal[a_player];
        }

        public string GetPlayerBlock(int a_player)
        {
            return PlayerBlock[a_player];
        }

        public string GetPlayerTurnover(int a_player)
        {
            return PlayerTurnover[a_player];
        }
        public string GetPlayerPersFoul(int a_player)
        {
            return PlayerPersFoul[a_player];
        }

        public string GetPlayerPoints(int a_player)
        {
            return PlayerPoints[a_player];
        }
        public void LoadSeasonStats(Game game)
        {
            String SeasonPath = @"C:\Users\Gabe\source\repos\ConsoleApp1\Seasons\";
            String SeasonSelection;
            String SeasonInput;
            Console.WriteLine("What season would you like to simulate? (ex. 2018-2019)");
            SeasonInput = Console.ReadLine();
            SeasonSelection = SeasonPath + SeasonInput + ".csv";
            StreamReader reader = new StreamReader(File.OpenRead(SeasonSelection));
            List<string> pID = new List<String>();
            List<string> pName = new List<String>();
            List<string> pPos = new List<String>();
            List<string> pAge = new List<String>();
            List<string> pTm = new List<String>();
            List<string> pG = new List<String>();
            List<string> pGS = new List<String>();
            List<string> pFG = new List<String>();
            List<string> pFGA = new List<String>();
            List<string> p3P = new List<String>();
            List<string> p2P = new List<String>();
            List<string> pFT = new List<String>();
            List<string> pFTA = new List<String>();
            List<string> pORB = new List<String>();
            List<string> pDRB = new List<String>();
            List<string> pAST = new List<String>();
            List<string> pSTL = new List<String>();
            List<string> pBLK = new List<String>();
            List<string> pTOV = new List<String>();
            List<string> pPF = new List<String>();
            List<string> pPTS = new List<String>();
            //string vara1, vara2, vara3, vara4;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 4)
                    {
                        pID.Add(values[0]);
                        pName.Add(values[1]);
                        pPos.Add(values[2]);
                        pAge.Add(values[3]);
                        pTm.Add(values[4]);
                        pG.Add(values[5]);
                        pGS.Add(values[6]);
                        pFG.Add(values[8]);
                        pFGA.Add(values[9]);
                        p3P.Add(values[11]);
                        p2P.Add(values[14]);
                        pFT.Add(values[18]);
                        pFTA.Add(values[19]);
                        pORB.Add(values[21]);
                        pDRB.Add(values[22]);
                        pAST.Add(values[24]);
                        pSTL.Add(values[25]);
                        pBLK.Add(values[26]);
                        pTOV.Add(values[27]);
                        pPF.Add(values[28]);
                        pPTS.Add(values[29]);

                    }
                }
            }
            PlayerID = pID.ToArray();
            PlayerName = pName.ToArray();
            PlayerPos = pPos.ToArray();
            PlayerAge = pAge.ToArray();
            PlayerTeam = pTm.ToArray();
            PlayerGames = pG.ToArray();
            PlayerGamesStart = pGS.ToArray();
            PlayerFieldGoal = pFG.ToArray();
            PlayerFieldGoalAttempt = pFGA.ToArray();
            Player3Point = p3P.ToArray();
            Player2Point = p2P.ToArray();
            PlayerFreeThow = pFT.ToArray();
            PlayerFreeThrowAttempt = pFTA.ToArray();
            PlayerOffensReb = pORB.ToArray();
            PlayerDefenceReb = pDRB.ToArray();
            PlayerAssist = pAST.ToArray();
            PlayerSteal = pSTL.ToArray();
            PlayerBlock = pBLK.ToArray();
            PlayerTurnover = pTOV.ToArray();
            PlayerPersFoul = pPF.ToArray();
            PlayerPoints = pPTS.ToArray();
        }
    }
}
