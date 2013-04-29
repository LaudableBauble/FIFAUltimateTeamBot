using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UltimateTeam.Toolkit.Model;

namespace FIFAUltimateTeamBot
{
    public class ResourceItem
    {
        public long ResourceId { get; set; }
        public int DefaultBidPrice { get; set; }
        public int DefaultBuyNowPrice { get; set; }
        public bool IsAllowedToBeAuctioned { get; set; }
        public Item ResourceData { get; set; }
    }
}
