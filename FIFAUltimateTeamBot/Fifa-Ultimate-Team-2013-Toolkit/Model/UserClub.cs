using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class UserClub
    {
        [DataMember(Name = "clubAbbr")]
        public string ClubAbbr { get; set; }

        [DataMember(Name = "clubName")]
        public string ClubName { get; set; }

        [DataMember(Name = "established")]
        public string Established { get; set; }

        [DataMember(Name = "lastAccessTime")]
        public string LastAccessTime { get; set; }

        [DataMember(Name = "platform")]
        public string Platform { get; set; }

        [DataMember(Name = "year")]
        public int Year { get; set; }
    }
}