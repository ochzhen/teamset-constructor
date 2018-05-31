using System;
using System.Collections.Generic;
using System.Linq;

namespace prakt2 
{
    class Solution {
        private readonly ProductivityComputer _computer;

        public Solution(ProductivityComputer computer)
        {
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
        }

        public IReadOnlyCollection<TeamSet> MostProductiveTeamSets()
        {
            List<TeamSet> teams = AllTeamSets();
            teams.Sort(new TeamSetComparer());
            TeamSet first = teams.First();
            return teams
                .TakeWhile(ts => ts.TotalProductivity == first.TotalProductivity)
                .ToArray();
        }

        private List<TeamSet> AllTeamSets()
        {
            var teamSets = new List<TeamSet>();

            var currentTeams = new LinkedList<Team>();
            
            var usedLayers = new LinkedList<int>();
            var usedEconomists = new LinkedList<int>();
            var usedEngineers = new LinkedList<int>();
            var usedProgrammers = new LinkedList<int>();

            FirstTeam(currentTeams, teamSets,
                usedLayers, usedEconomists, usedEngineers, usedProgrammers);

            return teamSets;
        }

        private void FirstTeam(
            LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers)
        {
            PerformActionInLoop(
                currentTeams, teamSets,
                usedLayers, usedEconomists, usedEngineers, usedProgrammers,
                FirstTeamAction);
        }

        private void FirstTeamAction(LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers,
            int lawyer, int economist, int engineer, int programmer)
        {
            Team team =  _computer.GetTeam(0, lawyer, economist, engineer, programmer);
            if (team == null)
                return;

            currentTeams.AddLast(team);
            SecondTeam(currentTeams, teamSets, usedLayers, usedEconomists, usedEngineers, usedProgrammers);
            currentTeams.RemoveLast();
        }

        private void SecondTeam(
            LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers)
        {
            PerformActionInLoop(
                currentTeams, teamSets,
                usedLayers, usedEconomists, usedEngineers, usedProgrammers,
                SecondTeamAction);
        }

        private void SecondTeamAction(
            LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers,
            int lawyer, int economist, int engineer, int programmer)
        {
            Team team =  _computer.GetTeam(1, lawyer, economist, engineer, programmer);
            if (team == null)
                return;

            currentTeams.AddLast(team);
            ThirdTeam(currentTeams, teamSets, usedLayers, usedEconomists, usedEngineers, usedProgrammers);
            currentTeams.RemoveLast();
        }

        private void ThirdTeam(
            LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers)
        {
            PerformActionInLoop(
                currentTeams, teamSets,
                usedLayers, usedEconomists, usedEngineers, usedProgrammers,
                ThirdTeamAction);
        }

        private void ThirdTeamAction(LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers,
            int lawyer, int economist, int engineer, int programmer)
        {
            Team team =  _computer.GetTeam(2, lawyer, economist, engineer, programmer);
            if (team == null)
                return;

            currentTeams.AddLast(team);
            FourthTeam(currentTeams, teamSets, usedLayers, usedEconomists, usedEngineers, usedProgrammers);
            currentTeams.RemoveLast();
        }

        private void FourthTeam(
            LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers)
        {
            PerformActionInLoop(
                currentTeams, teamSets,
                usedLayers, usedEconomists, usedEngineers, usedProgrammers,
                FourthTeamAction);
        }

        private void FourthTeamAction(LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers,
            int lawyer, int economist, int engineer, int programmer)
        {
            Team team =  _computer.GetTeam(3, lawyer, economist, engineer, programmer);
            if (team == null)
                return;

            currentTeams.AddLast(team);
            var newTeamSet = new TeamSet
            {
                Teams = currentTeams.ToList(),
                TotalProductivity = currentTeams.Sum(t => t.Productivity)
            };
            teamSets.Add(newTeamSet);
            currentTeams.RemoveLast();
        }

        private void PerformActionInLoop(LinkedList<Team> currentTeams, List<TeamSet> teamSets,
            LinkedList<int> usedLayers, LinkedList<int> usedEconomists,
            LinkedList<int> usedEngineers, LinkedList<int> usedProgrammers,
            Action<LinkedList<Team>, List<TeamSet>, LinkedList<int>, LinkedList<int>, LinkedList<int>, LinkedList<int>, int, int, int, int> action)
        {
            for (int lawyer = 0; lawyer < _computer.LawyersCount; lawyer++)
            {
                if (usedLayers.Contains(lawyer))
                    continue;
                usedLayers.AddLast(lawyer);
                for (int economist = 0; economist < _computer.EconomistsCount; economist++)
                {
                    if (usedEconomists.Contains(economist))
                        continue;
                    usedEconomists.AddLast(economist);
                    for (int engineer = 0; engineer < _computer.EngineersCount; engineer++)
                    {
                        if (usedEngineers.Contains(engineer))
                            continue;
                        usedEngineers.AddLast(engineer);
                        for (int programmer = 0; programmer < _computer.ProgrammersCount; programmer++)
                        {
                            if (usedProgrammers.Contains(programmer))
                                continue;
                            usedProgrammers.AddLast(programmer);

                            action(currentTeams, teamSets,
                                usedLayers, usedEconomists, usedEngineers, usedProgrammers,
                                lawyer, economist, engineer, programmer);

                            usedProgrammers.RemoveLast();
                        }
                        usedEngineers.RemoveLast();
                    }
                    usedEconomists.RemoveLast();
                }
                usedLayers.RemoveLast();
            }
        }
    }
}
