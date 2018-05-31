using System;
using System.Collections.Generic;
using System.Text;

namespace prakt2
{
    class Team
    {
        public int ProjectManagerId { get; }
        public int LawyerId { get; }
        public int EconomistId { get; }
        public int EngineerId { get; }
        public int ProgrammerId { get; }
        public double Productivity { get; }

        public Team(int pm, int lawyer, int economist,
            int engineer, int programmer, double productivity)
        {
            ProjectManagerId = pm;
            LawyerId = lawyer;
            EconomistId = economist;
            EngineerId = engineer;
            ProgrammerId = programmer;
            Productivity = productivity;
        }

        public override string ToString()
        {
            return $"PM:{ProjectManagerId} Layer:{LawyerId} Economist:{EconomistId} Engineer:{EngineerId} Programmer:{ProgrammerId} Productivity:{Productivity}";
        }
    }

    class TeamSet
    {
        public List<Team> Teams { get; set; }

        public double TotalProductivity { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Environment.NewLine}====== Total Set Productivity: {TotalProductivity} ======");
            Teams.ForEach(team => sb.Append($"{Environment.NewLine}{team}"));
            return sb.ToString();
        }
    }
}
