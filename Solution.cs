using System;
using System.Collections.Generic;
using System.Linq;

namespace prakt2 
{
    class Solution {
        private double[] pms;
        private double[] lawyers;
        private double[] economists;
        private double[] engineers;
        private double[] programmers;
        private Dictionary<string, Team> teamByKey;

        public Solution(double idxNumber, double groupNumber)
        {
            teamByKey = new Dictionary<string, Team>();
            ComputeProductivityPMs(idxNumber, groupNumber);
            ComputeProductivityLayers(idxNumber, groupNumber);
            ComputeProductivityEconomists(idxNumber, groupNumber);
            ComputeProductivityEngineers(idxNumber, groupNumber);
            ComputeProductivitySEs(idxNumber, groupNumber);
            ComputePossibleTeams();
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
            var key = $"0{lawyer}{economist}{engineer}{programmer}";
            Team team = teamByKey[key];
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
            var key = $"1{lawyer}{economist}{engineer}{programmer}";
            Team team = teamByKey[key];
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
            var key = $"2{lawyer}{economist}{engineer}{programmer}";
            Team team = teamByKey[key];
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
            var key = $"3{lawyer}{economist}{engineer}{programmer}";
            Team team = teamByKey[key];
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
            for (int lawyer = 0; lawyer < lawyers.Length; lawyer++)
            {
                if (usedLayers.Contains(lawyer))
                    continue;
                usedLayers.AddLast(lawyer);
                for (int economist = 0; economist < economists.Length; economist++)
                {
                    if (usedEconomists.Contains(economist))
                        continue;
                    usedEconomists.AddLast(economist);
                    for (int engineer = 0; engineer < engineers.Length; engineer++)
                    {
                        if (usedEngineers.Contains(engineer))
                            continue;
                        usedEngineers.AddLast(engineer);
                        for (int programmer = 0; programmer < programmers.Length; programmer++)
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

        private void ComputePossibleTeams()
        {
            for (int pm = 0; pm < pms.Length; pm++)
            {
                for (int lawyer = 0; lawyer < lawyers.Length; lawyer++)
                {
                    for (int economist = 0; economist < economists.Length; economist++)
                    {
                        for (int engineer = 0; engineer < engineers.Length; engineer++)
                        {
                            for (int programmer = 0; programmer < programmers.Length; programmer++)
                            {
                                double teamProductivity =
                                    ComputedProductivity(pm, lawyer, economist, engineer, programmer);
                                var key = $"{pm}{lawyer}{economist}{engineer}{programmer}";
                                teamByKey[key] = new Team(pm, lawyer, economist, engineer, programmer, teamProductivity);
                            }
                        }
                    }
                }
            }
        }

        private double ComputedProductivity(int pm, int lawyer, int economist, int engineer, int programmer)
        {
            throw new NotImplementedException();
        }

        private void ComputeProductivitySEs(double n, double m)
        {
            programmers = new double[]
            {
                0.6,
                ValidProdValue(0.2 + 2 * m * n / 100),
                ValidProdValue(0.5 + m * n / 100),
                ValidProdValue(1.0 - m * n / 100)
            };
        }

        private void ComputeProductivityEngineers(double n, double m)
        {
            engineers = new double[]
            {
                0.75,
                ValidProdValue(0.05 + 3 * m * n / 100),
                ValidProdValue(0.35 + m * n / 100),
                ValidProdValue(0.85 - m * n / 100)
            };
        }

        private void ComputeProductivityEconomists(double n, double m)
        {
            economists = new double[]
            {
                ValidProdValue(0.5 + m * n / 100),
                ValidProdValue(0.9 - m * n / 100),
                ValidProdValue(0.3 + 2 * m * n / 100),
                0.75
            };
        }

        private void ComputeProductivityLayers(double n, double m)
        {
            lawyers = new double[] 
            {
                0.75,
                ValidProdValue(0.25 + 2 * m * n / 100),
                ValidProdValue(0.45 + m * n / 100),
                ValidProdValue(0.95 - m * n / 100)
            };
        }

        private void ComputeProductivityPMs(double n, double m)
        {
            pms = new double[] 
            {
                ValidProdValue(0.5 + m * n / 100),
                ValidProdValue(0.9 - m*n / 100),
                ValidProdValue(0.3 + 2 * m * n / 100),
                0.85
            };
        }

        private double ValidProdValue(double val) 
        {
            if (val > 1)
                return 0.95;
            if (val <= 0)
                return 0.05;
            return val;
        }
    }
}