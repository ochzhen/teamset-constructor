using System;
using System.Collections.Generic;

namespace prakt2
{
    class ProductivityComputer
    {
        private double[] pms;
        private double[] lawyers;
        private double[] economists;
        private double[] engineers;
        private double[] programmers;
        private Dictionary<string, Team> teamByKey;

        public int PMsCount => pms.Length;
        public int LawyersCount => lawyers.Length;
        public int EconomistsCount => economists.Length;
        public int EngineersCount => engineers.Length;
        public int ProgrammersCount => programmers.Length;

        public ProductivityComputer(double idxNumber, double groupNumber)
        {
            teamByKey = new Dictionary<string, Team>();
            ComputeProductivityPMs(idxNumber, groupNumber);
            ComputeProductivityLayers(idxNumber, groupNumber);
            ComputeProductivityEconomists(idxNumber, groupNumber);
            ComputeProductivityEngineers(idxNumber, groupNumber);
            ComputeProductivitySEs(idxNumber, groupNumber);
            ComputePossibleTeams();
        }

        public Team GetTeam(int pm, int lawyer, int economist, int engineer, int programmer)
        {
            var key = $"{pm}{lawyer}{economist}{engineer}{programmer}";
            if (teamByKey.TryGetValue(key, out Team team))
            {
                return team;
            }
            return null;
        }

        private void ComputePossibleTeams()
        {
            var rand = new Random();
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
                                    ComputedTeamProductivity(pm, lawyer, economist, engineer, programmer);

                                var key = $"{pm}{lawyer}{economist}{engineer}{programmer}";
                                teamByKey[key] = new Team(pm, lawyer, economist, engineer, programmer, teamProductivity);
                            }
                        }
                    }
                }
            }
        }

        private double ComputedTeamProductivity(int pm, int lawyer, int economist, int engineer, int programmer)
        {
            double x0 = pms[pm];

            double newLaw = NewMemberProductivity(lawyers[lawyer], PmLawyerCoef(pm, lawyer));
            double newEcon = NewMemberProductivity(economists[economist], PmEconomistCoef(pm, economist));
            double newEng = NewMemberProductivity(engineers[engineer], PmEngineerCoef(pm, engineer));
            double newProg = NewMemberProductivity(programmers[programmer], PmProgrammerCoef(pm, programmer));

            double x1 = MemberProductivity(x0, newLaw);
            double x2 = MemberProductivity(x0, newEcon);
            double x3 = MemberProductivity(x0, newEng);
            double x4 = MemberProductivity(x0, newProg);
            
            double sum = x1 + x2 + x3 + x4;
            double r0 = 0.5 + sum / (2 * Math.Sqrt(sum * sum + 1));
            
            double p = (r0 * x0) / (r0 * x0 + (1 - r0) * (1 - x0));
            
            return p;
        }

        private double MemberProductivity(double x0, double xi)
        {
            double result = 0.5 * Math.Sqrt((xi * (1 - x0)) / (x0 * (1 - xi)) + (x0 * (1 - xi)) / (xi * (1 - x0)) - 2);
            return xi >= x0 ? result : -result;
        }

        private double NewMemberProductivity(double xi, double yi)
        {
            return xi * yi / (xi * yi + (1 - yi) * (1 - xi));
        }

        private readonly double[,] pmWithLawyer = new double[,]
        {
            { 0.8, 0.3, 0.2, 0.5 },
            { 0.9, 0.6, 0.4, 0.6 },
            { 0.3, 0.3, 0.7, 0.7 },
            { 0.7, 0.7, 0.2, 0.3 }
        };
        private double PmLawyerCoef(int pm, int lawyer)
        {
            return pmWithLawyer[pm, lawyer];
        }

        private readonly double[,] pmWithEconomist = new double[,]
        {
            { 0.1, 0.6, 0.6, 0.3 },
            { 0.8, 0.2, 0.2, 0.5 },
            { 0.5, 0.5, 0.3, 0.7 },
            { 0.4, 0.6, 0.5, 0.6 }
        };
        private double PmEconomistCoef(int pm, int economist)
        {
            return pmWithEconomist[pm, economist];
        }

        private readonly double[,] pmWithEngineer = new double[,]
        {
            { 0.1, 0.6, 0.6, 0.3 },
            { 0.8, 0.2, 0.2, 0.5 },
            { 0.5, 0.5, 0.3, 0.7 },
            { 0.4, 0.6, 0.5, 0.6 }
        };
        private double PmEngineerCoef(int pm, int engineer)
        {
            return pmWithEngineer[pm, engineer];
        }

        private readonly double[,] pmWithProgrammer = new double[,]
        {
            { 0.5, 0.5, 0.6, 0.7 },
            { 0.4, 0.6, 0.6, 0.2 },
            { 0.3, 0.6, 0.4, 0.3 },
            { 0.2, 0.5, 0.4, 0.5 }
        };
        private double PmProgrammerCoef(int pm, int programmer)
        {
            return pmWithProgrammer[pm, programmer];
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
                ValidProdValue(0.9 - m * n / 100),
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