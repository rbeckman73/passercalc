using System;

namespace WpfApp1.Models
{
    public interface IOrganization
    {
         string Name { get; }

        void CalculatePasserRating(QuarterbackStats stats);
    }

    public class NFLCLFOrganization : IOrganization
    {
        public string Name
        {
            get { return "NFL/CFL"; }
        }

        public void CalculatePasserRating(QuarterbackStats stats)
        {
            if (stats.PassingAttempts == 0M)
            {
                stats.PasserRating = 0M;
                return;
            }

            var a = (((stats.Completions / stats.PassingAttempts) - .3M) * 5);
            var b = ((stats.PassingYards / stats.PassingAttempts) - 3) * .25M;
            var c = (stats.TouchdownPasses / stats.PassingAttempts) * 20;
            var d = 2.375M * ((stats.Interceptions / stats.PassingAttempts) * 25);

            stats.PasserRating = ((AdjustResult(a) + AdjustResult(b) + AdjustResult(c) + AdjustResult(d)) / 6) * 100;
            stats.PasserRating = Math.Round(stats.PasserRating, 4, MidpointRounding.AwayFromZero);
        }

        private decimal AdjustResult(decimal number) => (number > 2.375M) ? 2.375M : (number < 0M ? 0M : number);
    }

    public class NCAAOrganization : IOrganization
    {
        public string Name
        {
            get { return "NCAA"; }
        }

        public void CalculatePasserRating(QuarterbackStats stats)
        {
            if (stats.PassingAttempts == 0M)
            {
                stats.PasserRating = 0M;
                return;
            }

            stats.PasserRating = ((8.4M * stats.PassingYards) + (330 * stats.TouchdownPasses) + (100 * stats.Completions) - (200 * stats.Interceptions)) / stats.PassingAttempts;
            stats.PasserRating = Math.Round(stats.PasserRating, 4, MidpointRounding.AwayFromZero);
        }
    }    
}
