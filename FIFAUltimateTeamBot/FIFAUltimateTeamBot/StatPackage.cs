using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFAUltimateTeamBot
{
    public class StatPackage
    {
        public long ItemID { get; set; }
        public DateTime Acquired { get; set; }
        public int TimesAuctioned { get; set; }
        public DateTime Sold { get; set; }
        public int SoldFor { get; set; }

        public StatPackage() { }
        public StatPackage(long id)
        {
            ItemID = id;
        }
    }
}
