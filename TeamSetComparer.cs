using System.Collections.Generic;

namespace prakt2
{
    class TeamSetComparer : IComparer<TeamSet>
    {
        public int Compare(TeamSet x, TeamSet y)
        {
            double diff = x.TotalProductivity - y.TotalProductivity;
            if (diff < 0)
                return 1;
            if (diff > 0)
                return -1;
            return 0;
        }
    }
}