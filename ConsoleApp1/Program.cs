using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program {
        static void Main(string[] args)
        {

            Human player1 = new Human("Player");
            CPU cpu1 = new CPU("CPU1");
            CPU cpu2 = new CPU("CPU2");
            CPU cpu3 = new CPU("CPU3");
            Game game = new Game();
            game.Teams.Add(player1);
            game.Teams.Add(cpu1);
            game.Teams.Add(cpu2);
            game.Teams.Add(cpu3);
            //This is the closest thing I could find for randomizing
            //a list found at https://stackoverflow.com/questions/273313/randomize-a-listt/1262619#1262619 5/20/20
            game.RandomizingDraftOrder();
            game.LoadSeasonStats(game);
            game.Drafting();
            foreach(LeaugeTeam teams in game.Teams)
            {
                Console.WriteLine("{0}'s team:",teams.GetName());
                foreach(int player in teams.team)
                {
                    game.PrintPlayer(player);
                }
                
            }
            //game.SetupSchedule();
            game.SetUpWeekMatchup(player1, cpu1, cpu2, cpu3);
        }
    }
}
