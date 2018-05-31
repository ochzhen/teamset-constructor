using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace prakt2
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine("Start." + Environment.NewLine);
            
            Console.WriteLine("Enter index number");
            int n = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter group number");
            int m = Int32.Parse(Console.ReadLine());

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var computer = new ProductivityComputer(n, m);

            // var team1 = computer.GetTeam(0, 0, 2, 2, 0);
            // var team2 = computer.GetTeam(1, 2, 0, 0, 2);
            // var team3 = computer.GetTeam(2, 3, 1, 1, 1);
            // var team4 = computer.GetTeam(3, 1, 3, 3, 3);
            
            // var sum = team1.Productivity + team2.Productivity + team3.Productivity + team4.Productivity;
            // Console.WriteLine($"Total Productivity: {sum}");

            var sln = new Solution(computer);

            IReadOnlyCollection<TeamSet> teamSets = sln.MostProductiveTeamSets();

            stopwatch.Stop();

            foreach (var teamSet in teamSets) {
                Console.WriteLine(teamSet);
            }
            
            Console.WriteLine(Environment.NewLine + "Elapsed time: " + stopwatch.Elapsed);
            Console.WriteLine("End.");
        }
    }
}
