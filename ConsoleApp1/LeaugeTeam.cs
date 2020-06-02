using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class LeaugeTeam
    {
        public List<int> team;
        public double[][] PlayersScores;
        int NumPlayers;
        protected Boolean IsHuman;
        protected String TeamName;
        public Double WeekScore;
        public int Wins;
        public int Losses;
        protected int Guards;
        protected int Forwards;
        protected int Center;
        public LeaugeTeam()
        {
            team = new List<int>(5);
            PlayersScores = new double[5][];
            PlayersScores[0] = new double[7];
            PlayersScores[1] = new double[7];
            PlayersScores[2] = new double[7];
            PlayersScores[3] = new double[7];
            PlayersScores[4] = new double[7];
            NumPlayers = 0;
            WeekScore = 0;
            Wins = 0;
            Losses = 0;
            Guards = 0;
            Forwards = 0;
            Center = 0;
        }
        public void AddingPlayer(int a_player)
        {
            team.Add(a_player);
            NumPlayers++;
        }
        public void DroppingPlayer(int a_player)
        {
            team.RemoveAt(a_player);
            NumPlayers--;
        }
        public String GetName()
        {
            return TeamName;
        }
        public void ShowStandings(List<LeaugeTeam> a_Teams)
        {
            int top1;
            int top2;
            int bottom1;
            int bottom2;

            if(a_Teams[0].Wins > a_Teams[1].Wins)
            {
                top1 = 0;
                bottom1 = 1;
            }
            else
            {
                top1 = 1;
                bottom1 = 0;
            }
            if(a_Teams[2].Wins > a_Teams[3].Wins)
            {
                top2 = 2;
                bottom2 = 3;
            }
            else
            {
                top2 = 3;
                bottom2 = 2;
            }
            if (a_Teams[top1].Wins < a_Teams[top2].Wins)
            {
                top1 = top1 + top2;
                top2 = top1 - top2;
                top1 = top1 - top2;
            }
            if(a_Teams[bottom1].Wins < a_Teams[bottom2].Wins)
            {
                bottom1 = bottom1 + bottom2;
                bottom2 = bottom1 - bottom2;
                bottom1 = bottom1 - bottom2;
            }
            if (a_Teams[top2].Wins < a_Teams[bottom1].Wins)
            {
                top2 = top2 + bottom1;
                bottom1 = top2 - bottom1;
                top2 = top2 - bottom1;
            }

            Console.WriteLine("1. {0} {1} {2}", a_Teams[top1].GetName(), a_Teams[top1].Wins, a_Teams[top1].Losses);
            Console.WriteLine("2. {0} {1} {2}", a_Teams[top2].GetName(), a_Teams[top2].Wins, a_Teams[top2].Losses);
            Console.WriteLine("3. {0} {1} {2}", a_Teams[bottom1].GetName(), a_Teams[bottom1].Wins, a_Teams[bottom1].Losses);
            Console.WriteLine("4. {0} {1} {2}", a_Teams[bottom2].GetName(), a_Teams[bottom2].Wins, a_Teams[bottom2].Losses);
        }
        public void ShowTeam(string[] a_playerList)
        {
            int j = 0;
            foreach(int player in team)
            {
                Console.Write("{0}.{1}:", j, a_playerList[player]);
                for(int i = 0; i < 7; i++)
                {
                    Console.Write("{0} ", PlayersScores[j][i]);
                }
                j++;
                Console.WriteLine("");
            }
        }

        public virtual bool CanDraftPosition(String[] a_PlayerPos, int a_pID)
        {
            string Position = a_PlayerPos[a_pID];
            if(Position.Contains('G') == true && Guards < 2)
            {
                Guards++;
                return true;
            }
            else if (Position.Contains('F') == true && Forwards < 2)
            {
                Forwards++;
                return true;
            }
            else if (Position.Contains('C') == true && Center < 2)
            {
                Center++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool CanDraft()
        {
            return false;
        }
        public virtual int Menu(string[] a_playerList, List<LeaugeTeam> a_teams)
        {
            return 1;
        }
        public virtual void AddGuard()
        {
            Guards++;
        }

        public virtual void AddForward()
        {
            Forwards++;
        }

        public virtual void AddCenters()
        {
            Center++;
        }
    }
    class Human : LeaugeTeam
    {

        public Human(String Name)
        {
            TeamName = Name;
            IsHuman = true;
        }

        public override bool CanDraft()
        {
            return IsHuman;
        }
        public override int Menu(string[] a_playerList, List<LeaugeTeam> a_teams)
        {
            string input;
            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Advance to Next week");
                Console.WriteLine("2. See your team");
                Console.WriteLine("3. Check Standings");
                Console.WriteLine("4. See the schedule");
                Console.WriteLine("5. See available players");
                input = Console.ReadLine();
                if (input == "1")
                {
                    return 1;
                }
                else if (input == "2")
                {
                    ShowTeam(a_playerList);
                }
                else if (input == "3")
                {
                    ShowStandings(a_teams);
                }
                else if(input == "4")
                {
                    return 4;
                }
                else if(input == "5")
                {
                    return 5;
                }
                else
                {
                    Console.WriteLine("Invalid option");
                }
            } while (input != "1");
            return 0;
        }

        public override bool CanDraftPosition(String[] a_PlayerPos, int a_pID)
        {
            string Position = a_PlayerPos[a_pID];
            if (Position.Contains('G') == true && Guards < 2)
            {
                Guards++;
                return true;
            }
            else if (Position.Contains('F') == true && Forwards < 2)
            {
                Forwards++;
                return true;
            }
            else if (Position.Contains('C') == true && Center < 2)
            {
                Center++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class CPU : LeaugeTeam
    {
        public CPU(String Name)
        {
            TeamName = Name;
            IsHuman = false;
        }

        public override void AddGuard()
        {
            Guards++;
        }

        public override void AddForward()
        {
            Forwards++;
        }

        public override void AddCenters()
        {
            Center++;
        }

        public override bool CanDraft()
        {
            return IsHuman;
        }
        public override bool CanDraftPosition(String[] a_PlayerPos, int a_pID)
        {
            string Position = a_PlayerPos[a_pID];
            if (Position.Contains('G') == true && Guards < 2)
            {
                return true;
            }
            else if (Position.Contains('F') == true && Forwards < 2)
            {
                return true;
            }
            else if (Position.Contains('C') == true && Center < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
