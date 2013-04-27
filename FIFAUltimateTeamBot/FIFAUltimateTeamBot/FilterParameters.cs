using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFAUltimateTeamBot
{
    public class FilterParameters
    {
        public string Name { get; set; }
        public int MinRating { get; set; }
        public int MaxRating { get; set; }
        public int MinBidPrice { get; set; }
        public int MaxBidPrice { get; set; }
        public int MinBuyNowPrice { get; set; }
        public int MaxBuyNowPrice { get; set; }

        /// <summary>
        /// Parse a string into a workable set of filter parameters.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <returns>The parsed filter parameter.</returns>
        public static FilterParameters Parse(string text)
        {
            //Create the filter to return.
            var filter = new FilterParameters();

            //Go through each parameter in turn by splitting the string at each space.
            foreach (string p in text.Split(' '))
            {

            }

            return filter;
        }
    }
}
