using System.Collections.Generic;

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
            ProjectManagerId = pm + 1;
            LawyerId = lawyer + 1;
            EconomistId = economist + 1;
            EngineerId = engineer + 1;
            ProgrammerId = programmer + 1;
            Productivity = productivity;
        }
    }

    class TeamSet
    {
        public List<Team> Teams { get; set; }

        public double TotalProductivity { get; set; }

        public TeamSet()
        {
            Teams = new List<Team>();
        }
    }
}
